using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmHouse.App.Presentation.Web.UI.Models;

public class CommentIndexViewModel
{
    /// <summary>
    /// 电影信息
    /// </summary>
    public MovieDiscViewModel Movie { get; set; } = new MovieDiscViewModel();

    /// <summary>
    /// 評論信息
    /// </summary>
    public List<CommentDiscViewModel> Comments { get; set; } = new List<CommentDiscViewModel>();

    /// <summary>
    /// 評論信息數據對象類
    /// </summary>
    public class CommentDiscViewModel
    {
        /// <summary>
        /// 評論ID
        /// </summary>
        public CommentIdVO CommentId { get; set; }
        /// <summary>
        /// 內容
        /// </summary>
        public ContentVO Content { get; set; }
        /// <summary>
        /// 評論人ID
        /// </summary>
        public UserIdVO UserId { get; set; }
        /// <summary>
        /// 評論人頭像
        /// </summary>
        public UserAvatarVO UserAvatar { get; set; }
        /// <summary>
        /// 評論人名
        /// </summary>
        public AccountNameVO Account { get; set; }
        /// <summary>
        /// 電影ID
        /// </summary>
        public MovieIdVO MovieId { get; set; }
        /// <summary>
        /// 發言時間
        /// </summary>
        public CommentTimeVO CommentTime { get; set; }

        public static CommentDiscViewModel FromEntity(CommentEntity comment)
        {
            var viewModel = new CommentDiscViewModel();
            viewModel.CommentId = comment.CommentId;
            viewModel.UserId = comment.UserId;
            viewModel.MovieId = comment.MovieId;
            viewModel.Content = comment.Content;
            viewModel.CommentTime = comment.CommentTime;
            viewModel.UserAvatar = comment.UserAccount.Avatar;
            viewModel.Account = comment.UserAccount.Account;

            return viewModel;
        }

    }

    /// <summary>
    /// 电影信息数据对象类
    /// </summary>
    public class MovieDiscViewModel
    {
        public MovieIdVO MovieId { get; set; }
        /// <summary>
        /// 电影中文名
        /// </summary>
        public MovieTitleVO Title { get; set; }
        /// <summary>
        /// 电影海报
        /// </summary>
        public MovieAvatarVO Avatar { get; set; }
        /// <summary>
        /// 导演列表（明星ID、明星名）（带导航功能）
        /// </summary>
        public List<SelectListItem> Directors { get; set; } = new List<SelectListItem>();
        /// <summary>
        /// 主演列表（明星ID、明星名）（带导航功能）
        /// </summary>
        public List<SelectListItem> Casts { get; set; } = new List<SelectListItem>();
        /// <summary>
        /// 类型（代碼串）
        /// </summary>
        public GenresVO Genres { get; set; }
        /// <summary>
        /// 类型（带导航功能）
        /// </summary>
        public List<SelectListItem> GenresValue { get; set; } = new List<SelectListItem>();
        /// <summary>
        /// 国家地区（代碼串）
        /// </summary>
        public CountriesVO Countries { get; set; }
        /// <summary>
        /// 国家地区（带导航功能）
        /// </summary>
        public List<SelectListItem> CountriesValue { get; set; } = new List<SelectListItem>();
        /// <summary>
        /// 片长
        /// </summary>
        public DurationsVO Durations { get; set; }
        /// <summary>
        /// 上映日期
        /// </summary>
        public PubdatesVO Pubdates { get; set; }

        public static MovieDiscViewModel FromEntity(MovieEntity movie)
        {
            var viewModel = new MovieDiscViewModel();

            viewModel.MovieId = movie.MovieId;
            viewModel.Avatar = movie.Avatar;
            viewModel.Title = movie.Title;
            viewModel.Pubdates = movie.Pubdates;
            viewModel.Durations = movie.Durations;
            viewModel.Directors = Helper.ModelUtils.GetDirectors(movie.DirectorsId, movie.Directors);
            viewModel.Casts = Helper.ModelUtils.GetCasts(movie.CastsId, movie.Casts);
            viewModel.Genres = movie.Genres;
            viewModel.Countries = movie.Countries;

            return viewModel;
        }

    }

}

