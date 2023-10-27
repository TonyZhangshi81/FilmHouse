using FilmHouse.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FilmHouse.Web.Controllers
{
    public class FilmHouseController : Controller
    {
        private readonly ILogger<FilmHouseController> _logger;

        public FilmHouseController(ILogger<FilmHouseController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [Route("[controller]")]
        [Route("[controller]/Index")]
        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}