using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;

namespace FilmHouse.App.Presentation.Web.UI.Models.Components;

public class MovieCommentsHurdleViewModel
{
    public List<CommentDiscViewModel> Comments { get; set; } = new List<CommentDiscViewModel>();

    public bool IsFinish { get; set; } = false;

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
    }
}
