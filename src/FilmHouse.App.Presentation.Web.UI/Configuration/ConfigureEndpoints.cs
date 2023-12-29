using System.Text.Json;
using System.Text;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Abstractions;

namespace FilmHouse.App.Presentation.Web.UI.Configuration;

public class ConfigureEndpoints
{
    public static Action<IEndpointRouteBuilder> FilmHouseEndpoints => endpoints =>
    {
        //endpoints.MapRazorPages();

        endpoints.MapHealthChecks("/health", new HealthCheckOptions()
        {
            Predicate = healthCheck => healthCheck.Tags.Contains("ready"),
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            },
            AllowCachingResponses = true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        // !Try sending the results to your Slack or Teams workspace
        // https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks/blob/master/doc/webhooks.md

        endpoints.MapHealthChecksUI(setup =>
        {
            // can use custom style libraries
            // https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks/blob/master/doc/styles-branding.md
            //setup.AddCustomStylesheet(@"wwwroot\css\dotnet.css");
        });

        endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
    };
}