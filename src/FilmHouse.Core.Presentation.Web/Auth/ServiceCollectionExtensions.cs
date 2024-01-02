using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilmHouse.Core.Presentation.Web.Auth;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static IServiceCollection AddFilmHouseAuthenticaton(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("Authentication");
        var authentication = section.Get<AuthenticationSettings>();
        services.Configure<AuthenticationSettings>(section);

        switch (authentication.Provider)
        {
            /*
            case AuthenticationProvider.EntraID:
                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                }).AddMicrosoftIdentityWebApp(configuration.GetSection("Authentication:EntraID"));
                // Internally pass `null` to cookie options so there's no way to add `AccessDeniedPath` here.

                break;
            */
            case AuthenticationProvider.Local:
                services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                    {
                        options.AccessDeniedPath = "/auth/accessdenied";
                        options.LoginPath = "/Account/Login";
                        options.LogoutPath = "/Account/SignOut";

                        options.Events = new CookieAuthenticationEvents
                        {
                            // 用户未登录时的url重定向设定
                            OnRedirectToLogin = context =>
                            {
                                var uri = new UriBuilder
                                {
                                    Scheme = context.Request.Scheme,
                                    Host = context.Request.Host.Host,
                                    Port = GetForwardedPort(context),
                                    Path = options.LoginPath,
                                    // 保留当前的请求以备登录成功后使用
                                    Query = "returnurl=" + HttpUtility.UrlEncode($"{context.Request.Path}{context.Request.QueryString.ToString()}")
                                };

                                var portNumber1 = context.HttpContext.Request.Host.Port;
                                var bbb = context.HttpContext.Request.Headers["X-Forwarded-Port"].ToString();
                                var aaa = context.HttpContext.Connection.LocalPort;

                                context.Response.Redirect(uri.ToString());
                                return Task.CompletedTask;
                            },

                            // 用户登录状态注销时的url重定向设定
                            OnRedirectToLogout = context =>
                            {
                                var uri = new UriBuilder
                                {
                                    Scheme = context.Request.Scheme,
                                    Host = context.Request.Host.Host,
                                    Port = GetForwardedPort(context),
                                    Path = options.LogoutPath
                                };
                                context.Response.Redirect(uri.ToString());
                                return Task.CompletedTask;
                            }
                        };

                        var section = configuration.GetSection("Session");

                        options.SlidingExpiration = true;
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(section.GetValue<int>("Timeout"));
                        options.Cookie.Name = section.GetValue<string>("Cookie.Name");
                        options.Cookie.SameSite = (SameSiteMode)section.GetValue<int>("Cookie.SameSite");

                    });
                break;
            default:
                var msg = $"Provider {authentication.Provider} is not supported.";
                throw new NotSupportedException(msg);
        }

        // 取得原始请求的端口号（代理前）
        int GetForwardedPort(RedirectContext<CookieAuthenticationOptions> context)
        {
            var post = 80;
            // 使用代理的情况下，
            if (context.HttpContext.Request.Headers.ContainsKey("X-Forwarded-Port"))
            {
                var forwardedPort = context.HttpContext.Request.Headers["X-Forwarded-Port"].FirstOrDefault();
                if (!string.IsNullOrEmpty(forwardedPort))
                {
                    post = Convert.ToInt32(forwardedPort);
                }
            }
            else
            {
                // 未使用代理的情况
                post = context.Request.Host.Port.GetValueOrDefault(80);
            }
            return post;
        }

        return services;
    }
}