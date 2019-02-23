using System.IO;
using System.Threading.Tasks;
using Ionic.Zip;
using Microsoft.AspNetCore.Mvc;

namespace FileShareSite
{
    public class ZipFileStreamResult : FileStreamResult
    {
        public ZipFile Archive { get; }
        public new bool EnableRangeProcessing => base.EnableRangeProcessing;
        public new Stream FileStream => base.FileStream;

        public ZipFileStreamResult(ZipFile archive, Stream fileStream, string contentType) : base(fileStream, contentType)
        {
            Archive = archive;
            base.EnableRangeProcessing = false;
        }

        public override void ExecuteResult(ActionContext context)
        {
            base.ExecuteResult(context);
            DisposeArchive(parent: null);
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            return base.ExecuteResultAsync(context).ContinueWith(DisposeArchive, TaskContinuationOptions.ExecuteSynchronously);
        }

        private void DisposeArchive(Task parent)
        {
            Archive.Dispose();
        }
    }
}
