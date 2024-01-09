using FilmHouse.App.Presentation.Web.UI.Models.Components;
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
    /// <param name="viewModel"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(CelebritiesLinkViewModel viewModel)
    {
        try
        {
            var list = viewModel.Celebrities;
            if (count != 0)
            {
                list = viewModel.Celebrities.Take(viewModel.Count).ToList();
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