using System.IO;

namespace FileShareSite.Models
{
    public class HighlightModel
    {
        public Stream Stream { get; }
        public string HighlightLanguage { get; }

        public string[] Blocks { get; }

        public HighlightModel(Stream stream, string highlightLanguage)
        {
            Stream = stream;
            HighlightLanguage = highlightLanguage;

            using (var reader = new StreamReader(stream))
                Blocks = new string[1] { reader.ReadToEnd() };
        }
    }
}
