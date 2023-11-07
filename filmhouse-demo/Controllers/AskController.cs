using demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo.Controllers
{
    public class AskController : Controller
    {
        public IActionResult Index(int offset = 0)
        {
            ViewBag.Title = "求资源";
            ViewBag.PageType = 2;
            ViewBag.NavType = 3;
            return View();
        }
    }
}