using System;

namespace FileShareSite
{
    public delegate void ProgressDelegate(float progress);

    public static class Helper
    {
        public static void Dispose<TDisposable>(ref TDisposable value) where TDisposable : IDisposable
        {
            TDisposable file = value;
            value = default;

            if (file != null)
                file.Dispose();
        }
    }
}
