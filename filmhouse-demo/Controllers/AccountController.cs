using demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Mine");
            }
            ViewBag.Title = "登录";
            return View();
        }

        public IActionResult Register()
        {
            ViewBag.Title = "注册";
            return View();
        }
    }
}