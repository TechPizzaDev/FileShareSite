using System;
using System.Collections.Generic;

namespace FileShareSite
{
    public static class HighlightTypeMap
    {
        private static readonly Dictionary<string, string> _mappings;

        static HighlightTypeMap()
        {
            _mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { ".cs", "cs" },
                { ".cshtml", "cshtml" },
                { ".md", "markdown" },
                { ".yaml", "yaml" },
                { ".yml", "yaml" },
                { ".json", "json" },
                { ".cmd", "dos" },
                { ".bat", "dos" },
                { ".ini", "ini" },
                { ".editorconfig", "ini" }
            };
        }
        
        public static bool TryGetLanguage(string extension, out string language)
        {
            if (extension == null)
                throw new ArgumentNullException(nameof(extension));

            if (!extension.StartsWith('.'))
                extension = '.' + extension;

            return _mappings.TryGetValue(extension, out language);
        }
    }
}