using demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int offset = 0)
        {
            // 未登录状态
            ViewBag.IsAuthenticated = 0;
            ViewBag.PageType = 1;
            return View();
        }
    }
}