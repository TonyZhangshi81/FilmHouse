using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.App.Presentation.Web.UI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmHouse.App.Presentation.Web.UI.Models.Components
{
    public class MovieShortReviewHurdleViewModel
    {
        /// <summary>
        /// 想看的人數
        /// </summary>
        public int PlanCount { get; set; } = 0;
        /// <summary>
        /// 已看的人數
        /// </summary>
        public int FinishCount { get; set; } = 0;
        /// <summary>
        /// 喜歡的人數
        /// </summary>
        public int FavorCount { get; set; } = 0;

        /// <summary>
        /// 登錄是否看過（可以追加評論）
        /// </summary>
        public bool IsFinish { get; set; } = false;

        /// <summary>
        /// 個人短評內容
        /// </summary>
        public ContentVO Content { get; set; }
        /// <summary>
        /// 評論ID
        /// </summary>
        public CommentIdVO CommentId { get; set; }
        /// <summary>
        /// 影片相關的專輯
        /// </summary>
        public List<SelectListItem> Albums { get; set; } = new List<SelectListItem>();

    }
}
