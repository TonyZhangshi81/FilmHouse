using FilmHouse.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.ViewComponents;

public class MovieSummaryHurdleViewComponent : ViewComponent
{
    private readonly ILogger<MovieSummaryHurdleViewComponent> _logger;

    public MovieSummaryHurdleViewComponent(ILogger<MovieSummaryHurdleViewComponent> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="summary"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(SummaryVO summary)
    {
        try
        {
            return View("/Views/Components/MovieSummaryHurdle/Index.cshtml", summary);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading MovieSummaryHurdle.");
            return Content(string.Empty);
        }
    }
}