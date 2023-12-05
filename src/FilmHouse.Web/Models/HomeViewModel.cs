using System.Collections.Generic;
using System.Linq;
using FilmHouse.Data.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace FilmHouse.Web.Models
{
    public class HomeViewModel
    {
        public HomeDiscViewModel Discovery { get; set; }
        //public List<MovieListViewModel> News { get; set; }
        //public List<MovieListViewModel> Mosts { get; set; }
    }

    public class HomeDiscViewModel
    {
        public DiscoveryIdVO DiscoveryId { get; set; }

        public DiscoveryAvatarVO Avatar { get; set; }

        public SortOrderVO Order { get; set; }

        public MovieViewModel Movie { get; set; }

    }

    public class MovieViewModel 
    {
        public MovieTitleVO Title { get; set; }
        public MovieIdVO MovieId { get; set; }
        public DoubanIDVO DoubanID { get; set; }
        public RatingVO Rating { get; set; }
        public SummaryVO Summary { get; set; }

        public List<DirectorViewModel> Directors { get; set; }
        public List<DirectorViewModel> Writers { get; set; }
        public List<DirectorViewModel> Casts { get; set; }

        public bool IsPlan { get; set; } = false;
        public bool IsFinish { get; set; } = false;
        public bool IsFavor { get; set; } = false;
    }

    public class DirectorViewModel
    {
        public CelebrityIdVO Id { get; set; }
        public CelebrityNameVO Name { get; set; }
    }

}