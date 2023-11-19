using demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo.Controllers
{
    public class AlbumController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "专辑";
            ViewBag.PageType = 2;
            ViewBag.NavType = 4;
            return View();
        }
    }
}