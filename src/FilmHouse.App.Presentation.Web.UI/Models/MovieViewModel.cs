using FilmHouse.App.Presentation.Web.UI.Helper;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;

namespace FilmHouse.App.Presentation.Web.UI.Models;

/// <summary>
/// 影片首页用的数据对象类
/// </summary>
public class MovieIndexViewModel
{
    /// <summary>
    /// 电影信息
    /// </summary>
    public MovieDiscViewModel Movie { get; set; } = new MovieDiscViewModel();
    /// <summary>
    /// 电影资源信息
    /// </summary>
    public List<ResourceDiscViewModel> Resources { get; set; } = new List<ResourceDiscViewModel>();
    /// <summary>
    /// 评论信息
    /// </summary>
    public List<CommentDiscViewModel> Comments { get; set; } = new List<CommentDiscViewModel>();
    /// <summary>
    /// 该影片相关影集
    /// </summary>
    public List<AlbumDiscViewModel> Albums { get; set; } = new List<AlbumDiscViewModel>();
    /// <summary>
    /// 个人评论信息
    /// </summary>
    public CommentDiscViewModel PersonalReview { get; set; }

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
        /// 电影英文名
        /// </summary>
        public MovieTitleEnVO TitleEn { get; set; }
        /// <summary>
        /// 影片的更多中文名
        /// </summary>
        public MovieAkaVO Aka { get; set; }
        /// <summary>
        /// 导演列表
        /// </summary>
        public List<CelebrityViewModel> Directors { get; set; } = new List<CelebrityViewModel>();
        /// <summary>
        /// 作者列表
        /// </summary>
        public List<CelebrityViewModel> Writers { get; set; } = new List<CelebrityViewModel>();
        /// <summary>
        /// 主页列表
        /// </summary>
        public List<CelebrityViewModel> Casts { get; set; } = new List<CelebrityViewModel>();
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

        /// <summary>
        /// 年代
        /// </summary>
        public YearVO Year { get; set; }
        /// <summary>
        /// 上映日期
        /// </summary>
        public PubdatesVO Pubdates { get; set; }
        /// <summary>
        /// 片长
        /// </summary>
        public DurationsVO Durations { get; set; }
        /// <summary>
        /// 类型（代碼串）
        /// </summary>
        public GenresVO Genres { get; set; }
        /// <summary>
        /// 类型（文子串）
        /// </summary>
        public List<CodeValueVO> GenresValue { get; set; } = new List<CodeValueVO>();
        /// <summary>
        /// 语言（代碼串）
        /// </summary>
        public LanguagesVO Languages { get; set; }
        /// <summary>
        /// 语言（文子串）
        /// </summary>
        public List<CodeValueVO> LanguagesValue { get; set; } = new List<CodeValueVO>();
        /// <summary>
        /// 国家地区（代碼串）
        /// </summary>
        public CountriesVO Countries { get; set; }
        /// <summary>
        /// 国家地区（文子串）
        /// </summary>
        public List<CodeValueVO> CountriesValue { get; set; } = new List<CodeValueVO>();
        /// <summary>
        /// 评分
        /// </summary>
        public RatingVO Rating { get; set; }
        /// <summary>
        /// 评价人数
        /// </summary>
        public RatingCountVO RatingCount { get; set; }
        /// <summary>
        /// 豆瓣ID
        /// </summary>
        public DoubanIDVO DoubanID { get; set; }
        /// <summary>
        /// IMDbId
        /// </summary>
        public IMDbIDVO IMDbID { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public SummaryVO Summary { get; set; }
        /// <summary>
        /// 电影海报
        /// </summary>
        public MovieAvatarVO Avatar { get; set; }

        public static MovieDiscViewModel FromEntity(MovieEntity movie)
        {
            var viewModel = new MovieDiscViewModel();
            viewModel.MovieId = movie.MovieId;
            viewModel.Avatar = movie.Avatar;
            viewModel.Title = movie.Title;
            viewModel.TitleEn = movie.TitleEn;
            viewModel.Year = movie.Year;
            viewModel.DoubanID = movie.DoubanID;
            viewModel.Rating = movie.Rating;
            viewModel.Summary = movie.Summary;
            viewModel.Aka = movie.Aka;
            viewModel.Pubdates = movie.Pubdates;
            viewModel.Durations = movie.Durations;
            viewModel.Genres = movie.Genres;
            viewModel.Countries = movie.Countries;
            viewModel.Languages = movie.Languages;
            viewModel.IMDbID = movie.IMDb;
            viewModel.RatingCount = movie.RatingCount;

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

        /// <summary>
        /// 评论总数
        /// </summary>
        public int CommentCount { get; set; } = 0;
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
        /// <summary>
        /// 想看、看过、喜欢的数量统计
        /// </summary>
        public int PlanCount { get; set; }
        public int FinishCount { get; set; }
        public int FavorCount { get; set; }
        /// <summary>
        /// 当前用户是否是创建者
        /// </summary>
        public bool IsCreate { get; set; } = false;
    }

    /// <summary>
    /// 电影资源信息数据对象类
    /// </summary>
    public class ResourceDiscViewModel
    { 
        /// <summary>
        /// 资源ID
        /// </summary>
        public ResourceIdVO ResourceId { get; set; }
        /// <summary>
        /// 资源大小（字节单位）
        /// </summary>
        public DisplayResourceSizeVO DiscSize { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        public FavorCountVO FavorCount { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public ResourceTypeVO Type { get; set; }
        /// <summary>
        /// 资源内容
        /// </summary>
        public ResourceContentVO Content { get; set; }
        /// <summary>
        /// 资源名
        /// </summary>
        public ResourceNameVO Name { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public UserIdVO UserId { get; set; }
        /// <summary>
        /// 创建者名
        /// </summary>
        public AccountNameVO Account { get; set; }

        public static ResourceDiscViewModel FromEntity(ResourceEntity resource)
        {
            var viewModel = new ResourceDiscViewModel();
            viewModel.ResourceId = resource.ResourceId;
            viewModel.Content = resource.Content;
            viewModel.Name = resource.Name;
            viewModel.DiscSize = Util.CalculateToDiscSize(resource.Size);
            viewModel.FavorCount = resource.FavorCount;
            viewModel.Type = resource.Type;
            viewModel.UserId = resource.UserId;
            if (resource.UserId != null)
            {
                viewModel.Account = resource.UserAccount.Account;
            }
            return viewModel;
        }
    }

    /// <summary>
    /// 电影评论信息数据对象类
    /// </summary>
    public class CommentDiscViewModel
    {
        /// <summary>
        /// 内容
        /// </summary>
        public ContentVO Content { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public UserIdVO UserId { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public UserAvatarVO UserAvatar { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public AccountNameVO Account { get; set; }
        /// <summary>
        /// 电影ID
        /// </summary>
        public MovieIdVO MovieId { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public CommentTimeVO CommentTime { get; set; }

        public static CommentDiscViewModel FromEntity(CommentEntity comment)
        {
            var viewModel = new CommentDiscViewModel();
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
    /// 电影专辑信息数据对象类
    /// </summary>
    public class AlbumDiscViewModel
    {
        /// <summary>
        /// 专辑ID
        /// </summary>
        public AlbumIdVO AlbumId { get; set; }
        /// <summary>
        /// 专辑名
        /// </summary>
        public AlbumTitleVO Title { get; set; }

        public static AlbumDiscViewModel FromEntity(AlbumEntity album)
        {
            var viewModel = new AlbumDiscViewModel();
            viewModel.AlbumId = album.AlbumId;
            viewModel.Title = album.Title;
            return viewModel;
        }

    }

}


