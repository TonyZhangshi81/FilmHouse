using FilmHouse.App.Presentation.Web.UI.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.ViewComponents;

public class MovieCommentsHurdleViewComponent : ViewComponent
{
    private readonly ILogger<MovieCommentsHurdleViewComponent> _logger;

    public MovieCommentsHurdleViewComponent(ILogger<MovieCommentsHurdleViewComponent> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="commentsViewModel"></param>
    /// <param name="transfer"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(MovieCommentsHurdleViewModel commentsViewModel, string transfer)
    {
        try
        {
            ViewBag.Transfer = transfer;
            return View("/Views/Components/MovieCommentsHurdle/Index.cshtml", commentsViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading MovieCommentsHurdle.");
            return Content(string.Empty);
        }
    }
}