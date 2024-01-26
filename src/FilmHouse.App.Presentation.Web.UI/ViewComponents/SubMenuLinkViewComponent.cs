using FilmHouse.App.Presentation.Web.UI.Models.Components;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using static FilmHouse.App.Presentation.Web.UI.Models.Components.SubMenuLinkViewModel;

namespace FilmHouse.App.Presentation.Web.UI.ViewComponents;

public class SubMenuLinkViewComponent : ViewComponent
{
    private readonly ILogger<SubMenuLinkViewComponent> _logger;
    private readonly ICodeProvider _codeProvider;

    public SubMenuLinkViewComponent(ILogger<SubMenuLinkViewComponent> logger, ICodeProvider codeProvider)
    {
        _logger = logger;
        _codeProvider = codeProvider;
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
            var viewModel = new SubMenuLinkViewModel();

            // 國家地區
            var elements = this._codeProvider.AvailableAt(CodeGroupVO.Codes.Country).Elements.OrderBy(d => d.Order).ToList().Take(5);
            foreach (var item in elements)
            {
                viewModel.SubMenus.Add(SubMenuViewModel.FromEntity(item));
            }

            // 類型
            elements = this._codeProvider.AvailableAt(CodeGroupVO.Codes.MovieGenre).Elements.OrderBy(d => d.Order).ToList().Take(5);
            foreach (var item in elements)
            {
                viewModel.SubMenus.Add(SubMenuViewModel.FromEntity(item));
            }

            return View("/Views/Components/SubMenuLink/Index.cshtml", viewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading SubMenuLink.");
            return Content(string.Empty);
        }
    }
}