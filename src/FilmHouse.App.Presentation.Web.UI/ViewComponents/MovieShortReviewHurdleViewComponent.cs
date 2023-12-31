using FilmHouse.App.Presentation.Web.UI.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.ViewComponents;

public class MovieShortReviewHurdleViewComponent : ViewComponent
{
    private readonly ILogger<MovieShortReviewHurdleViewComponent> _logger;

    public MovieShortReviewHurdleViewComponent(ILogger<MovieShortReviewHurdleViewComponent> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="viewModel"></param>
    /// <param name="transfer"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(MovieShortReviewHurdleViewModel viewModel, string transfer)
    {
        try
        {
            ViewBag.Transfer = transfer;
            return View("/Views/Components/MovieShortReviewHurdle/Index.cshtml", viewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading MovieShortReviewHurdle.");
            return Content(string.Empty);
        }
    }
}