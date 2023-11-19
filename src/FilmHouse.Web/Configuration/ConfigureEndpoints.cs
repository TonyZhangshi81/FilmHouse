using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace FilmHouse.Web.Configuration;

public class ConfigureEndpoints
{
    public static Action<IEndpointRouteBuilder> FilmHouseEndpoints => endpoints =>
    {
        endpoints.MapRazorPages();

        endpoints.MapHealthChecks("/health", new HealthCheckOptions() { Predicate = _ => true, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });

        endpoints.MapHealthChecksUI();

        endpoints.MapControllerRoute(name: "default", pattern: "{controller=FilmHouse}/{action=Index}/{id?}");
    };

    /*
    private static Task WriteResponse(HttpContext context, HealthReport result)
    {
        var obj = new
        {
            Helper.AppVersion,
            DotNetVersion = Environment.Version.ToString(),
            EnvironmentTags = Helper.GetEnvironmentTags(),
            GeoMatch = context.Request.Headers["x-afd-geo-match"]
        };

        return context.Response.WriteAsJsonAsync(obj);
    }
    */
}