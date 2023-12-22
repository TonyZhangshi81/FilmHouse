using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FilmHouse.Core.Presentation.Web.Filters;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Commands.Account;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using FilmHouse.Core.Utils;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using FilmHouse.Core.Presentation.Web.Auth;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.Features;

namespace FilmHouse.App.Presentation.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        #region Initizalize

        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        private readonly AuthenticationSettings _authenticationSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="settingProvider"></param>
        /// <param name="currentRequestId"></param>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AccountController(IMediator mediator, ISettingProvider settingProvider, ICurrentRequestId currentRequestId, ILogger<AccountController> logger, IOptions<AuthenticationSettings> authSettings)
        {
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this._logger = logger;

            this._authenticationSettings = authSettings.Value;
        }

        #endregion Initizalize

        #region 登录
        //
        // GET: /Account/Login/
        [HttpGet]
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
        public async Task<IActionResult> Login(LoginViewModel model, string returnurl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var command = new PasswordSignInCommand(model.Account, model.Password);
                var result = await _mediator.Send(command);

                switch (result.Status)
                {
                    case Commands.Account.SignInStatus.Success:
                        await this.SetClaimsIdentity(model.Account, result.UserId, result.IsAdmin);
                        await this._mediator.Send(new ValidateLoginCommand(result.UserId, new(Helper.GetClientIP(HttpContext))));

                        this._logger.LogInformation($@"Authentication success for local account ""{model.Account}""");

                        if (result.IsAdmin.AsPrimitive())
                        {
                            return RedirectToAction("Index", "Movie", new { Area = "Manage" });
                        }
                        return RedirectToLocal(returnurl);

                    case Commands.Account.SignInStatus.UndefinedAccount:
                        ModelState.AddModelError("", "用户名不存在。");
                        return View(model);

                    case Commands.Account.SignInStatus.Failure:
                    default:
                        ModelState.AddModelError(string.Empty, "请检查用户名或密码是否正确。");
                        return View(model);
                }
            }
            catch (Exception e)
            {
                this._logger.LogError(e, $@"Authentication failed for local account ""{model.Account}""");

                ModelState.AddModelError(string.Empty, "用户验证失败。");
                return View(model);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="userId"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        private async Task SetClaimsIdentity(AccountNameVO account, UserIdVO userId, IsAdminVO isAdmin)
        {
            // 创建Claim类型的数组，将登录用户的所有信息（比如用户名）存储在Claim类型的字符串键值对中
            var claims = new List<Claim>
                    {
                        new (ClaimTypes.Name, account.AsPrimitive(), typeof(AccountNameVO).Name),
                        new (ClaimTypes.Role, (isAdmin.AsPrimitive() ? "Administrator" : "Guest")),
                        new ("uid", userId.AsPrimitive().ToString())
                    };

            // 将上面创建的Claim类型的数组传入ClaimsIdentity中，用来构造一个ClaimsIdentity对象
            var ci = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // 将上面创建的ClaimsIdentity对象传入ClaimsPrincipal中，用来构造一个ClaimsPrincipal对象
            var p = new ClaimsPrincipal(ci);

            // 对用户进行身份验证并在成功后进行登录。使用Cookie身份验证方案。
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, p);
        }

        #endregion

        #region 注销

        //
        // POST: /Account/SignOut/
        [HttpGet]
        public async Task<IActionResult> SignOut(string returnUrl)
        {
            switch (this._authenticationSettings.Provider)
            {
                case AuthenticationProvider.EntraID:
                    /*
                    var callbackUrl = Url.Page("/Index", null, null, Request.Scheme);
                    return SignOut(
                        new AuthenticationProperties { RedirectUri = callbackUrl },
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        OpenIdConnectDefaults.AuthenticationScheme);
                    */
                    break;
                case AuthenticationProvider.Local:
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    break;
                default:
                    break;
            }
            var consentFeature = HttpContext.Features.Get<ITrackingConsentFeature>();
            if (consentFeature != null)
            {
                // 设置TrackingConsentFeature的同意状态为未同意
                consentFeature.WithdrawConsent();
            }

            return RedirectToLocal(returnUrl);
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/grantcookie")]
        public bool GrantCookie()
        {
            var consentFeature = HttpContext.Features.Get<ITrackingConsentFeature>();
            if (consentFeature != null)
            {
                // 设置TrackingConsentFeature的同意状态为同意
                consentFeature.GrantConsent();
            }
            return true;
        }




        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (!Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl) && !string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
