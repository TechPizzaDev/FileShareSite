namespace FileShareSite
{
    public class ArchiveFile : ArchiveItem
    {
        public long Length { get; }
        public long CompressedLength { get; }

        public ArchiveFile(
            ArchiveDirectory parent, string name, long length, long compressedLength) : base(parent, name)
        {
            Length = length;
            CompressedLength = compressedLength;
        }
    }
}
