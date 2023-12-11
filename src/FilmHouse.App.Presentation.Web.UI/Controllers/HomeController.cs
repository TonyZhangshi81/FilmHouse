using FilmHouse.Commands.Home;
using FilmHouse.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.DependencyInjection;

namespace FilmHouse.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Initizalize

        private readonly IMediator _mediator;
        private readonly ISettingProvider _settingProvider;
        private readonly ICurrentRequestId _currentRequestId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public HomeController(IMediator mediator, ISettingProvider settingProvider, ICurrentRequestId currentRequestId)
        {
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this._settingProvider = settingProvider ?? throw new ArgumentNullException(nameof(settingProvider));
            this._currentRequestId = currentRequestId ?? throw new ArgumentNullException(nameof(currentRequestId));
        }

        #endregion Initizalize

        [Route("")]
        [Route("[controller]/{pageIndex}")]
        public async Task<ActionResult> Index(int pageIndex = 1)
        {
            var maxPage = this._settingProvider.GetValue("Home:Discovery:MaxPage").CastTo<int>();

            var model = new HomeViewModel();
            model.Discovery.MaxPage = maxPage;

            var command = new DisplayCommand(pageIndex, maxPage, User.Identity);
            var display = await _mediator.Send(command);

            if (display.Status != 0)
            {
                return RedirectToAction("NotFound", "Error");
            }

            model.Discovery = HomeDiscViewModel.FromEntity(display.Discoveries.ElementAt(0));
            // 最新栏目
            model.News = HomeViewModel.FromEntity(display.NewMovies);
            // 热门栏目
            model.Mosts = HomeViewModel.FromEntity(display.MostMovies);

            // 当前页码
            model.Discovery.CurrentPageIndex = display.CurrentPageIndex;
            // 上一页码
            model.Discovery.PrePageIndex = display.PrePageIndex;
            // 下一页码
            model.Discovery.PostPageIndex = display.PostPageIndex;

            // 想看
            model.Discovery.Movie.IsPlan = display.IsPlan;
            // 看过
            model.Discovery.Movie.IsFinish = display.IsFinish;
            // 喜欢
            model.Discovery.Movie.IsFavor = display.IsFavor;

            return View(model);
        }
    }
}