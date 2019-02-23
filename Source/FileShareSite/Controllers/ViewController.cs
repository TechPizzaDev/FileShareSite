using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FileShareSite.Models;
using Ionic.Zip;
using Microsoft.AspNetCore.Mvc;
using FileIO = System.IO.File;

namespace FileShareSite.Controllers
{
    public class ViewController : Controller
    {
        private static readonly char[] _segmentSeparators = { '/', '\\' };

        private static string[] GetSegments(string path)
        {
            return path.Split(_segmentSeparators, StringSplitOptions.RemoveEmptyEntries);
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index(string path)
        {
            const string root = @"C:\Users\Michal Piatkowski\Downloads";

            if (path == null)
                path = root;
            else
                path = Path.Combine(root, path);

            // try to enter a zip archive
            var archiveResult = BuildArchiveView(path);
            if (archiveResult.IsValid)
            {
                if (archiveResult.HasFile)
                {
                    return View("HighlightIndex", new HighlightModel());

                    // serve file inside zip
                    var file = archiveResult.File;
                    var archive = archiveResult.Archive;
                    var stream = archive[file].OpenReader();
                    return new ZipFileStreamResult(archive, stream, MimeTypeMap.GetMime(Path.GetExtension(file)));
                }

                if(archiveResult.HasModel)
                    // serve directory inside zip
                    return View(archiveResult.Model);

                return NotFound();
            }

            // serve file
            if (FileIO.Exists(path))
                return File(FileIO.OpenRead(path), MimeTypeMap.GetMime(Path.GetExtension(path)), enableRangeProcessing: true);

            // serve directory
            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
                return NotFound();

            var dirModel = BuildDirectoryModel(dir);
            return View(dirModel);
        }

        private FileSystemModel BuildDirectoryModel(DirectoryInfo directory)
        {
            var infos = directory.GetFileSystemInfos();
            var items = new FileSystemEntryModel[infos.Length];

            for (int i = 0; i < infos.Length; i++)
            {
                var info = infos[i];

                if (info is DirectoryInfo)
                    items[i] = new DirectoryEntryModel(info.Name);
                else if (info is FileInfo fileInfo)
                    items[i] = new FileEntryModel(info.Name, fileInfo.Length);
            }

            return new FileSystemModel(items, isArchive: false);
        }

        private ArchiveViewResult BuildArchiveView(string path)
        {
            var segments = GetSegments(path);
            var pathBuilder = new StringBuilder(path.Length);

            for (int i = 0; i < segments.Length; i++)
            {
                if (!segments[i].EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                    continue;
                
                for (int j = 0; j < i; j++)
                {
                    pathBuilder.Append(segments[j]);
                    pathBuilder.Append('/');
                }
                pathBuilder.Append(segments[i]);

                string zipPath = pathBuilder.ToString();
                pathBuilder.Clear();

                if (!FileIO.Exists(zipPath))
                    continue;

                int offset = i + 1;
                var subPath = segments.Length > offset ? segments.AsMemory(offset) : Array.Empty<string>().AsMemory();

                var result = BuildArchiveModelFromZip(zipPath, subPath);
                if (result.IsValid)
                    return result;
            }
            
            return default;
        }

        private ArchiveViewResult BuildArchiveModelFromZip(string zipPath, ReadOnlyMemory<string> subPath)
        {
            bool leaveArchiveOpen = false;
            ZipFile archive = null;

            try
            {
                archive = ZipFile.Read(zipPath);
                var topDir = new ZipRootDirectoryEntry();

                foreach (var entry in archive.Entries)
                {
                    var segments = GetSegments(entry.FileName);
                    int index = 0;

                    void Recursive(ZipDirectoryEntry directory)
                    {
                        string name = Path.GetFileName(segments[index++].ToString());

                        if (entry.IsDirectory)
                        {
                            if (!directory.Directories.TryGetValue(name, out var nextDir))
                            {
                                nextDir = new ZipDirectoryEntry(directory, name);
                                directory.Directories.Add(name, nextDir);
                            }

                            if (index < segments.Length)
                                Recursive(nextDir);
                        }
                        else
                        {
                            if (index < segments.Length)
                            {
                                if (!directory.Directories.TryGetValue(name, out var nextDir))
                                {
                                    nextDir = new ZipDirectoryEntry(directory, name);
                                    directory.Directories.Add(name, nextDir);
                                }
                                Recursive(nextDir);
                            }
                            else
                                directory.Files.Add(name, new ZipFileEntry(directory, name, entry.UncompressedSize, entry.CompressedSize));
                        }
                    }

                    Recursive(topDir);
                }

                var modelItems = new List<FileSystemEntryModel>();

                void AddItems(ZipDirectoryEntry directory)
                {
                    foreach (var dir in directory.Directories.Values)
                        modelItems.Add(new ZipDirectoryEntryModel(dir.Name));

                    foreach (var file in directory.Files.Values)
                        modelItems.Add(new ZipFileEntryModel(file.Name, file.Length, file.CompressedLength));
                }

                if (subPath.Length == 0)
                    AddItems(topDir);
                else
                {
                    int index = 0;
                    ArchiveSearchResult RecursiveSearch(ZipDirectoryEntry directory)
                    {
                        if (index < subPath.Length)
                        {
                            var key = subPath.Span[index++];
                            if (directory.Directories.TryGetValue(key, out var nextDir))
                            {
                                return RecursiveSearch(nextDir);
                            }
                            else if (directory.Files.TryGetValue(key, out var file))
                            {
                                return new ArchiveSearchResult(successful: true, value: file.FullName);
                            }

                            return new ArchiveSearchResult(successful: false, value: null);
                        }

                        AddItems(directory);
                        return new ArchiveSearchResult(successful: true, value: null);
                    }

                    var searchResult = RecursiveSearch(topDir);
                    if (searchResult.IsSuccessful && searchResult.Value != null)
                    {
                        leaveArchiveOpen = true;
                        return new ArchiveViewResult(archive, file: searchResult.Value);
                    }
                }

                return new ArchiveViewResult(model: new FileSystemModel(modelItems, isArchive: true));
            }
            finally
            {
                if (!leaveArchiveOpen)
                    archive?.Dispose();
            }
        }

        readonly struct ArchiveSearchResult
        {
            public bool IsSuccessful { get; }
            public string Value { get; }

            public ArchiveSearchResult(bool successful, string value)
            {
                IsSuccessful = successful;
                Value = value;
            }
        }

        readonly struct ArchiveViewResult
        {
            public bool IsValid { get; }

            public ZipFile Archive { get; }
            public string File { get; }
            public bool HasFile => Archive != null && File != null;

            public FileSystemModel Model { get; }
            public bool HasModel => Model != null;

            private ArchiveViewResult(ZipFile archive, string file, FileSystemModel model)
            {
                IsValid = true;
                Archive = archive;
                File = file;
                Model = model;
            }

            public ArchiveViewResult(ZipFile archive, string file) : this(archive, file, null)
            {
            }

            public ArchiveViewResult(FileSystemModel model) : this(null, null, model)
            {
            }
        }
    }
} 