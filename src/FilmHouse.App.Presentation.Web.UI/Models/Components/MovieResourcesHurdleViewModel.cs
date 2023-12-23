using FilmHouse.Core.ValueObjects;
using FilmHouse.Web.Models;

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
    }
}
