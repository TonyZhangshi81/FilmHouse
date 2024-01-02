using FilmHouse.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmHouse.App.Presentation.Web.UI.Models;

public class SearchViewModel
{
    /// <summary>
    /// 检索关键字
    /// </summary>
    public SearchKeywordVO Keyword { get; set; }
}

public class ClassifyViewModel
{
    public IReadOnlyList<SelectListItem> listGenre { get; set; }
    public IReadOnlyList<SelectListItem> listCountry { get; set; }
    public IReadOnlyList<SelectListItem> listYear { get; set; }
    public List<MovieDiscViewModel> listMovies { get; set; } =  new List<MovieDiscViewModel>();

    public int Count { get; set; }
    public int MovieSize { get { return 15; } }

    public string Search { get; set; }
    public string Genre { get; set; }
    public string Country { get; set; }
    public string Year { get; set; }

    public int Page { get; set; }
    public int PagingCount { get; set; }
    public int PagingSize { get { return 10; } }

}