using AspNetCoreRateLimit;
using FilmHouse.Data.PostgreSql;
using FilmHouse.Mvc.Health;
using FilmHouse.Mvc.SecurityHeaders;
using FilmHouse.Utils;
using FilmHouse.Utils.PasswordGenerator;
using FilmHouse.Web;
using FilmHouse.Web.Configuration;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using NLog.Web;
using Spectre.Console;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Encoder = FilmHouse.Web.Configuration.Encoder;

Console.OutputEncoding = Encoding.UTF8;
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

var dbType = builder.Configuration.GetConnectionString("DatabaseType");
var connStr = builder.Configuration.GetConnectionString("FilmHouseDatabase");
var persistKeys = builder.Configuration["PersistKeysFile:path"];
var cultures = new[] { "en-US", "zh-cn" }.Select(p => new CultureInfo(p)).ToList();

WriteParameterTable();
AnsiConsole.MarkupLine("[link=https://github.com/TonyZhangshi81/FilmHouse]GitHub: TonyZhangshi81/FilmHouse[/]");

ConfigureConfiguration();
ConfigureServices(builder.Services);

var app = builder.Build();

await FirstRun();

ConfigureMiddleware();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
try
{
    var discriminator = app.Services.GetRequiredService<IOptions<DataProtectionOptions>>().Value.ApplicationDiscriminator;
    logger.LogInformation($"Application Discriminator: {discriminator}");
    logger.LogInformation("Starting application");

    app.Run();

    logger.LogInformation("Stopped application");
}
catch (Exception exception)
{
    logger.LogError(exception, "Application terminated unexpectedly");
}
finally
{
    NLog.LogManager.Shutdown();
}

void WriteParameterTable()
{
    var appVersion = Helper.AppVersion;
    var table = new Spectre.Console.Table
    {
        Title = new($"FilmHouse.Web {appVersion} | .NET {Environment.Version}")
    };

    var strHostName = Dns.GetHostName();
    var ipEntry = Dns.GetHostEntry(strHostName);
    var ips = ipEntry.AddressList;

    table.AddColumn("Parameter");
    table.AddColumn("Value");
    table.AddRow(new Markup("[blue]Path[/]"), new Text(Environment.CurrentDirectory));
    table.AddRow(new Markup("[blue]System[/]"), new Text(Helper.TryGetFullOSVersion()));
    table.AddRow(new Markup("[blue]User[/]"), new Text(Environment.UserName));
    table.AddRow(new Markup("[blue]Host[/]"), new Text(Environment.MachineName));
    table.AddRow(new Markup("[blue]IP addresses[/]"), new Rows(ips.Select(p => new Text(p.ToString()))));
    table.AddRow(new Markup("[blue]Database type[/]"), new Text(dbType!));
    table.AddRow(new Markup("[blue]Image storage[/]"), new Text(builder.Configuration["ImageStorage:Provider"]!));
    table.AddRow(new Markup("[blue]Editor[/]"), new Text(builder.Configuration["Editor"]!));

    AnsiConsole.Write(table);
}

void ConfigureConfiguration()
{
}

void ConfigureServices(IServiceCollection services)
{
    services.AddLogging(logging =>
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Trace);
        logging.AddConsole();
    });
    builder.Host.UseNLog();

    AppDomain.CurrentDomain.Load("FilmHouse.Business");

    services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
    services.AddOptions()
            .AddHttpContextAccessor()
            .AddRateLimit(builder.Configuration.GetSection("IpRateLimiting"));

    services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(persistKeys!))
            .SetDefaultKeyLifetime(TimeSpan.FromDays(15))
            .SetApplicationName("filmhouse_web");

    services.Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
        options.Secure = CookieSecurePolicy.SameAsRequest;
        options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.None;
    });

    services.Configure<RequestLocalizationOptions>(options =>
    {
        options.DefaultRequestCulture = new("en-US");
        options.SupportedCultures = cultures;
        options.SupportedUICultures = cultures;
    });

    services.AddLocalization(options => options.ResourcesPath = "Resources");
    services.AddControllers(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    services.AddRazorPages()
            .AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider = (_, factory) => factory.Create(typeof(Program)))
            .AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Admin/Post", "admin");
                options.Conventions.AuthorizeFolder("/Admin");
                options.Conventions.AuthorizeFolder("/Settings");
            });

    services.AddCustomerHealthChecks(builder.Configuration);

    // Fix Chinese character being encoded in HTML output
    services.AddSingleton(Encoder.FilmHouseHtmlEncoder);

    services.AddAntiforgery(options =>
    {
        const string csrfName = "CSRF-TOKEN-FILMHOUSE";
        options.Cookie.Name = $"X-{csrfName}";
        options.FormFieldName = $"{csrfName}-FORM";
        options.HeaderName = "XSRF-TOKEN";
    });

    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    services.AddTransient<IPasswordGenerator, DefaultPasswordGenerator>();

    services.AddPostgreSqlStorage(connStr!);

}

async Task FirstRun()
{
    try
    {
        var startUpResut = await app.InitStartUp(dbType);

        if (startUpResut == StartupInitResult.DatabaseConnectionFail)
        {
            app.MapGet("/", () => Results.Problem(
                detail: "Database connection test failed, please check your connection string and firewall settings, then RESTART FilmHouse manually.",
                statusCode: 500
                ));
            app.Run();
        }
        else if (startUpResut == StartupInitResult.DatabaseSetupFail)
        {
            app.MapGet("/", () => Results.Problem(
                detail: "Database setup failed, please check error log, then RESTART FilmHouse manually.",
                statusCode: 500
            ));
            app.Run();
        }
    }
    catch (Exception e)
    {
        app.MapGet("/", _ => throw new("Start up failed: " + e.Message));
        app.Run();
    }
}

void ConfigureMiddleware()
{
    /*
    if (!app.Environment.IsProduction())
    {
        app.Logger.LogWarning($"Running in environment: {app.Environment.EnvironmentName}. Application Insights disabled.");

        var tc = app.Services.GetRequiredService<TelemetryConfiguration>();
        tc.DisableTelemetry = true;
        TelemetryDebugWriter.IsTracingDisabled = true;
    }
    */

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseStatusCodePages(ConfigureStatusCodePages.Handler).UseExceptionHandler("/error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            const int durationInSeconds = 24 * 60 * 60; // 24 hours
            ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + durationInSeconds;
        }
    });
    app.UseCookiePolicy();
    app.UseSecurityHeaders(builder =>
    {
        builder.PermissionsPolicySettings.Camera.AllowNone();

        builder.CspSettings.Defaults.AllowNone();
        builder.CspSettings.Connect.AllowSelf();
        builder.CspSettings.Manifest.AllowSelf();
        builder.CspSettings.Objects.AllowNone();
        builder.CspSettings.Frame.AllowNone();
        builder.CspSettings.Scripts.AllowSelf();

        builder.CspSettings.Styles
            .AllowSelf()
            .AllowUnsafeInline();

        builder.CspSettings.Fonts.AllowSelf();

        builder.CspSettings.Images
            .AllowSelf()
            .Allow("https://i2.wp.com")
            .Allow("https://www.gravatar.com");

        builder.CspSettings.BaseUri.AllowNone();
        builder.CspSettings.FormAction.AllowSelf();
        builder.CspSettings.FrameAncestors.AllowNone();

        builder.ReferrerPolicy = ReferrerPolicies.NoReferrerWhenDowngrade;
    });
    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new("en-US"),
        SupportedCultures = cultures,
        SupportedUICultures = cultures
    });
    app.UseIpRateLimiting();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

#pragma warning disable ASP0014
    app.UseEndpoints(ConfigureEndpoints.FilmHouseEndpoints);
#pragma warning restore ASP0014

}