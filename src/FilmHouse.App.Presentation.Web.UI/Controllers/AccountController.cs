using System.Security.Claims;
using System.Web;
using FilmHouse.App.Presentation.Web.UI.Models;
using FilmHouse.Commands.Account;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Presentation.Web.Auth;
using FilmHouse.Core.Presentation.Web.Filters;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FilmHouse.App.Presentation.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        #region Initizalize

        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;
        private readonly ICurrentRequestId _currentRequestId;

        private readonly AuthenticationSettings _authenticationSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        /// <param name="authSettings"></param>
        /// <param name="currentRequestId"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AccountController(IMediator mediator, ILogger<AccountController> logger, IOptions<AuthenticationSettings> authSettings, ICurrentRequestId currentRequestId)
        {
            this._logger = Guard.GetNotNull(logger, nameof(ILogger<AccountController>));
            this._mediator = Guard.GetNotNull(mediator, nameof(IMediator));
            this._currentRequestId = Guard.GetNotNull(currentRequestId, nameof(ICurrentRequestId));

            var auSetting = Guard.GetNotNull(authSettings, nameof(IOptions<AuthenticationSettings>));
            this._authenticationSettings = auSetting.Value;
        }

        #endregion Initizalize

        #region 登录
        //
        // GET: /Account/Login/
        [HttpGet]
        [AllowAnonymous]
        [LogonFilter]
        public ActionResult Login(string returnurl)
        {
            if (base.User.Identity.IsAuthenticated)
            {
                return base.RedirectToAction("Index", "Mine");
            }
            base.ViewBag.Transfer = !string.IsNullOrEmpty(returnurl) ? HttpUtility.UrlDecode(returnurl) : "/";
            return base.View();
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
                if (!base.ModelState.IsValid)
                {
                    return base.View(model);
                }

                var command = new PasswordSignInCommand(model.Account, model.Password);
                var result = await this._mediator.Send(command);

                switch (result.Status)
                {
                    case Commands.Account.SignInStatus.Success:
                        await this.SetClaimsIdentity(model.Account, result.UserId, result.IsAdmin);
                        await this._mediator.Send(new ValidateLoginCommand(result.UserId, new(Helper.GetClientIP(base.HttpContext))));

                        this._logger.LogInformation($@"Authentication success for local account ""{model.Account}""");

                        if (result.IsAdmin.AsPrimitive())
                        {
                            return base.RedirectToAction("Index", "Movie", new { Area = "Manage" });
                        }

                        return this.RedirectToLocal(returnurl);

                    case Commands.Account.SignInStatus.UndefinedAccount:
                        base.ModelState.AddModelError("", "用户名不存在。");
                        return base.View(model);

                    case Commands.Account.SignInStatus.Failure:
                    default:
                        base.ModelState.AddModelError(string.Empty, "请检查用户名或密码是否正确。");
                        return base.View(model);
                }
            }
            catch (Exception e)
            {
                this._logger.LogError(e, $@"Authentication failed for local account ""{model.Account}""");

                base.ModelState.AddModelError(string.Empty, "用户验证失败。");
                return base.View(model);
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
            await base.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, p);
        }

        private ActionResult RedirectToLocal(string transfer)
        {
            if (!string.IsNullOrEmpty(transfer) && !string.IsNullOrWhiteSpace(transfer)) // !base.Url.IsLocalUrl(transfer) && 
            {
                return base.Redirect(transfer);
            }
            return base.RedirectToAction("Index", "Home");
        }

        #endregion

        #region 注销

        //
        // POST: /Account/SignOut/
        [HttpGet]
        public async Task<IActionResult> SignOut(string transfer)
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
                    await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    this.HttpContext.Request.Cookies.Select(_ => _.Key)
                                                    .Where(_ => _.EndsWith("-FILMHOUSE"))
                                                    .ToList()
                                                    .ForEach(_ => this.HttpContext.Response.Cookies.Delete(_));
                    break;
                default:
                    break;
            }
            var consentFeature = base.HttpContext.Features.Get<ITrackingConsentFeature>();
            if (consentFeature != null)
            {
                // 设置TrackingConsentFeature的同意状态为未同意
                consentFeature.WithdrawConsent();
            }

            return this.RedirectToLocal(transfer);
        }

        #endregion

        #region 注册

        //
        // GET: /Account/Register/
        [AllowAnonymous]
        public ActionResult Register(string transfer)
        {
            ViewBag.Transfer = transfer;
            return View();
        }

        //
        // POST: /Account/Register/
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string transfer)
        {
            if (ModelState.IsValid)
            {
                // 创建请求ID
                this._currentRequestId.Set(new RequestIdVO(Guid.NewGuid()));

                var clientIP = new LastLoginIpVO(Helper.GetClientIP(base.HttpContext));
                var command = new CreateAccountCommand(model.Account, model.Password, clientIP);
                var result = await this._mediator.Send(command);
                if (result.Status == CreateStatus.Success)
                {
                    await this.SetClaimsIdentity(model.Account, result.UserId, result.IsAdmin);
                    await this._mediator.Send(new ValidateLoginCommand(result.UserId, clientIP));

                    this._logger.LogInformation($@"Authentication success for local account ""{model.Account}""");

                    if (result.IsAdmin.AsPrimitive())
                    {
                        return RedirectToAction("Index", "ManageMovie");
                    }
                    else
                    {
                        return RedirectToLocal(transfer);
                    }
                }
                ModelState.AddModelError("", "用户名已存在");
            }

            return View(model);
        }

        #endregion

        #region 重置密码
        //
        // GET: /Account/ResetPassword/
        [AllowAnonymous]
        //[ResetPasswordFilter]
        public ActionResult ResetPassword()
        {
            return View();
        }

        //
        // POST: /Account/ResetPassword/
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            /*
            model.Account = Session["ResetAccount"].ToString();
            var result = AccountManager.ResetPassword(model.Account, model.Password);
            if (result.Succeeded)
            {
                Session["CanReset"] = false;
                return RedirectToAction("Login", "Account");
            }
            ModelState.AddModelError("", "重置密码失败，请重试。");
            */
            return View();
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
            var consentFeature = base.HttpContext.Features.Get<ITrackingConsentFeature>();
            if (consentFeature != null)
            {
                // 设置TrackingConsentFeature的同意状态为同意
                consentFeature.GrantConsent();
            }
            return true;
        }






    }
}
