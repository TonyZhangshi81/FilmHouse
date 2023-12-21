using System.ComponentModel.DataAnnotations;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;

namespace FilmHouse.Web.Models
{
    public class ManageResViewModel
    {
        public string Id { get; set; }

        public string MovieTitle { get; set; }

        public string MovieId { get; set; }

        [Display(Name = "资源链接")]
        //[Required(ErrorMessage = "资源链接 不能为空")]
        public string Content { get; set; }

        [Display(Name = "资源标题")]
        [Required(ErrorMessage = "资源标题 不能为空")]
        public string FileName { get; set; }

        [Display(Name = "资源大小")]
        [Required(ErrorMessage = "资源大小 不能为空")]
        [Range(0, float.MaxValue, ErrorMessage = ("请输入有效的文件大小"))]
        public string FileSize { get; set; }

        public byte ResType { get; set; }

        public byte Status { get; set; }

        public string User { get; set; }
    }

    public class ResourceDiscViewModel
    {
        public ResourceIdVO ResourceId { get; set; }

        public ResourceContentVO Content { get; set; }

        public ResourceNameVO Name { get; set; }

        public ResourceSizeVO Size { get; set; }

        public DisplayResourceSizeVO DiscSize { get; set; }

        public FavorCountVO FavorCount { get; set; }

        public ResourceTypeVO Type { get; set; }

        public UserIdVO UserId { get; set; }
        public AccountNameVO Account { get; set; }

        public MovieIdVO MovieId { get; set; }
        public MovieTitleVO MovieTitle { get; set; }

        public ReviewStatusVO ReviewStatus { get; set; }

        public NoteVO Note { get; set; }

        public static ResourceDiscViewModel FromEntity(ResourceEntity resource)
        {
            var viewModel = new ResourceDiscViewModel();
            viewModel.ResourceId = resource.ResourceId;
            viewModel.Content = resource.Content;
            viewModel.Name = resource.Name;
            viewModel.Size = resource.Size;
            viewModel.DiscSize = CalculateToDiscSize(resource.Size);
            viewModel.FavorCount = resource.FavorCount;
            viewModel.Type = resource.Type;
            viewModel.ReviewStatus = resource.ReviewStatus;
            viewModel.Note = resource.Note;
            viewModel.UserId = resource.UserId;
            viewModel.MovieId = resource.MovieId;

            if (resource.UserId != null)
            {
                viewModel.Account = resource.UserAccount.Account;
            }
            if (resource.MovieId != null)
            {
                viewModel.MovieTitle = resource.Movie.Title;
            }

            return viewModel;
        }

        /// <summary>
        /// 显示用文件大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        static DisplayResourceSizeVO CalculateToDiscSize(ResourceSizeVO size)
        {
            if (size.AsPrimitive() > 1024 * 1024 * 1024)
            {
                return new($"{size.AsPrimitive() / (1024 * 1024 * 1024)} G");
            }
            else if (size.AsPrimitive() > 1024 * 1024)
            {
                return new($"{size.AsPrimitive() / (1024 * 1024)} M");
            }
            else if (size.AsPrimitive() > 1024)
            {
                return new($"{size.AsPrimitive() / 1024} K");
            }
            else
            {
                return new($"{size.AsPrimitive()} 字节");
            }
        }
    }

    public class FilterResViewModel
    {
        public List<ResourceDiscViewModel> Ress { get; set; }

        public string Search { get; set; }

        public bool OnlyUnaudit { get; set; }

        public int Page { get; set; }
    }

    public class RejectResViewModel
    {
        public string Id { get; set; }
        public string Note { get; set; }
    }

}