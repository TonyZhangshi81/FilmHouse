using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmHouse.App.Presentation.Web.UI.Models;

public class SearchInedxViewModel
{
    /// <summary>
    /// 检索关键字
    /// </summary>
    public SearchKeywordVO Keyword { get; set; }


    public class SearchResultViewModel
    {
        /// <summary>
        /// 影片類型
        /// </summary>
        public IReadOnlyList<SelectListItem> ListGenre { get; set; }
        /// <summary>
        /// 國家地區
        /// </summary>
        public IReadOnlyList<SelectListItem> ListCountry { get; set; }
        /// <summary>
        /// 年代
        /// </summary>
        public IReadOnlyList<SelectListItem> ListYear { get; set; }
        /// <summary>
        /// 影片查詢結果
        /// </summary>
        public List<MovieDiscViewModel> ListMovies { get; set; } = new List<MovieDiscViewModel>();

        /// <summary>
        /// 查詢件數
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MovieSize { get { return 15; } }
        /// <summary>
        /// 關鍵字查詢
        /// </summary>
        public string Search { get; set; }
        /// <summary>
        /// 影片類型查詢
        /// </summary>
        public string Genre { get; set; }
        /// <summary>
        /// 國家地區
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 年代
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// 頁碼
        /// </summary>
        public int Page { get; set; }
        public int PagingCount { get; set; }
        public int PagingSize { get { return 10; } }

        public static MovieDiscViewModel FromEntity(MovieEntity movie)
        {
            var viewModel = new MovieDiscViewModel();
            viewModel.MovieId = movie.MovieId;
            viewModel.Avatar = movie.Avatar;
            viewModel.Title = movie.Title;
            viewModel.Year = movie.Year;
            viewModel.DoubanID = movie.DoubanID;
            viewModel.Rating = movie.Rating;
            viewModel.Directors = Helper.ModelUtils.GetDirectors(movie.DirectorsId, movie.Directors);
            viewModel.Writers = Helper.ModelUtils.GetWriters(movie.WritersId, movie.Writers);
            viewModel.Casts = Helper.ModelUtils.GetCasts(movie.CastsId, movie.Casts);

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
        /// 电影海报
        /// </summary>
        public MovieAvatarVO Avatar { get; set; }
        /// <summary>
        /// 电影中文名
        /// </summary>
        public MovieTitleVO Title { get; set; }
        /// <summary>
        /// 豆瓣ID
        /// </summary>
        public DoubanIDVO DoubanID { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public RatingVO Rating { get; set; }
        /// <summary>
        /// 年代
        /// </summary>
        public YearVO Year { get; set; }
        /// <summary>
        /// 导演列表（带导航功能）
        /// </summary>
        public List<SelectListItem> Directors { get; set; } = new List<SelectListItem>();
        /// <summary>
        /// 类型（文子串）
        /// </summary>
        public List<CodeValueVO> GenresValue { get; set; } = new List<CodeValueVO>();
        /// <summary>
        /// 作者列表（带导航功能）
        /// </summary>
        public List<SelectListItem> Writers { get; set; } = new List<SelectListItem>();
        /// <summary>
        /// 主页列表（带导航功能）
        /// </summary>
        public List<SelectListItem> Casts { get; set; } = new List<SelectListItem>();
    }

    /// <summary>
    /// 明星数据对象类（带导航功能）
    /// </summary>
    public class CelebrityViewModel
    {
        /// <summary>
        /// 明星ID
        /// </summary>
        public CelebrityIdVO Id { get; set; }
        /// <summary>
        /// 明星名
        /// </summary>
        public CelebrityNameVO Name { get; set; }
    }
}

