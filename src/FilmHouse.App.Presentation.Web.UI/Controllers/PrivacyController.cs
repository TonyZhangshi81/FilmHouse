using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.Controllers;

public class PrivacyController : Controller
{
    [HttpGet]
    [Route("[controller]/Index")]
    public IActionResult Index()
    {
        return base.View();
    }
}