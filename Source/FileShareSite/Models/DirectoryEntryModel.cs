
namespace FileShareSite.Models
{
    public class DirectoryEntryModel : FileSystemEntryModel
    {
        public override string Type => "DIR";

        public DirectoryEntryModel(string name) : base(name) 
        {
        }
    }
}
