using System;
using System.Collections.Generic;

namespace FileShareSite
{
    public class ArchiveDirectory : ArchiveItem
    {
        public Dictionary<string, ArchiveDirectory> Directories { get; }
        public Dictionary<string, ArchiveFile> Files { get; }

        public ArchiveDirectory(ArchiveDirectory parent, string name) : base(parent, name)
        {
            Directories = new Dictionary<string, ArchiveDirectory>(StringComparer.Ordinal);
            Files = new Dictionary<string, ArchiveFile>(StringComparer.Ordinal);
        }

        protected override string CreateFullName()
        {
            return base.CreateFullName() + "/";
        }
    }
}
