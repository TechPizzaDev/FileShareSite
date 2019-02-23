using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileShareSite
{
    public static class HighlightTypeMap
    {
        private static readonly Lazy<Dictionary<string, string>> _mappings;

        static HighlightTypeMap()
        {
            _mappings = new Lazy<Dictionary<string, string>>(
                BuildMappings, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        private static Dictionary<string, string> BuildMappings()
        {
            var mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { ".cs", "cs" },
                { ".cshtml", "cshtml" },
                { ".csproj", "xml" },
                { ".json", "json" },
                { ".md", "markdown" },
                { ".xml", "xml" },
                { ".user", "xml" }
            };

            return mappings;
        }

        public static bool TryGetLanguage(string extension, out string language)
        {
            if (extension == null)
                throw new ArgumentNullException(nameof(extension));

            if (!extension.StartsWith('.'))
                extension = '.' + extension;

            return _mappings.Value.TryGetValue(extension, out language);
        }
    }
}