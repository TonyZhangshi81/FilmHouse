using demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index(string id = "m01")
        {
            ViewBag.MovieId = id;
            switch (id)
            {
                case "m01":
                    ViewBag.Title = "剪刀手爱德华";
                    break;
                case "m02":
                    ViewBag.Title = "黑天鹅";
                    break;
                default:
                    ViewBag.Title = "雷神4：爱与雷霆";
                    break;
            }
            ViewBag.PageType = 2;
            return View();
        }
    }
}