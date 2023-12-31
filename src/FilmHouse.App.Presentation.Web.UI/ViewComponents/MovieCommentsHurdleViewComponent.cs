using FilmHouse.App.Presentation.Web.UI.Models;
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
    /// <param name="comments"></param>
    /// <param name="transfer"></param>
    /// <param name="isFinish"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(List<CommentDiscViewModel> comments, string transfer, bool isFinish)
    {
        try
        {
            ViewBag.Transfer = transfer;
            ViewBag.IsFinish = isFinish;
            return View("/Views/Components/MovieCommentsHurdle/Index.cshtml", comments);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading MovieCommentsHurdle.");
            return Content(string.Empty);
        }
    }
}