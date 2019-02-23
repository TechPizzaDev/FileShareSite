using System.Diagnostics;
using FileShareSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileShareSite.Controllers
{
    public class StatusController : Controller
    {
        public IActionResult Index()
        {
            return new ContentResult() { Content = "hej" };
        }

        [HttpGet]
        public IActionResult Error()
        {
            string id = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(new ErrorViewModel(id));
        }
    }
}