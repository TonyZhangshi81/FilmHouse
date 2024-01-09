using FilmHouse.App.Presentation.Web.UI.Helper;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;

namespace FilmHouse.App.Presentation.Web.UI.Models.Components
{
    public class MovieResourcesHurdleViewModel
    {
        public MovieIdVO MovieId { get; set; }

        /// <summary>
        /// 电影中文名
        /// </summary>
        public MovieTitleVO Title { get; set; }

        /// <summary>
        /// 电影资源列表
        /// </summary>
        public List<ResourceDiscViewModel> Resources { get; set; }

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
    }
}
