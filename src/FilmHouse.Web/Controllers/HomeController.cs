using FilmHouse.Commands.Home;
using FilmHouse.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Initizalize

        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public HomeController(IMediator mediator)
        {
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion Initizalize

        [Route("")]
        [Route("[controller]")]
        [Route("[controller]/Index")]
        public async Task<ActionResult> Index(int offset = 0)
        {
            // 未登录状态
            ViewBag.IsAuthenticated = 0;
            ViewBag.PageType = 1;
            ViewBag.NavType = 1;

            var model = new HomeViewModel();

            var command = new DisplayCommand();
            var display = await _mediator.Send(command);

            if (display.Status != 0)
            {
                return RedirectToAction("NotFound", "Error");
            }

            if (offset >= display.Discoveries.Count || offset < 0)
            {
                return Redirect("/Home/Index?offset=0");
            }


            var showDiscovery = display.Discoveries.ElementAt(offset);
            model.Discovery.DiscoveryId = showDiscovery.DiscoveryId;
            model.Discovery.Avatar = showDiscovery.Avatar;
            model.Discovery.Order = showDiscovery.Order;



            return View(model);
        }
    }
}