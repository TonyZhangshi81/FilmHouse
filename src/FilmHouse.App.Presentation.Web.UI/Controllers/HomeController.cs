using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Utils;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects;
using FilmHouse.App.Presentation.Web.UI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FilmHouse.Core.DependencyInjection;

namespace FilmHouse.App.Presentation.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        #region Initizalize

        private readonly IMediator _mediator;
        private readonly ISettingProvider _settingProvider;
        private readonly ICodeProvider _codeProvider;
        private readonly ICurrentRequestId _currentRequestId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="settingProvider"></param>
        /// <param name="codeProvider"></param>
        /// <param name="currentRequestId"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public HomeController(IMediator mediator, ISettingProvider settingProvider, ICodeProvider codeProvider, ICurrentRequestId currentRequestId)
        {
            this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
            this._settingProvider = Guard.GetNotNull(settingProvider, nameof(ISettingProvider));
            this._codeProvider = Guard.GetNotNull(codeProvider, nameof(ICodeProvider));
            this._currentRequestId = Guard.GetNotNull(currentRequestId, nameof(ICurrentRequestId));
        }

        #endregion Initizalize

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [Route("")]
        [Route("[controller]/{pageIndex}")]
        public async Task<ActionResult> Index(int pageIndex = 1)
        {
            this._currentRequestId.Set(new RequestIdVO(Guid.NewGuid()));

            var maxPage = this._settingProvider.GetValue("Home:Discovery:MaxPage").CastTo<int>();

            var model = new HomeViewModel();
            model.Discovery.MaxPage = maxPage;

            var command = new FilmHouse.Commands.Home.DisplayCommand(pageIndex, maxPage);
            var display = await this._mediator.Send(command);

            if (display.Status != 0)
            {
                return base.RedirectToAction("NotFound", "Error");
            }

            model.Discovery = HomeDiscViewModel.FromEntity(display.Discoveries.ElementAt(0));
            model.Discovery.Movie.GenresValue = display.DiscMovie.Genres.AsCodeElement(this._codeProvider, GenresVO.Group).Select(_ => _.Name).ToList();
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

            return base.View(model);
        }
    }
}