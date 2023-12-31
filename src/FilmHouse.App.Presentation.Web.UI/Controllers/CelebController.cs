using Azure.Core;
using FilmHouse.App.Presentation.Web.UI.Models;
using FilmHouse.Commands.Celeb;
using FilmHouse.Commands.Mark;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.Controllers
{
    public class CelebController : Controller
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
        public CelebController(IMediator mediator, ISettingProvider settingProvider, ICodeProvider codeProvider)
        {
            this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
            this._settingProvider = Guard.GetNotNull(settingProvider, nameof(ISettingProvider));
            this._codeProvider = Guard.GetNotNull(codeProvider, nameof(ICodeProvider));
        }

        #endregion Initizalize

        #region 影人详情页

        /// <summary>
        /// 
        /// </summary>
        /// <param name="celebrityId"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(CelebrityIdVO celebrityId)
        {
            if (!await this._mediator.Send(new IsExistWithCelebrityIdCommand(celebrityId)))
            {
                return RedirectToAction("NotFound", "Error");
            }

            var display = await this._mediator.Send(new DisplayCommand(celebrityId));

            var model = new CelebViewModel();
            // 影人信息
            model.Celebrity = CelebDiscViewModel.FromEntity(display.DiscCelebrity);
            // 是否收藏过
            model.Celebrity.IsCollect = display.IsCollect;
            // 是不是创建者
            model.Celebrity.IsCreate = display.IsCreate;
            // 与明星相关的电影信息
            model.CelebAboutMovies = display.CelebAboutMovies;

            return View(model);
        }

        #endregion
    }
}
