using FilmHouse.App.Presentation.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.ViewComponents;

public class CelebritiesLinkViewComponent : ViewComponent
{
    private readonly ILogger<CelebritiesLinkViewComponent> _logger;

    public CelebritiesLinkViewComponent(ILogger<CelebritiesLinkViewComponent> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="celebrities"></param>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(List<CelebrityViewModel> celebrities, string type, int count = 0)
    {
        try
        {
            ViewData["type"] = type;

            var list = celebrities;
            if (count != 0)
            {
                list = celebrities.Take(count).ToList();
            }
            return View("/Views/Components/CelebritiesLink/Index.cshtml", list);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading CelebritiesLink.");
            return Content(string.Empty);
        }
    }
}