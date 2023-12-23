﻿using FilmHouse.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Spectre.Console;

namespace FilmHouse.App.Presentation.Web.UI.ViewComponents;

public class MovieLanguagesHurdleViewComponent : ViewComponent
{
    private readonly ILogger<MovieLanguagesHurdleViewComponent> _logger;

    public MovieLanguagesHurdleViewComponent(ILogger<MovieLanguagesHurdleViewComponent> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="genres"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(List<CodeValueVO> genres, int count = 0)
    {
        try
        {
            var list = genres;
            if (count != 0)
            {
                list = genres.Take(count).ToList();
            }

            return View("/Views/Components/MovieLanguagesHurdle/Index.cshtml", list);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading MovieLanguagesHurdle.");
            return Content(string.Empty);
        }
    }
}