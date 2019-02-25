namespace FileShareSite
{
    public class ArchiveFileEntry : ArchiveItemEntry
    {
        public long Length { get; }
        public long CompressedLength { get; }

        public ArchiveFileEntry(
            ArchiveDirectoryEntry parent, string name, long length, long compressedLength) : base(parent, name)
        {
            Length = length;
            CompressedLength = compressedLength;
        }
    }
}
