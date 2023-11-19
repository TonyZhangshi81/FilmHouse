using demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo.Controllers
{
    public class AskController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "求资源";
            ViewBag.PageType = 2;
            ViewBag.NavType = 3;
            return View();
        }
    }
}