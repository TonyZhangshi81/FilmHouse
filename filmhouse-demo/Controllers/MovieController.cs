using demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace demo.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index(string id = "m01")
        {
            ViewBag.MovieId = id;
            ViewBag.Title = this.GetMovieTitle(id);
            ViewBag.PageType = 2;
            return View();
        }

        private string GetMovieTitle(string id = "m01")
        {
            var title = "";
            switch (id)
            {
                case "m01":
                    title = "剪刀手爱德华";
                    break;
                case "m02":
                    title = "黑天鹅";
                    break;
                case "m03":
                    title = "雷神4：爱与雷霆";
                    break;
                default:
                    title = "雷神4：爱与雷霆";
                    break;
            }
            return title;
        }

        public IActionResult Comment(string id = "m01")
        {
            ViewBag.Title = $"{this.GetMovieTitle(id)}的短评";
            ViewBag.PageType = 2;
            return View();
        }
    }
}