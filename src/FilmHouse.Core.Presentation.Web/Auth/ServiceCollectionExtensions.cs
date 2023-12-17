using Microsoft.AspNetCore.Authentication.Cookies;
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
                    });
                break;
            default:
                var msg = $"Provider {authentication.Provider} is not supported.";
                throw new NotSupportedException(msg);
        }

        return services;
    }
}