using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;

namespace FilmHouse.App.Presentation.Web.UI.Models.Components;

public class SubMenuLinkViewModel
{
    /// <summary>
    /// 子菜单项目集合
    /// </summary>
    public List<SubMenuViewModel> SubMenus { get; set; } = new List<SubMenuViewModel>();

    public class SubMenuViewModel
    {
        public CodeGroupVO Group { get; set; }
        public CodeKeyVO Code { get; set; }
        public CodeValueVO Name { get; set; }
        public SortOrderVO Order { get; set; }


        public static SubMenuViewModel FromEntity(CodeElement code)
        {
            var viewModel = new SubMenuViewModel();
            viewModel.Group = code.Group;
            viewModel.Code = code.Code;
            viewModel.Name = code.Name;
            viewModel.Order = code.Order;

            return viewModel;
        }
    }

}
