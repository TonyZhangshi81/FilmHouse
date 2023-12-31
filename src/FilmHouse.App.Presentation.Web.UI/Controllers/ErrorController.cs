using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/NotAdmin/
        public ActionResult NotAdmin()
        {
            return View();
        }

        //
        // GET: /Error/NotFound/
        public new ActionResult NotFound()
        {
            return View();
        }
    }
}
