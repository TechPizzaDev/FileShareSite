
namespace FileShareSite.Models
{
    public class FileSystemEntryModel
    {
        public virtual string Type => string.Empty;
        public string Name { get; }

        public FileSystemEntryModel(string name)
        {
            Name = name;
        }
    }
}
