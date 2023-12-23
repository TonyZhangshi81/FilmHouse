using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;

namespace FilmHouse.Web.Models
{
    public class CommentViewModel
    {
        public CommentDiscViewModel Comment { get; set; } = new CommentDiscViewModel();
    }

    public class CommentDiscViewModel
    {
        public CommentIdVO CommentId { get; set; }

        public ContentVO Content { get; set; }

        public UserIdVO UserId { get; set; }
        public UserAvatarVO UserAvatar { get; set; }
        public AccountNameVO Account { get; set; }

        public MovieIdVO MovieId { get; set; }

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
}