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


        public bool IsPlan { get; set; } = false;
        public bool IsFinish { get; set; } = false;
        public bool IsFavor { get; set; } = false;

        /// <summary>
        /// 当前用户是否是创建者
        /// </summary>
        public bool IsCreate { get; set; } = false;

    }
}
