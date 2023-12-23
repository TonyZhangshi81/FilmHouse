using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Web.Models;

namespace FilmHouse.App.Presentation.Web.UI.Models.Components
{
    public class MovieShortReviewHurdleViewModel
    {
        public int PlanCount { get; set; } = 0;
        public int FinishCount { get; set; } = 0;
        public int FavorCount { get; set; } = 0;
        public bool IsFinish { get; set; } = false;
        public CommentDiscViewModel PersonalReview { get; set; } = new CommentDiscViewModel();
        public List<AlbumDiscViewModel> Albums { get; set; } = new List<AlbumDiscViewModel>();
    }
}
