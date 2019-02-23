using System;

namespace FileShareSite
{
    public static class FileSizeHelper
    {
        private static readonly string[] _sizeSuffixes = { "K", "M", "G", "T", "P", "E", "Z" };

        public static string GetString(long size)
        {
            int order = -1;
            decimal div = size;
            while (div >= 1024 && order < _sizeSuffixes.Length - 1)
            {
                order++;
                div = div / 1024;
            }

            int decimals = div < 10 ? 1 : 0;
            string result = Math.Round(div, decimals).ToString("0.#");

            if (order >= 0)
                result += " " + _sizeSuffixes[order];
            return result;
        }

        public static string GetString(long? size)
        {
            if (size.HasValue)
                return GetString(size.Value);
            return string.Empty;
        }
    }
}