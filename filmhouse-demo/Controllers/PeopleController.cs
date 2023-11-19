using demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;

namespace demo.Controllers
{
    public class PeopleController : Controller
    {
        public IActionResult Index(string id, string account)
        {
            ViewBag.AccountId = id;
            ViewBag.Title = account + "的主页";
            ViewBag.Account = account;
            ViewBag.PageType = 2;
            return View();
        }
    }
}