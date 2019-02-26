
using System;

namespace FileShareSite
{
    public class ArchiveTreeBuilder
    {
        private static readonly char[] _segmentSeparators = { '/', '\\' };

        public ArchiveTreeBuilder()
        {
        }

        public ArchiveDirectory BuildTree(IArchive archive, ProgressDelegate onProgress)
        {
            var topDir = new ArchiveDirectory(null, string.Empty);

            int processed = 0;
            foreach(IArchiveEntry entry in archive)
            {
                string[] segments = GetSegments(entry.FullName);

                void AddRecursively(ArchiveDirectory directory)
                {

                }


                AddRecursively(topDir);

                processed++;
                onProgress(processed / (float)archive.Count);
            }
            
            return null;
        }

        private static string[] GetSegments(string path)
        {
            return path.Split(_segmentSeparators, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
