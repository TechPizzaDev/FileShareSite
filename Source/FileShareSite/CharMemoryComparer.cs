using System;
using System.Collections.Generic;

namespace FileShareSite
{
    public class CharMemoryComparer : IEqualityComparer<ReadOnlyMemory<char>>
    {
        public static readonly CharMemoryComparer Instance = new CharMemoryComparer();

        private CharMemoryComparer()
        {
        }

        public bool Equals(ReadOnlyMemory<char> x, ReadOnlyMemory<char> y)
        {
            if (x.Length != y.Length)
                return false;

            var xSpan = x.Span;
            var ySpan = y.Span;
            int length = xSpan.Length;

            for (int i = 0; i < length; i++)
            {
                if (xSpan[i] != ySpan[i])
                    return false;
            }
            return true;
        }

        public int GetHashCode(ReadOnlyMemory<char> obj)
        {
            unchecked
            {
                var span = obj.Span;
                var length = span.Length;
                int code = 17;
                for (int i = 0; i < length; i++)
                    code = code * 31 + span[i].GetHashCode();
                return code;
            }
        }
    }
}