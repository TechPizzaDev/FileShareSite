using System.Collections;
using System.Collections.Generic;
using Ionic.Zip;

namespace FileShareSite
{
    public class ZipArchive : IArchive, IEnumerable<IArchiveEntry>
    {
        private ZipFile _file;

        public int Count => _file.Count;

        public ZipArchive(ZipFile file)
        {
            _file = file;
        }

        public IArchiveEntry GetEntry(string path)
        {
            return new ZipArchiveEntry(_file[path]);
        }

        public IArchiveEntry GetEntry(int index)
        {
            return new ZipArchiveEntry(_file[index]);
        }

        public void Dispose()
        {
            if (_file != null)
            {
                _file.Dispose();
                _file = null;
            }
        }

        #region GetEnumerator() and Enumerator

        public Enumerator GetEnumerator() => new Enumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IEnumerator<IArchiveEntry> IEnumerable<IArchiveEntry>.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<IArchiveEntry>
        {
            private IArchive _archive;
            private int _index;

            public IArchiveEntry Current { get; private set; }
            object IEnumerator.Current => Current;

            public Enumerator(IArchive archive)
            {
                _archive = archive;
                _index = 0;
                Current = default;
            }

            public bool MoveNext()
            {
                if (_index < _archive.Count)
                {
                    Current = _archive.GetEntry(_index);
                    _index++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                _index = 0;
                Current = null;
            }

            public void Dispose()
            {
            }
        }
        #endregion
    }
}
