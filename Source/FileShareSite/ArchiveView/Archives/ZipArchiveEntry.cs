using System.IO;
using Ionic.Zip;

namespace FileShareSite
{
    public class ZipArchiveEntry : IArchiveEntry
    {
        private ZipEntry _entry;

        public long Length => _entry.UncompressedSize;
        public long CompressedLength => _entry.CompressedSize;
        public bool IsDirectory => _entry.IsDirectory;
        public string FullName => _entry.FileName;
        public string Name => Path.GetFileName(FullName);

        public ZipArchiveEntry(ZipEntry entry)
        {
            _entry = entry;
        }

        public Stream OpenStream()
        {
            return _entry.OpenReader();
        }

        public override string ToString()
        {
            return nameof(ZipArchiveEntry) + ": " + FullName;
        }

        public void Dispose()
        {
            _entry = null;
        }
    }
}
