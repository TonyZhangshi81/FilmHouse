using FilmHouse.Core.ValueObjects;

namespace FilmHouse.App.Presentation.Web.UI.Models.Components
{
    public class MovieActionBarViewModel
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
        /// 当前用户是否为创建者
        /// </summary>
        public bool IsCreate { get; set; } = false;

    }
}
