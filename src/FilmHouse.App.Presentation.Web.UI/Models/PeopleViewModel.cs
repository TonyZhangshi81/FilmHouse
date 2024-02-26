using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmHouse.App.Presentation.Web.UI.Models;


public class PeopleIndexViewModel
{
    /// <summary>
    /// 电影信息
    /// </summary>
    public UserAccountDiscViewModel MineDiscViewModel { get; set; } = new UserAccountDiscViewModel();

    /// <summary>
    /// 
    /// </summary>
    public class UserAccountDiscViewModel
    {
        public UserIdVO UserId { get; set; }

        public AccountNameVO Account { get; set; }

        public UserAvatarVO Avatar { get; set; }

        public CoverVO Cover { get; set; }

        /// <summary>
        /// 想看的电影数量
        /// </summary>
        public int PlanCount { get; set; }
        /// <summary>
        /// 想看的电影（带导航）
        /// </summary>
        public List<SelectListItem> MoviePlans { get; set; }

        /// <summary>
        /// 看过的电影数量
        /// </summary>
        public int FinishCount { get; set; }
        /// <summary>
        /// 看过的电影（带导航）
        /// </summary>
        public List<SelectListItem> MovieFinishs { get; set; }

        /// <summary>
        /// 喜欢的电影数量
        /// </summary>
        public int FavorCount { get; set; }
        /// <summary>
        /// 喜欢的电影（带导航）
        /// </summary>
        public List<SelectListItem> MovieFavors { get; set; }

        /// <summary>
        /// 收藏的影人数量
        /// </summary>
        public int CollectCount { get; set; }
        /// <summary>
        /// 收藏的影人（带导航）
        /// </summary>
        public List<SelectListItem> CelebCollects { get; set; }

        /// <summary>
        /// 共同喜好的电影数量
        /// </summary>
        public int CommonCount { get; set; } = 0;
        /// <summary>
        /// 共同喜好的电影（带导航）
        /// </summary>
        public List<MovieListItem> MovieCommons { get; set; } = new List<MovieListItem>();

        /// <summary>
        /// 专辑数量
        /// </summary>
        public int AlbumCount { get; set; }
        /// <summary>
        /// 专辑（带导航）
        /// </summary>
        public List<AlbumListItem> Albums { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CommentCount { get; set; }
        public List<CommentListItem> Comments { get; set; }


        public static UserAccountDiscViewModel FromEntity(UserAccountEntity userAccount)
        {
            var viewModel = new UserAccountDiscViewModel();
            viewModel.UserId = userAccount.UserId;
            viewModel.Account = userAccount.Account;
            viewModel.Avatar = userAccount.Avatar;
            viewModel.Cover = userAccount.Cover;
            return viewModel;
        }
    }

    /// <summary>
    /// 专辑
    /// </summary>
    public class AlbumListItem
    {
        public AlbumIdVO AlbumId { get; set; }
        /// <summary>
        /// 影集名称
        /// </summary>
        public AlbumTitleVO Title { get; set; }
        /// <summary>
        /// 评价
        /// </summary>
        public SummaryVO Summary { get; set; }
        /// <summary>
        /// 封面地址
        /// </summary>
        public CoverVO Cover { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public UserIdVO UserId { get; set; }
        /// <summary>
        /// 評論人名
        /// </summary>
        public AccountNameVO Account { get; set; }
        /// <summary>
        /// 关注度
        /// </summary>
        public AmountAttentionVO AmountAttention { get; set; }
        /// <summary>
        /// 被收藏數
        /// </summary>
        public FollowCountVO FollowCount { get; set; }

        public static AlbumListItem FromEntity(AlbumEntity album, FollowCountVO followCount)
        {
            var viewModel = new AlbumListItem();
            viewModel.AlbumId = album.AlbumId;
            viewModel.Title = album.Title;
            viewModel.Summary = album.Summary;
            viewModel.Cover = album.Cover;
            viewModel.UserId = album.UserId;
            viewModel.Account = album.UserAccount.Account;
            viewModel.AmountAttention = album.AmountAttention;
            viewModel.FollowCount = followCount;

            return viewModel;
        }

    }

    /// <summary>
    /// 共同喜好的电影
    /// </summary>
    public class MovieListItem
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

        public static MovieListItem FromEntity(MovieEntity movie)
        {
            var viewModel = new MovieListItem();
            viewModel.MovieId = movie.MovieId;
            viewModel.Title = movie.Title;
            viewModel.Avatar = movie.Avatar;

            return viewModel;
        }
    }

    /// <summary>
    /// 电影评论
    /// </summary>
    public class CommentListItem
    {
        public MovieIdVO MovieId { get; set; }
        /// <summary>
        /// 电影中文名
        /// </summary>
        public MovieTitleVO Title { get; set; }
        /// <summary>
        /// 內容
        /// </summary>
        public ContentVO Content { get; set; }
        /// <summary>
        /// 發言時間
        /// </summary>
        public CommentTimeVO CommentTime { get; set; }

        public static CommentListItem FromEntity(CommentEntity comment)
        {
            var viewModel = new CommentListItem();
            viewModel.MovieId = comment.MovieId;
            viewModel.Content = comment.Content;
            viewModel.CommentTime = comment.CommentTime;
            viewModel.Title = comment.Movie.Title;

            return viewModel;
        }
    }

}