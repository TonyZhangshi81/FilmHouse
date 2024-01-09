using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;

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
                MovieTitle = MovieListViewModel.SetTitle(item.Title, item.TitleEn),
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
        /// 导演列表
        /// </summary>
        public List<CelebrityViewModel> Directors { get; set; } = new List<CelebrityViewModel>();
        /// <summary>
        /// 作者列表
        /// </summary>
        public List<CelebrityViewModel> Writers { get; set; } = new List<CelebrityViewModel>();
        /// <summary>
        /// 类型（文子串）
        /// </summary>
        public List<CodeValueVO> GenresValue { get; set; } = new List<CodeValueVO>();
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

            return viewModel;
        }
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
