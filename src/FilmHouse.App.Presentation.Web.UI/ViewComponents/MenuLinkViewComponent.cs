using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.ViewComponents;

public class MenuLinkViewComponent : ViewComponent
{
    private readonly ILogger<MenuLinkViewComponent> _logger;

    public MenuLinkViewComponent(ILogger<MenuLinkViewComponent> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="navType"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(int navType)
    {
        try
        {
            ViewBag.NavType = navType;
            return View("/Views/Components/MenuLink/Index.cshtml");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading MenuLink.");
            return Content(string.Empty);
        }
    }
}