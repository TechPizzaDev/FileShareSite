using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace FileShareSite.Controllers
{
    public class DownloadController : ControllerBase
    {
        // GET: /<controller>/
        public IActionResult Download(string path)
        {


            if(Request.Headers.TryGetValue("Accept-Encoding", out var values))
            {

            }

            return new FileContentResult(Encoding.UTF8.GetBytes("download"), "application/octet-stream")
            {
                FileDownloadName = "download.txt",
            };
        }
    }
}
