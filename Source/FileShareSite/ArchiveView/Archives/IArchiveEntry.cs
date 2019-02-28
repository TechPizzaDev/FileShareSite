using System;
using System.IO;

namespace FileShareSite
{
    public interface IArchiveEntry : IDisposable
    {
        long Length { get; }
        long CompressedLength { get; }
        bool IsDirectory { get; }
        string FullName { get; }
        string Name { get; }

        Stream OpenStream();
    }
}
