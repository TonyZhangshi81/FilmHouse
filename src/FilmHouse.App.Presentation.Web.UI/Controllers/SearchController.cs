using FilmHouse.App.Presentation.Web.UI.Models;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.Controllers;

public class SearchController : Controller
{
    #region Initizalize

    private readonly IMediator _mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public SearchController(IMediator mediator)
    {
        this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
    }

    #endregion Initizalize

    //
    // GET: /Search/
    public async Task<IActionResult> Index(string search, string genre = "0", string country = "0", string year = "0", int page = 1)
    {
        var initCommand = new FilmHouse.Commands.Search.InitCommand();
        var initDisc = await this._mediator.Send(initCommand);

        var viewModel = new SearchInedxViewModel.SearchResultViewModel()
        {
            ListCountry = initDisc.ListCountry,
            ListGenre = initDisc.ListGenre,
            ListYear = initDisc.ListYear,
        };


        var searchCommand = new FilmHouse.Commands.Search.SearchCommand(new SearchKeywordVO(search), new CodeKeyVO(genre), new CodeKeyVO(country), new YearVO(year));
        var searchResult = await this._mediator.Send(searchCommand);

        viewModel.Count = searchResult.Movies.Count();
        foreach (var movie in searchResult.Movies)
        {
            viewModel.ListMovies.Add(SearchInedxViewModel.SearchResultViewModel.FromEntity(movie));
        }
        /*
        string url = Translator.BuildUrl(Request.Url.ToString(), "search", search);
        url = Translator.BuildUrl(url, "genre", genre);
        url = Translator.BuildUrl(url, "country", country);
        url = Translator.BuildUrl(url, "year", year);

        ViewBag.CurrentUrl = url;
        */
        return View(viewModel);
    }
}
