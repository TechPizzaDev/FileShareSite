using System;
using System.Collections.Generic;

namespace FileShareSite
{
    public interface IArchive : IEnumerable<IArchiveEntry>, IDisposable
    {
        int Count { get; }
        string Name { get; }

        IArchiveEntry GetEntry(string path);
        IArchiveEntry GetEntry(int index);
    }
}
