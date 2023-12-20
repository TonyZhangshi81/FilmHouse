using FilmHouse.App.Presentation.Web.UI.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.ViewComponents;

public class MovieActionBarViewComponent : ViewComponent
{
    private readonly ILogger<MovieActionBarViewComponent> _logger;

    public MovieActionBarViewComponent(ILogger<MovieActionBarViewComponent> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="viewModel"></param>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(MovieActionBarViewModel viewModel, string returnUrl)
    {
        try
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("/Views/Components/MovieActionBar/Index.cshtml", viewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading MovieActionBar.");
            return Content(string.Empty);
        }
    }
}