using FilmHouse.Business;
using FilmHouse.Business.Commands.Home;
using FilmHouse.Data.Entities;
using FilmHouse.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using static IdentityModel.OidcConstants;

namespace FilmHouse.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommandHandler<DisplayCommand, (int status, IReadOnlyList<DiscoveryEntity> discoveries)> _displayCommandHandler;

        public HomeController(ICommandHandler<DisplayCommand, (int status, IReadOnlyList<DiscoveryEntity> discoveries)> displayCommandHandler)
        {
            this._displayCommandHandler = displayCommandHandler;
        }

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

            var result = await this._displayCommandHandler.HandleAsync(new DisplayCommand());
            if (result.status != 0)
            {
            }

            if (offset >= result.discoveries.Count || offset < 0)
            {
                return Redirect("/Home/Index?offset=0");
            }


            var showDiscovery = result.discoveries.ElementAt(offset);
            model.Discovery.DiscoveryId = showDiscovery.DiscoveryId;
            model.Discovery.Avatar = showDiscovery.Avatar;
            model.Discovery.Order = showDiscovery.Order;



            return View(model);
        }
    }
}