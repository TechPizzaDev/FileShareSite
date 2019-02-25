using System;
using System.Collections.Generic;
using System.Threading;

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
                { ".md", "markdown" },
                { ".yaml", "yaml" },
                { ".yml", "yaml" },
                { ".json", "json" }
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