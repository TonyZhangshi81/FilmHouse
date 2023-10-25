using FilmHouse.Dome.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FilmHouse.Dome.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int offset = 0)
        {
            return View();
        }
    }
}