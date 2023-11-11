using demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo.Controllers
{
    public class ErrorController : Controller
    {
        public new IActionResult NotFound()
        {
            ViewBag.Title = "404 Not Found";
            ViewBag.PageType = 2;

            return View();
        }

        public IActionResult NotAdmin()
        {
            ViewBag.Title = "无权限，访问受限";
            ViewBag.PageType = 2;

            return View();
        }
    }
}