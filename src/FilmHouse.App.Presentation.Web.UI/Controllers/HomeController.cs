﻿using System.Linq;
using FilmHouse.App.Presentation.Web.UI.Models;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmHouse.App.Presentation.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        #region Initizalize

        private readonly IMediator _mediator;
        private readonly ISettingProvider _settingProvider;
        private readonly ICodeProvider _codeProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="settingProvider"></param>
        /// <param name="codeProvider"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public HomeController(IMediator mediator, ISettingProvider settingProvider, ICodeProvider codeProvider)
        {
            this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
            this._settingProvider = Guard.GetNotNull(settingProvider, nameof(ISettingProvider));
            this._codeProvider = Guard.GetNotNull(codeProvider, nameof(ICodeProvider));
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
            var maxPage = this._settingProvider.GetValue(ConfigKeyVO.Keys.HomeDiscoveryMaxPage).CastTo<int>();
            var maxNew = this._settingProvider.GetValue(ConfigKeyVO.Keys.HomeDiscoveryNewMovies).CastTo<int>();
            var maxMost = this._settingProvider.GetValue(ConfigKeyVO.Keys.HomeDiscoveryMostMovies).CastTo<int>();

            var model = new HomeIndexViewModel();
            model.Discovery.MaxPage = maxPage;

            var command = new FilmHouse.Commands.Home.DisplayCommand(pageIndex, maxPage, maxNew, maxMost);
            var display = await this._mediator.Send(command);

            if (display.Status != 0)
            {
                return base.RedirectToAction("NotFound", "Error");
            }

            model.Discovery = HomeIndexViewModel.HomeDiscViewModel.FromEntity(display.Discoveries.ElementAt(0));
            model.Discovery.Movie.GenresValue = model.Discovery.Movie.Genres.AsCodeElement(this._codeProvider, GenresVO.Group).Select(_ => new SelectListItem() { Text = _.Name.AsPrimitive(), Value = _.Code.AsPrimitive() }).ToList();
            // 最新栏目
            model.News = HomeIndexViewModel.FromEntity(display.NewMovies);
            // 热门栏目
            model.Mosts = HomeIndexViewModel.FromEntity(display.MostMovies);

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