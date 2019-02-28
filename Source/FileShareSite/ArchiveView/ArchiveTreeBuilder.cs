
using System;

namespace FileShareSite
{
    public class ArchiveTreeBuilder
    {
        private static readonly char[] _segmentSeparators = { '/', '\\' };

        public ArchiveTreeBuilder()
        {
        }

        public ArchiveDirectory BuildTree(IArchive archive, ProgressDelegate onProgress)
        {
            var topDir = new ArchiveDirectory(null, archive.Name);

            int processed = 0;
            foreach(IArchiveEntry entry in archive)
            {
                string[] segments = GetPathSegments(entry.FullName);
                if (entry.IsDirectory)
                {
                    CreateDirectoryTree(segments, 0, segments.Length, topDir);
                }
                else
                {
                    if (segments.Length == 1)
                    {
                        string segment = segments[0];
                        var file = CreateFileFromEntry(topDir, segment, entry);
                        topDir.Files.Add(segment, file);
                    }
                    else
                    {
                        var dir = CreateDirectoryTree(segments, 0, segments.Length - 1, topDir);
                        string segment = segments[segments.Length - 1];

                        var file = CreateFileFromEntry(topDir, segment, entry);
                        dir.Files.Add(segment, file);
                    }
                }
                processed++;
                onProgress?.Invoke(processed / (float)archive.Count);
            }
            
            return topDir;
        }

        private static ArchiveDirectory CreateDirectoryTree(string[] segments, int offset, int count, ArchiveDirectory topDir)
        {
            if (offset >= count)
                return topDir;

            string segment = segments[offset];
            if (!topDir.Directories.TryGetValue(segment, out var subDir))
            {
                subDir = new ArchiveDirectory(topDir, segment);
                topDir.Directories.Add(segment, subDir);
            }

            return CreateDirectoryTree(segments, offset + 1, count, subDir);
        }

        private static ArchiveFile CreateFileFromEntry(ArchiveDirectory parent, string name, IArchiveEntry entry)
        {
            return new ArchiveFile(
                parent,
                name,
                entry.Length,
                entry.CompressedLength);
        }

        private static string[] GetPathSegments(string path)
        {
            return path.Split(_segmentSeparators, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
