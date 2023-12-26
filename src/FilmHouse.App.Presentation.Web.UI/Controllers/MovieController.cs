using System.Linq;
using FilmHouse.App.Presentation.Web.UI.Models.Components;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.Web.Controllers;

public class MovieController : Controller
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
    public MovieController(IMediator mediator, ISettingProvider settingProvider, ICodeProvider codeProvider)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
        this._settingProvider = Guard.GetNotNull(settingProvider, nameof(ISettingProvider));
        this._codeProvider = Guard.GetNotNull(codeProvider, nameof(ICodeProvider));
    }

    #endregion Initizalize

    #region 电影详情页

    ////
    //// GET: /Movie/IndexNew/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="movieId"></param>
    /// <returns></returns>
    public async Task<IActionResult> Index(MovieIdVO movieId)
    {
        var command = new FilmHouse.Commands.Movie.DisplayCommand(movieId);
        var display = await this._mediator.Send(command);
        if (display.DiscMovie == null)
        {
            return base.RedirectToAction("NotFound", "Error");
        }

        var model = new MovieViewModel();
        model.Movie = MovieDiscViewModel.FromEntity(display.DiscMovie);
        model.Movie.GenresValue = display.DiscMovie.Genres.AsCodeElement(this._codeProvider, GenresVO.Group).Select(_ => _.Name).ToList();
        model.Movie.CountriesValue = display.DiscMovie.Countries.AsCodeElement(this._codeProvider, CountriesVO.Group).Select(_ => _.Name).ToList();
        model.Movie.LanguagesValue = display.DiscMovie.Languages.AsCodeElement(this._codeProvider, LanguagesVO.Group).Select(_ => _.Name).ToList();
        // 评论总数
        model.Movie.CommentCount = display.CommentCount;

        // 创建者
        model.Movie.IsCreate = display.IsCreate;
        // 想看
        model.Movie.IsPlan = display.IsPlan;
        model.Movie.PlanCount = display.PlanCount;
        // 看过
        model.Movie.IsFinish = display.IsFinish;
        model.Movie.FinishCount = display.FinishCount;
        // 喜欢
        model.Movie.IsFavor = display.IsFavor;
        model.Movie.FavorCount = display.FavorCount;

        // 电影资源
        if (display.DiscMovie.Resources.Any())
        {
            foreach (var item in display.DiscMovie.Resources)
            {
                model.Resources.Add(ResourceDiscViewModel.FromEntity(item));
            }
        }

        // 电影评论
        if (display.DiscMovie.Comments.Any())
        {
            foreach (var item in display.DiscMovie.Comments)
            {
                model.Comments.Add(CommentDiscViewModel.FromEntity(item));
            }
        }
        // 个人评论
        if (display.PersonalReview != null)
        {
            model.PersonalReview = CommentDiscViewModel.FromEntity(display.PersonalReview);
        }

        // 当前影片相关的影集
        if (display.Albums.Any())
        {
            foreach (var item in display.Albums)
            {
                model.Albums.Add(AlbumDiscViewModel.FromEntity(item));
            }
        }

        return base.View(model);
    }


    #endregion
}