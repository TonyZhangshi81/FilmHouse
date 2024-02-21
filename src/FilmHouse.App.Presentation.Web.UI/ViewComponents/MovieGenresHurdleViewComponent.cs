using FilmHouse.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Spectre.Console;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmHouse.App.Presentation.Web.UI.ViewComponents;

public class MovieGenresHurdleViewComponent : ViewComponent
{
    private readonly ILogger<MovieGenresHurdleViewComponent> _logger;

    public MovieGenresHurdleViewComponent(ILogger<MovieGenresHurdleViewComponent> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="genres"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(List<SelectListItem> genres, int count = 0)
    {
        try
        {
            var list = genres;
            if (count != 0)
            {
                list = genres.Take(count).ToList();
            }

            return View("/Views/Components/MovieGenresHurdle/Index.cshtml", list);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading MovieGenresHurdle.");
            return Content(string.Empty);
        }
    }
}