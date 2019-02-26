using System;
using System.IO;

namespace FileShareSite
{
    public interface IArchiveEntry : IDisposable
    {
        long Length { get; }
        long CompressedLength { get; }
        string FullName { get; }

        Stream OpenStream();
    }
}
