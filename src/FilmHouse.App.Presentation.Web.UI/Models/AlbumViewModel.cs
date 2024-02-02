using System.ComponentModel.DataAnnotations.Schema;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;

namespace FilmHouse.App.Presentation.Web.UI.Models;

public class AlbumIndexViewModel
{
    public List<AlbumDiscViewModel> Albums { get; set; } = new List<AlbumDiscViewModel>();

    /// <summary>
    /// 影集信息数据对象类
    /// </summary>
    public class AlbumDiscViewModel
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

        public static AlbumDiscViewModel FromEntity(AlbumEntity album, FollowCountVO followCount)
        {
            var viewModel = new AlbumDiscViewModel();
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
}


public class AlbumDetailViewModel
{
    public AlbumDiscViewModel Album { get; set; } = new AlbumDiscViewModel();



    /// <summary>
    /// 影集信息数据对象类
    /// </summary>
    public class AlbumDiscViewModel
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
        /// 创建者ID
        /// </summary>
        public UserIdVO UserId { get; set; }
        /// <summary>
        /// 创建者名
        /// </summary>
        public AccountNameVO Account { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public CreatedOnVO CreatedOn { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public UpDatedOnVO UpDatedOn { get; set; }


        /// <summary>
        /// 关注度
        /// </summary>
        public AmountAttentionVO AmountAttention { get; set; }

        public static AlbumDiscViewModel FromEntity(AlbumEntity album)
        {
            var viewModel = new AlbumDiscViewModel();
            viewModel.AlbumId = album.AlbumId;
            viewModel.Title = album.Title;
            viewModel.Summary = album.Summary;
            viewModel.UserId = album.UserId;
            viewModel.Account = album.UserAccount.Account;
            viewModel.AmountAttention = album.AmountAttention;
            viewModel.CreatedOn = album.CreatedOn;
            viewModel.UpDatedOn = album.UpDatedOn;


            return viewModel;
        }

        /// <summary>
        /// 当前用户是否是创建者
        /// </summary>
        public bool IsCreate { get; set; } = false;

        /// <summary>
        /// 是否取消关注
        /// </summary>
        public bool HasFollow { get; set; } = false;
    }


    /// <summary>
    /// 专辑中的电影
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

}