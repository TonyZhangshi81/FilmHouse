using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FilmHouse.Core.Presentation.Web.Filters;

namespace FilmHouse.App.Presentation.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        #region Initizalize

        private readonly IMediator _mediator;
        private readonly ISettingProvider _settingProvider;
        private readonly ICurrentRequestId _currentRequestId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public AccountController(IMediator mediator, ISettingProvider settingProvider, ICurrentRequestId currentRequestId)
        {
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this._settingProvider = settingProvider ?? throw new ArgumentNullException(nameof(settingProvider));
            this._currentRequestId = currentRequestId ?? throw new ArgumentNullException(nameof(currentRequestId));
        }

        #endregion Initizalize

        #region 登录
        //
        // GET: /Account/Login/
        [AllowAnonymous]
        [LogonFilter]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Mine");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login/
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnurl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ModelState.AddModelError("", "请检查用户名或密码是否正确。");
            return View(model);

            /*
            var result = AccountManager.PasswordSignIn(model.Account, model.Password);
            switch (result)
            {
                case SignInStatus.Success:
                    if (AccountManager.IsAdmin(AccountManager.GetId(model.Account)))
                    {
                        return RedirectToAction("Index", "Movie", new { Area = "Manage" });
                    }
                    else
                    {
                        return RedirectToLocal(returnurl);
                    }
                case SignInStatus.UndefinedAccount:
                    ModelState.AddModelError("", "用户名不存在。");
                    return View(model);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "请检查用户名或密码是否正确。");
                    return View(model);
            }
            */
        }
        #endregion
    }
}
