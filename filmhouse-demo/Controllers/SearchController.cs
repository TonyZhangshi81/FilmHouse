using demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index(int offset = 0)
        {
            ViewBag.Title = "分类结果";
            ViewBag.PageType = 2;
            ViewBag.NavType = 2;

            return View();
        }
    }
}