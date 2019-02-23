
namespace FileShareSite.Models
{
    public class FileEntryModel : FileSystemEntryModel
    {
        public long Length { get; }

        public FileEntryModel(string name, long length) : base(name) 
        {
            Length = length;
        }
    }
}
