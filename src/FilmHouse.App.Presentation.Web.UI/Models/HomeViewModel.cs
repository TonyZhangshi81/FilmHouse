using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmHouse.App.Presentation.Web.UI.Models;

/// <summary>
/// 首页用数据对象类
/// </summary>
public class HomeIndexViewModel
{
    /// <summary>
    /// 每日发现
    /// </summary>
    public HomeDiscViewModel Discovery { get; set; } = new HomeDiscViewModel();
    /// <summary>
    /// 最新影片栏目
    /// </summary>
    public List<MovieListViewModel> News { get; set; }
    /// <summary>
    /// 热门影片栏目
    /// </summary>
    public List<MovieListViewModel> Mosts { get; set; }

    public static List<MovieListViewModel> FromEntity(IReadOnlyList<MovieEntity> movices)
    {
        List<MovieListViewModel> news = new List<MovieListViewModel>();
        foreach (var item in movices)
        {
            news.Add(new MovieListViewModel()
            {
                MovieId = item.MovieId,
                // 电影标题
                MovieTitle = Helper.ModelUtils.SetTitle(item.Title, item.TitleEn),
                // 年代
                Year = item.Year
            });
        }
        return news;
    }

    public class HomeDiscViewModel
    {
        /// <summary>
        /// 每日发现ID
        /// </summary>
        public DiscoveryIdVO DiscoveryId { get; set; }
        /// <summary>
        /// 每日发现专题图片
        /// </summary>
        public DiscoveryAvatarVO Avatar { get; set; }
        /// <summary>
        /// 排序顺（滚动播放用）
        /// </summary>
        public SortOrderVO Order { get; set; }
        /// <summary>
        /// 影片信息
        /// </summary>
        public MovieDiscViewModel Movie { get; set; } = new MovieDiscViewModel();
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPageIndex { set; get; }
        /// <summary>
        /// 上一页
        /// </summary>
        public int PrePageIndex { get; set; }
        /// <summary>
        /// 下一页
        /// </summary>
        public int PostPageIndex { get; set; }
        /// <summary>
        /// 最大页码限制数
        /// </summary>
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

    /// <summary>
    /// 最新与热门
    /// </summary>
    public class MovieListViewModel
    {
        public MovieIdVO MovieId { get; set; }
        /// <summary>
        /// 電影名
        /// </summary>
        public MovieTitleAndEnVO MovieTitle { get; set; }
        /// <summary>
        /// 年代
        /// </summary>
        public YearVO Year { get; set; }
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
        /// 豆瓣ID
        /// </summary>
        public DoubanIDVO DoubanID { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public RatingVO Rating { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public SummaryVO Summary { get; set; }
        /// <summary>
        /// 导演列表（明星ID、明星名）（带导航功能）
        /// </summary>
        public List<SelectListItem> Directors { get; set; } = new List<SelectListItem>();
        /// <summary>
        /// 作者列表（明星ID、明星名）（带导航功能）
        /// </summary>
        public List<SelectListItem> Writers { get; set; } = new List<SelectListItem>();
        /// <summary>
        /// 类型（文子串）
        /// </summary>
        public List<SelectListItem> GenresValue { get; set; } = new List<SelectListItem>();
        /// <summary>
        /// 是否想看
        /// </summary>
        public bool IsPlan { get; set; } = false;
        /// <summary>
        /// 是否看过
        /// </summary>
        public bool IsFinish { get; set; } = false;
        /// <summary>
        /// 是否喜欢
        /// </summary>
        public bool IsFavor { get; set; } = false;

        public static MovieDiscViewModel FromEntity(MovieEntity movie)
        {
            var viewModel = new MovieDiscViewModel();
            viewModel.MovieId = movie.MovieId;
            viewModel.Title = movie.Title;
            viewModel.DoubanID = movie.DoubanID;
            viewModel.Rating = movie.Rating;
            viewModel.Summary = movie.Summary;
            viewModel.Directors = Helper.ModelUtils.GetDirectors(movie.DirectorsId, movie.Directors);
            viewModel.Writers = Helper.ModelUtils.GetWriters(movie.WritersId, movie.Writers);

            return viewModel;
        }
    }

}
