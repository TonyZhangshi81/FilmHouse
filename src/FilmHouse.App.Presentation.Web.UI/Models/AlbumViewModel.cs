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
