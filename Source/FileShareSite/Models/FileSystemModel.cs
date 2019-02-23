using System.Collections.Generic;

namespace FileShareSite.Models
{
    public class FileSystemModel
    {
        public IList<FileSystemEntryModel> Entries { get; }
        public bool IsArchive { get; }

        public FileSystemModel(IList<FileSystemEntryModel> entries, bool isArchive)
        {
            Entries = entries;
            IsArchive = isArchive;
        }
    }
}
