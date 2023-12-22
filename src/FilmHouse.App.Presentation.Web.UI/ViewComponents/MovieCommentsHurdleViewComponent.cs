using FilmHouse.Web.Models;
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
    /// <param name="returnUrl"></param>
    /// <param name="isFinish"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(List<CommentDiscViewModel> comments, string returnUrl, bool isFinish)
    {
        try
        {
            ViewBag.ReturnUrl = returnUrl;
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