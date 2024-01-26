using FilmHouse.App.Presentation.Web.UI.Helper;
using FilmHouse.App.Presentation.Web.UI.Models;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.Controllers;

public class SearchController : Controller
{
    #region Initizalize

    private readonly IMediator _mediator;
    private readonly ISettingProvider _settingProvider;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="settingProvider"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public SearchController(IMediator mediator, ISettingProvider settingProvider)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
        this._settingProvider = Guard.GetNotNull(settingProvider, nameof(ISettingProvider));
    }

    #endregion Initizalize

    //
    // GET: /Search/
    public async Task<IActionResult> Index(string search, string genre = "0", string country = "0", string year = "0", int page = 1)
    {
        var pagingSize = this._settingProvider.GetValue(ConfigKeyVO.Keys.MovieSearchMax).CastTo<int>();

        var initCommand = new FilmHouse.Commands.Search.InitCommand();
        var initDisc = await this._mediator.Send(initCommand);

        // 查询项目
        var viewModel = new SearchInedxViewModel.SearchResultViewModel()
        {
            // 国家地区
            ListCountry = initDisc.ListCountry,
            // 类型
            ListGenre = initDisc.ListGenre,
            // 年代
            ListYear = initDisc.ListYear,
        };


        var searchCommand = new FilmHouse.Commands.Search.SearchCommand(new SearchKeywordVO(search), new CodeKeyVO(genre), new CodeKeyVO(country), new YearVO(year), pagingSize, page);
        var searchResult = await this._mediator.Send(searchCommand);

        // 单页显示件数
        viewModel.PagingSize = pagingSize;
        // 当前页码
        viewModel.Page = page;
        // 查询总页数
        viewModel.PagingCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(searchResult.SearchCount) / pagingSize));
        // 查询总件数
        viewModel.Count = searchResult.SearchCount;
        // 查询结果（单页）
        foreach (var movie in searchResult.Movies)
        {
            var mark = searchResult.Marks.Where(d => d.MovieId == movie.MovieId).FirstOrDefault();

            // 电影偏好
            if (mark == null)
            {
                viewModel.ListMovies.Add(SearchInedxViewModel.SearchResultViewModel.FromEntity(movie));
            }
            else
            {
                viewModel.ListMovies.Add(SearchInedxViewModel.SearchResultViewModel.FromEntity(movie, mark.IsPlan, mark.IsFinish, mark.IsFavor));
            }
        }
        string url = ModelUtils.BuildUrl(Request.GetDisplayUrl(), "search", search);
        url = ModelUtils.BuildUrl(url, "genre", genre);
        url = ModelUtils.BuildUrl(url, "country", country);
        url = ModelUtils.BuildUrl(url, "year", year);
        ViewBag.CurrentUrl = url;
        return View(viewModel);
    }
}
