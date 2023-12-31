using FilmHouse.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.ViewComponents;

public class SummaryHurdleViewComponent : ViewComponent
{
    private readonly ILogger<SummaryHurdleViewComponent> _logger;

    public SummaryHurdleViewComponent(ILogger<SummaryHurdleViewComponent> logger)
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
            return View("/Views/Components/SummaryHurdle/Index.cshtml", summary);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading SummaryHurdle.");
            return Content(string.Empty);
        }
    }
}