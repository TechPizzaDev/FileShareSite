using IonicZipFile = Ionic.Zip.ZipFile;

namespace FileShareSite
{
    public static class ZipExtensions
    {
        public static IArchive ToArchive(this IonicZipFile file)
        {
            return new ZipArchive(file);
        }
    }
}
