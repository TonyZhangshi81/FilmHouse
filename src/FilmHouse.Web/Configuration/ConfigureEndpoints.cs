using System.Text.Json;
using System.Text;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Abstractions;

namespace FilmHouse.Web.Configuration;

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

        endpoints.MapControllerRoute(name: "default", pattern: "{controller=FilmHouse}/{action=Index}/{id?}");
    };

    /*
     * !
     * Suspend the use of custom detection report information output, Using UIResponseWriter. 
     * WriteHealthCheckUIResponse as UseHealthChecks report format standard output with UseHealthChecksUI testing information to display the page
     * 
    private static Task WriteResponse(HttpContext context, HealthReport healthReport)
    {
        context.Response.ContentType = "application/json; charset=utf-8";

        var options = new JsonWriterOptions { Indented = true };

        using var memoryStream = new MemoryStream();
        using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
        {
            jsonWriter.WriteStartObject();
            jsonWriter.WriteString("status", healthReport.Status.ToString());
            jsonWriter.WriteStartObject("results");

            foreach (var healthReportEntry in healthReport.Entries)
            {
                jsonWriter.WriteStartObject(healthReportEntry.Key);
                jsonWriter.WriteString("status",
                    healthReportEntry.Value.Status.ToString());
                jsonWriter.WriteString("description",
                    healthReportEntry.Value.Description);
                jsonWriter.WriteStartObject("data");

                foreach (var item in healthReportEntry.Value.Data)
                {
                    jsonWriter.WritePropertyName(item.Key);

                    JsonSerializer.Serialize(jsonWriter, item.Value,
                        item.Value?.GetType() ?? typeof(object));
                }

                jsonWriter.WriteEndObject();
                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndObject();
            jsonWriter.WriteEndObject();
        }

        return context.Response.WriteAsync(
            Encoding.UTF8.GetString(memoryStream.ToArray()));
    }
    */
}