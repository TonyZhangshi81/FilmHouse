using FilmHouse.Core.ValueObjects;

namespace FilmHouse.App.Presentation.Web.UI.Models.Components;

public class CelebritiesLinkViewModel
{
    public List<CelebrityViewModel> Celebrities { get; set; } = new List<CelebrityViewModel>();

    public string Type { get; set; } = string.Empty;

    public int Count { get; set; } = 4;

    /// <summary>
    /// 明星数据对象类（带导航功能）
    /// </summary>
    public class CelebrityViewModel
    {
        /// <summary>
        /// 明星ID
        /// </summary>
        public CelebrityIdVO Id { get; set; }
        /// <summary>
        /// 明星名
        /// </summary>
        public CelebrityNameVO Name { get; set; }
    }
}
