using FilmHouse.App.Presentation.Web.UI.Models.Components;
using FilmHouse.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Spectre.Console;

namespace FilmHouse.App.Presentation.Web.UI.ViewComponents;

public class MovieResourcesHurdleViewComponent : ViewComponent
{
    private readonly ILogger<MovieResourcesHurdleViewComponent> _logger;

    public MovieResourcesHurdleViewComponent(ILogger<MovieResourcesHurdleViewComponent> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="movieResources"></param>
    /// <param name="transfer"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(MovieResourcesHurdleViewModel movieResources, string transfer)
    {
        try
        {
            ViewBag.Transfer = transfer;
            return View("/Views/Components/MovieResourcesHurdle/Index.cshtml", movieResources);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading MovieResourcesHurdle.");
            return Content(string.Empty);
        }
    }
}