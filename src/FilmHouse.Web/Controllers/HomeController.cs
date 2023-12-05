using FilmHouse.Business;
using FilmHouse.Business.Commands.Home;
using FilmHouse.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FilmHouse.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommandHandler<DisplayCommand, IReadOnlyList<DiscoveryEntity>> _displayCommandHandler;

        public HomeController(ICommandHandler<DisplayCommand, IReadOnlyList<DiscoveryEntity>> displayCommandHandler)
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


            var list = await this._displayCommandHandler.HandleAsync(new DisplayCommand());


            return View();
        }
    }
}