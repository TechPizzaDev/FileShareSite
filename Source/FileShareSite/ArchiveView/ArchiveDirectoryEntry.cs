using System;
using System.Collections.Generic;

namespace FileShareSite
{
    public class ArchiveDirectoryEntry : ArchiveItemEntry
    {
        public Dictionary<string, ArchiveDirectoryEntry> Directories { get; }
        public Dictionary<string, ArchiveFileEntry> Files { get; }

        public ArchiveDirectoryEntry(ArchiveDirectoryEntry parent, string name) : base(parent, name)
        {
            Directories = new Dictionary<string, ArchiveDirectoryEntry>(StringComparer.Ordinal);
            Files = new Dictionary<string, ArchiveFileEntry>(StringComparer.Ordinal);
        }
    }
}
