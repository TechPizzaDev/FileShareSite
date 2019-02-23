using System;

namespace FileShareSite
{
    public static class StringHelper
    {
        public static bool EndsWith(this ReadOnlyMemory<char> memory, string value, StringComparison comparison)
        {
            if (value.Length > memory.Length)
                return false;

            var span = memory.Span;
            int sizeDiff = span.Length - value.Length;
            for (int i = value.Length; i-- > 0;)
            {
                char c = span[i + sizeDiff];
                switch (comparison)
                {
                    case StringComparison.InvariantCultureIgnoreCase:
                        if (char.ToLowerInvariant(c) != char.ToLowerInvariant(value[i]))
                            return false;
                        break;

                    case StringComparison.OrdinalIgnoreCase:
                        if (char.ToLower(c) != char.ToLower(value[i]))
                            return false;
                        break;

                    case StringComparison.Ordinal:
                        if (c != value[i])
                            return false;
                        break;

                    default:
                        throw new ArgumentException("String comparison is not supported: " + comparison);
                }
            }
            return true;
        }

        public static int Count(this string str, char value)
        {
            if (str == null)
                return 0;

            int count = 0;
            for (int i = 0; i < str.Length; i++)
                if (str[i] == value)
                    count++;
            return count;
        }

        public static int Count(this string str, char value1, char value2)
        {
            if (str == null)
                return 0;

            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c == value1 || c == value2)
                    count++;
            }
            return count;
        }
    }
}
