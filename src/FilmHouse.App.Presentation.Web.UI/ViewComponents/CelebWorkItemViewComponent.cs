using FilmHouse.App.Presentation.Web.UI.Models.Components;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FilmHouse.App.Presentation.Web.UI.ViewComponents;

public class CelebWorkItemViewComponent : ViewComponent
{
    private readonly ILogger<CelebWorkItemViewComponent> _logger;
    private readonly ICodeProvider _codeProvider;
    private readonly ISettingProvider _settingProvider;

    public CelebWorkItemViewComponent(ILogger<CelebWorkItemViewComponent> logger, ISettingProvider settingProvider, ICodeProvider codeProvider)
    {
        this._logger = Guard.GetNotNull(logger, nameof(ILogger<CelebWorkItemViewComponent>));
        this._settingProvider = Guard.GetNotNull(settingProvider, nameof(ISettingProvider));
        this._codeProvider = Guard.GetNotNull(codeProvider, nameof(ICodeProvider));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="movie"></param>
    /// <param name="celebrityId"></param>
    /// <returns></returns>
    public IViewComponentResult Invoke(MovieEntity movie, CelebrityIdVO celebrityId)
    {
        try
        {
            var model = CelebWorkItemViewModel.FromEntity(movie, celebrityId);
            // 电影类型
            model.GenresValue = model.Genres.AsCodeElement(this._codeProvider, GenresVO.Group).Select(_ => _.Name).ToList();
            // 电影卡片明星显示最大个数
            ViewBag.TakeCount = this._settingProvider.GetValue(ConfigKeyVO.Keys.WorkItemCelebMax.AsPrimitive()).CastTo<int>();

            return View("/Views/Components/CelebWorkItem/Index.cshtml", model);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Reading CelebWorkItem.");
            return Content(string.Empty);
        }
    }
}