using demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Mine");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public IActionResult Register(string returnUrl)
        {
            ViewBag.Title = "注册";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}