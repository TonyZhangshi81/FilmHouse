using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.Web.Controllers;

public class PrivacyController : Controller
{
    [Route("[controller]/Index")]
    public IActionResult Index()
    {
        return View();
    }
}