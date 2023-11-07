using demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo.Controllers
{
    public class AlbumController : Controller
    {
        public IActionResult Index(int offset = 0)
        {
            ViewBag.Title = "专辑";
            ViewBag.PageType = 2;
            ViewBag.NavType = 4;
            return View();
        }
    }
}