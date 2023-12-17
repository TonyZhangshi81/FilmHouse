using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.ViewComponents;

public class SubMenuLinkViewComponent : ViewComponent
{
    private readonly ILogger<SubMenuLinkViewComponent> _logger;

    public SubMenuLinkViewComponent(ILogger<SubMenuLinkViewComponent> logger)
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
            return View("/Views/Components/SubMenuLink/Index.cshtml");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading SubMenuLink.");
            return Content(string.Empty);
        }
    }
}