using System.IO;

namespace FileShareSite.Models
{
    public class FileEntryModel : FileSystemEntryModel
    {
        private string _type;

        public override string Type => _type;
        public long Length { get; }

        public FileEntryModel(string name, long length) : base(name) 
        {
            Length = length;
            _type = base.Type;

            string extension = Path.GetExtension(name);
            if (Controllers.ViewController._archiveExtensions.Contains(extension))
                _type = "ZIP";
        }
    }
}
