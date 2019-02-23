
namespace FileShareSite.Models
{
    public class ZipFileEntryModel : FileEntryModel
    {
        public long CompressedLength { get; }

        public ZipFileEntryModel(string name, long length, long compressedLength) : base(name, length) 
        {
            CompressedLength = compressedLength;
        }
    }
}
