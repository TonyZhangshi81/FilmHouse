using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Data.Core.ValueObjects;
using FilmHouse.Data.Entities;

namespace FilmHouse.Web.Models
{
    public class HomeViewModel
    {
        public HomeDiscViewModel Discovery { get; set; } = new HomeDiscViewModel();
        public List<MovieListViewModel> News { get; set; }
        public List<MovieListViewModel> Mosts { get; set; }

        public static List<MovieListViewModel> FromEntity(IReadOnlyList<MovieEntity> movices)
        {
            List <MovieListViewModel> news = new List<MovieListViewModel>();
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

        public MovieViewModel Movie { get; set; } = new MovieViewModel();

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

            viewModel.Movie = MovieViewModel.FromEntity(discovery.Movie);

            return viewModel;
        }

    }

    public class MovieViewModel
    {
        public MovieTitleVO Title { get; set; }
        public MovieIdVO MovieId { get; set; }
        public DoubanIDVO DoubanID { get; set; }
        public RatingVO Rating { get; set; }
        public SummaryVO Summary { get; set; }

        public List<CelebrityViewModel> Directors { get; set; } = new List<CelebrityViewModel>();
        public List<CelebrityViewModel> Writers { get; set; } = new List<CelebrityViewModel>();
        public List<CelebrityViewModel> Casts { get; set; } = new List<CelebrityViewModel>();

        public bool IsPlan { get; set; } = false;
        public bool IsFinish { get; set; } = false;
        public bool IsFavor { get; set; } = false;

        public static MovieViewModel FromEntity(MovieEntity movie)
        {
            var viewModel = new MovieViewModel();

            viewModel.Title = movie.Title;
            viewModel.MovieId = movie.MovieId;
            viewModel.DoubanID = movie.DoubanID;
            viewModel.Summary = movie.Summary;
            viewModel.Title = movie.Title;

            #region Directors

            if (movie.DirectorsId == null)
            {
                if (movie.Directors != null)
                {
                    var directors = movie.Directors.ToEnumerable().GetEnumerator();
                    while (directors.MoveNext())
                    {
                        viewModel.Directors.Add(new CelebrityViewModel() { Name = directors.Current });
                    }
                }
            }
            else
            {
                var index = 0;
                var directorIds = movie.DirectorsId.ToEnumerable().GetEnumerator();
                var directors = movie.Directors.ToEnumerable().Cast<CelebrityNameVO>().ToArray();
                while (directorIds.MoveNext())
                {
                    viewModel.Directors.Add(new CelebrityViewModel() { Id = directorIds.Current, Name = directors.ElementAt(index) });
                    index++;
                }
            }

            #endregion Directors

            #region Writers

            if (movie.WritersId == null)
            {
                if (movie.Writers != null)
                {
                    var writers = movie.Writers.ToEnumerable().GetEnumerator();
                    while (writers.MoveNext())
                    {
                        viewModel.Writers.Add(new CelebrityViewModel() { Name = writers.Current });
                    }
                }
            }
            else
            {
                var index = 0;
                var writerIds = movie.WritersId.ToEnumerable().GetEnumerator();
                var writers = movie.Writers.ToEnumerable().Cast<CelebrityNameVO>().ToArray();
                while (writerIds.MoveNext())
                {
                    viewModel.Writers.Add(new CelebrityViewModel() { Id = writerIds.Current, Name = writers.ElementAt(index) });
                    index++;
                }
            }

            #endregion Writers

            #region Casts

            if (movie.CastsId == null)
            {
                if (movie.Casts != null)
                {
                    var casts = movie.Casts.ToEnumerable().GetEnumerator();
                    while (casts.MoveNext())
                    {
                        viewModel.Casts.Add(new CelebrityViewModel() { Name = casts.Current });
                    }
                }
            }
            else
            {
                var index = 0;
                var castsId = movie.CastsId.ToEnumerable().GetEnumerator();
                var casts = movie.Casts.ToEnumerable().Cast<CelebrityNameVO>().ToArray();
                while (castsId.MoveNext())
                {
                    viewModel.Casts.Add(new CelebrityViewModel() { Id = castsId.Current, Name = casts.ElementAt(index) });
                    index++;
                }
            }

            #endregion Casts

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
            if(titleEn != null)
            {
                tle = new MovieTitleAndEnVO( $"{title}\t{titleEn.AsPrimitive()}");
            }
            return tle;
        }
    }

}