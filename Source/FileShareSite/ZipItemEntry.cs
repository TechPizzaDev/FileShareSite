using System;
using System.Collections.Generic;
using System.Text;

namespace FileShareSite
{
    public abstract class ZipItemEntry
    {
        public ZipDirectoryEntry Parent { get; }
        public string Name { get; }
        public string FullName
        {
            get
            {
                var builder = new StringBuilder();

                // start the recursion
                if(Parent != null)
                    RecursiveAppend(builder, Parent);

                builder.Append(Name);
                return builder.ToString();
            }
        }

        protected ZipItemEntry(ZipDirectoryEntry parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        private static void RecursiveAppend(StringBuilder builder, ZipDirectoryEntry parent)
        {
            // calling this function again before appending the current
            // name creates the full name by unwinding the hierarchy
            if (parent.Parent != null)
                RecursiveAppend(builder, parent.Parent);

            if (!(parent is ZipRootDirectoryEntry))
            {
                builder.Append(parent.Name);
                builder.Append('/');
            }
        }
    }

    public class ZipFileEntry : ZipItemEntry
    {
        public long Length { get; }
        public long CompressedLength { get; }

        public ZipFileEntry(
            ZipDirectoryEntry parent, string name, long length, long compressedLength) : base(parent, name)
        {
            Length = length;
            CompressedLength = compressedLength;
        }
    }

    public class ZipDirectoryEntry : ZipItemEntry
    {
        public Dictionary<string, ZipDirectoryEntry> Directories { get; }
        public Dictionary<string, ZipFileEntry> Files { get; }

        public ZipDirectoryEntry(ZipDirectoryEntry parent, string name) : base(parent, name)
        {
            Directories = new Dictionary<string, ZipDirectoryEntry>(StringComparer.Ordinal);
            Files = new Dictionary<string, ZipFileEntry>(StringComparer.Ordinal);
        }
    }
}
