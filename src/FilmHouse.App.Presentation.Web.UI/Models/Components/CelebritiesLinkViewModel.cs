using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmHouse.App.Presentation.Web.UI.Models.Components;

public class CelebritiesLinkViewModel
{
    /// <summary>
    /// 明星ID、明星名
    /// </summary>
    public List<SelectListItem> Celebrities { get; set; } = new List<SelectListItem>();
    /// <summary>
    /// 類別（導演、編劇、主演）
    /// </summary>
    public string Type { get; set; } = string.Empty;

}
