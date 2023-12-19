using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.Web.Controllers;

public class PrivacyController : Controller
{
    [HttpGet]
    [Route("[controller]/Index")]
    public IActionResult Index()
    {
        return this.View();
    }
}