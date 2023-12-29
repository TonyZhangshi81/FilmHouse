using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;

namespace FilmHouse.App.Presentation.Web.UI.Models;

public class HomeViewModel
{
    public HomeDiscViewModel Discovery { get; set; } = new HomeDiscViewModel();
    public List<MovieListViewModel> News { get; set; }
    public List<MovieListViewModel> Mosts { get; set; }

    public static List<MovieListViewModel> FromEntity(IReadOnlyList<MovieEntity> movices)
    {
        List<MovieListViewModel> news = new List<MovieListViewModel>();
        foreach (var item in movices)
        {
            news.Add(new MovieListViewModel() { MovieId = item.MovieId, MovieTitle = MovieListViewModel.SetTitle(item.Title, item.TitleEn), Year = item.Year });
        }
        return news;
    }
}

public class HomeDiscViewModel
{
    public DiscoveryIdVO DiscoveryId { get; set; }

    public DiscoveryAvatarVO Avatar { get; set; }

    public SortOrderVO Order { get; set; }

    public MovieDiscViewModel Movie { get; set; } = new MovieDiscViewModel();

    public int CurrentPageIndex { set; get; }
    public int PrePageIndex { get; set; }
    public int PostPageIndex { get; set; }
    public int MaxPage { get; set; }

    public static HomeDiscViewModel FromEntity(DiscoveryEntity discovery)
    {
        var viewModel = new HomeDiscViewModel();

        viewModel.DiscoveryId = discovery.DiscoveryId;
        viewModel.Avatar = discovery.Avatar;
        viewModel.Order = discovery.Order;

        viewModel.Movie = MovieDiscViewModel.FromEntity(discovery.Movie);

        return viewModel;
    }

}

public class CelebrityViewModel
{
    public CelebrityIdVO Id { get; set; }
    public CelebrityNameVO Name { get; set; }
}

public class MovieListViewModel
{
    public MovieIdVO MovieId { get; set; }
    public MovieTitleAndEnVO MovieTitle { get; set; }
    public YearVO Year { get; set; }

    public static MovieTitleAndEnVO SetTitle(MovieTitleVO title, MovieTitleEnVO titleEn)
    {
        var tle = new MovieTitleAndEnVO(title.AsPrimitive());
        if (titleEn != null)
        {
            tle = new MovieTitleAndEnVO($"{title}\t{titleEn.AsPrimitive()}");
        }
        return tle;
    }
}