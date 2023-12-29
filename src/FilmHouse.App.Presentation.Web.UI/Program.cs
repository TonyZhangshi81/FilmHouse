using System.Configuration;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using AspNetCoreRateLimit;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Presentation.Web.Auth;
using FilmHouse.Core.Presentation.Web.Health;
using FilmHouse.Core.Presentation.Web.SecurityHeaders;
using FilmHouse.Core.Utils;
using FilmHouse.Data;
using FilmHouse.Data.MySql;
using FilmHouse.Data.PostgreSql;
using FilmHouse.Data.SqlServer;
using FilmHouse.Web;
using FilmHouse.Web.Configuration;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders;
using Microsoft.Net.Http.Headers;
using NLog.Web;
using Spectre.Console;
using Encoder = FilmHouse.Web.Configuration.Encoder;

Console.OutputEncoding = Encoding.UTF8;
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

// 创建WebApplicationBuilder
var builder = WebApplication.CreateBuilder(args);

// 获取数据库类型
var dbType = builder.Configuration.GetConnectionString("DatabaseType");
// 获取数据库连接字符串
var connStr = builder.Configuration.GetConnectionString("FilmHouseDatabase");
// 获取持久化密钥文件路径
var persistKeys = builder.Configuration["PersistKeysFile:path"];
// 获取 CultureInfo 列表
var cultures = new[] { "en-US", "zh-cn" }.Select(p => new CultureInfo(p)).ToList();

// 打印参数表
WriteParameterTable();
// 打印 GitHub 链接
AnsiConsole.MarkupLine("[link=https://github.com/TonyZhangshi81/FilmHouse]GitHub: TonyZhangshi81/FilmHouse[/]");

// 配置配置文件
ConfigureConfiguration();
// 配置服务
ConfigureServices(builder.Services);

// 创建WebApplication
var app = builder.Build();

// 执行第一次运行
await FirstRun();

// 配置中间件
ConfigureMiddleware();

// 获取应用程序的日志记录器
var logger = app.Services.GetRequiredService<ILogger<Program>>();
try
{
    // 获取应用程序的标识符
    var discriminator = app.Services.GetRequiredService<IOptions<DataProtectionOptions>>().Value.ApplicationDiscriminator;
    logger.LogInformation($"Application Discriminator: {discriminator}");
    logger.LogInformation("Starting application");

    // 启动应用程序
    app.Run();

    logger.LogInformation("Stopped application");
}
catch (Exception exception)
{
    // 记录应用程序终止时出现的异常
    logger.LogError(exception, "Application terminated unexpectedly");
}
finally
{
    // 关闭日志记录器
    NLog.LogManager.Shutdown();
}

void WriteParameterTable()
{
    // 获取应用程序版本
    var appVersion = Helper.AppVersion;
    // 创建一个表格，标题为FilmHouse.Web和.NET版本
    var table = new Spectre.Console.Table
    {
        Title = new($"FilmHouse.Web {appVersion} | .NET {Environment.Version}")
    };

    // 获取主机名
    var strHostName = Dns.GetHostName();
    // 获取主机信息
    var ipEntry = Dns.GetHostEntry(strHostName);
    // 获取IP地址列表
    var ips = ipEntry.AddressList;

    table.AddColumn("Parameter");
    table.AddColumn("Value");
    // 添加当前路径
    table.AddRow(new Markup("[blue]Path[/]"), new Text(Environment.CurrentDirectory));
    // 添加操作系统信息
    table.AddRow(new Markup("[blue]System[/]"), new Text(Helper.TryGetFullOSVersion()));
    // 添加当前用户信息
    table.AddRow(new Markup("[blue]User[/]"), new Text(Environment.UserName));
    // 添加主机名
    table.AddRow(new Markup("[blue]Host[/]"), new Text(Environment.MachineName));
    // 添加IP地址
    table.AddRow(new Markup("[blue]IP addresses[/]"), new Rows(ips.Select(p => new Text(p.ToString()))));
    // 添加数据库类型
    table.AddRow(new Markup("[blue]Database type[/]"), new Text(dbType!));
    // 添加图片存储
    table.AddRow(new Markup("[blue]Image storage[/]"), new Text(builder.Configuration["ImageStorage:Provider"]!));
    // 添加编辑器
    table.AddRow(new Markup("[blue]Editor[/]"), new Text(builder.Configuration["Editor"]!));
    // 添加种子最大重试可用性
    table.AddRow(new Markup("[blue]SeedMaxRetryAvailability[/]"), new Text(builder.Configuration["SeedMaxRetryAvailability"]!));

    AnsiConsole.Write(table);
}

void ConfigureConfiguration()
{
    // 创建一个新的配置构建器，并添加三个JSON文件
    IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddJsonFile("appsettings.Development.json")
        .AddJsonFile("health.option.json")
        .Build();
    // 将配置添加到服务中
    builder.Services.AddSingleton<IConfiguration>(configuration);
}
void ConfigureServices(IServiceCollection services)
{
    services.AddLogging(logging =>
    {
        logging.ClearProviders();
        //logging.SetMinimumLevel(LogLevel.Trace);
        //logging.AddConsole();
    });
    builder.Host.UseNLog();

    services.Configure<WebEncoderOptions>(options =>
    {
        // https://www.cnblogs.com/cdaniu/p/16024229.html
        options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
    });

    // 缓存管理
    // https://www.cnblogs.com/chenxi001/p/13363786.html
    services.AddMemoryCache();

    // https://www.cnblogs.com/chenxi001/p/13308860.html
    services.AddResponseCaching();

    // DI的批量登录设定
    var assemblies = StartupCore.GetAppAssemblies(true);
    services.AddLocalService(assemblies);

    // 添加MediatR服务
    services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
    // 添加选项服务，并添加HttpContextAccessor和RateLimit服务
    services.AddOptions()
            .AddHttpContextAccessor()
            .AddRateLimit(builder.Configuration.GetSection("IpRateLimiting"));

    // 添加分布式内存缓存服务
    services.AddDistributedMemoryCache();
    // 配置Session服务的选项
    services.AddSession(options =>
    {
        // 配置会话选项
        options.Cookie.IsEssential = true;
        // 设置会话的闲置超时时间为20分钟。如果在20分钟内没有任何活动，则会话将被视为过期并被清除。
        options.IdleTimeout = TimeSpan.FromMinutes(20);
        // 设置会话Cookie的HttpOnly属性为`true`。这意味着该Cookie只能通过HTTP协议进行传输，客户端脚本无法访问该Cookie。
        options.Cookie.HttpOnly = true;
    });

    // 添加数据保护服务
    services.AddDataProtection()
            // 将数据保护密钥持久化到文件系统
            .PersistKeysToFileSystem(new DirectoryInfo(persistKeys!))
            // 设置默认密钥有效期为15天
            .SetDefaultKeyLifetime(TimeSpan.FromDays(15))
            // 设置应用程序名称
            .SetApplicationName("filmhouse_web");

    // 配置Cookie策略选项
    services.Configure<CookiePolicyOptions>(options =>
    {
        // 设置为`true`表示要求用户在使用Cookie之前必须给予明确的同意。这通常是根据法律要求或隐私政策的规定来决定的。
        options.CheckConsentNeeded = context => true;
        // 设置为`None`表示允许跨站点请求时发送Cookie。这是为了兼容旧版浏览器或需要与其他域名进行交互的情况。
        options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
        // 设置为`SameAsRequest`表示Cookie的安全性与请求的安全性相同。如果请求是通过HTTPS进行的，则Cookie也会被标记为安全，只能通过HTTPS传输。
        options.Secure = CookieSecurePolicy.Always;
        // 设置为`None`表示允许客户端脚本访问和操作Cookie。如果设置为`HttpOnlyPolicy.Always`，则Cookie将被标记为仅限服务器访问，无法通过客户端脚本访问。
        options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
        // 指定用于跟踪用户是否已同意 cookie 使用策略
        options.ConsentCookieValue = "true";
        // 配置其他的 `CookieBuilder` 相关属性
        options.ConsentCookie = new CookieBuilder
        {
            // 设置过期间隔为3天
            Expiration = TimeSpan.FromDays(3),
            IsEssential = true,
            Name = "COOKIE_CONSENT"
        };
    });

    // 添加本地化资源路径
    services.AddLocalization(options => options.ResourcesPath = "Resources");
    // 添加控制器，并添加自动验证的AntiforgeryTokenFilter
    services.AddControllers(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    // 添加 Razor 页面，并添加数据注释本地化提供者
    services.AddRazorPages()
            .AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider = (_, factory) => factory.Create(typeof(Program)))
            .AddRazorPagesOptions(options =>
            {
                // 访问地址映射(/Admin/Post -> admin)
                //options.Conventions.AddPageRoute("/Admin/Post", "admin");
                // /Admin文件夹下的所有 Razor 页面都设置为需要授权才能访问
                //options.Conventions.AuthorizeFolder("/Admin");
            });

    // 添加健康检查UI
    services.AddHealthChecksUI()
            // 添加内存存储
            .AddInMemoryStorage();
    // 添加客户健康检查
    services.AddCustomerHealthChecks();

    // 添加电影厅认证
    services.AddFilmHouseAuthenticaton(builder.Configuration);

    // 添加一个单例服务，用于FilmHouseHtmlEncoder
    services.AddSingleton(Encoder.FilmHouseHtmlEncoder);

    // 添加反跨站请求伪造（CSRF）支持，以便FilmHouse可以与ASP.NET Core应用程序进行交互
    services.AddAntiforgery(options =>
    {
        // 定义一个常量，用于存储CSRF令牌的名称
        const string csrfName = "CSRF-TOKEN-FILMHOUSE";
        // 设置cookie名称
        options.Cookie.Name = $"X-{csrfName}";
        // 设置表单字段名称
        options.FormFieldName = $"{csrfName}-FORM";
        // 设置HTTP头名称
        options.HeaderName = "XSRF-FILMHOUSE-TOKEN";
    });

    // HttpContext.User认证状态取得的方法注入
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    // 配置数据库
    services.AddDataBaseSqlStorage(dbType, connStr);

    /*
    services.AddIdentity<UserEntity, UserRoleEntity>(opt =>
    {
        opt.Password.RequireDigit = false;
        opt.Password.RequireLowercase = false;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequiredLength = 6;
        opt.Password.RequiredUniqueChars = 1;
        opt.Lockout.MaxFailedAccessAttempts = 5;
        opt.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 5, 0);
        opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
        opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
    })
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<FilmHouseDbContext>();
    */

    // 添加服务提供者，以便服务可以注入到其他服务中
    services.AddScoped<IServiceProvider>(provider => provider.GetService<IServiceProvider>());
}

async Task FirstRun()
{
    try
    {
        var startUpResut = await app.InitStartUp(dbType);

        // 数据库连接测试失败
        if (startUpResut == StartupInitResult.DatabaseConnectionFail)
        {
            app.MapGet("/", () => Results.Problem(
                detail: "Database connection test failed, please check your connection string and firewall settings, then RESTART Moonglade manually.",
                statusCode: 500
                ));
            app.Run();
        }
        // 数据库初始化失败
        else if (startUpResut == StartupInitResult.DatabaseSetupFail)
        {
            app.MapGet("/", () => Results.Problem(
                detail: "Database setup failed, please check error log, then RESTART Moonglade manually.",
                statusCode: 500
            ));
            app.Run();
        }
        // 数据缓存处理失败
        else if (startUpResut == StartupInitResult.CodeDataCacheFail)
        {
            app.MapGet("/", () => Results.Problem(
                detail: "Data cache processing error,please check error log, then RESTART Moonglade manually.",
                statusCode: 500
            ));
            app.Run();
        }
    }
    catch (Exception e)
    {
        app.MapGet("/", _ => throw new("Start up failed:" + e.Message));
        app.Run();
    }
}

void ConfigureMiddleware()
{
    // 开发环境使用DeveloperExceptionPage中间件，显示错误信息
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        // 生产环境使用ExceptionHandler中间件，重定向到/error页面
        app.UseExceptionHandler(configure => configure.Run(async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null)
            {
                var exception = exceptionHandlerPathFeature.Error;
                switch (exception)
                {
                    default:
                        context.Response.StatusCode = 200;
                        context.Response.Redirect("/error", true);
                        break;
                }
            }

            await Task.CompletedTask;
        }));
    }

    // 处理转发的头信息（其中包括客戶端請求host名和客戶端請求協議類型）
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedProto
    });

    // 重定向到HTTPS
    app.UseHttpsRedirection();
    // 静态文件，支持缓存，支持自定义缓存头
    app.UseStaticFiles(new StaticFileOptions
    {
        // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/static-files?view=aspnetcore-7.0
        OnPrepareResponse = ctx =>
        {
            const int durationInSeconds = 24 * 60 * 60;
            ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + durationInSeconds;
        }
    });

    // 请求本地化，支持请求 Culture
    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new("zh-cn"),
        SupportedCultures = cultures,
        SupportedUICultures = cultures
    });

    // 健康检查，支持UI
    // address: https://localhost:7144/healthchecks-ui#/healthchecks
    app.UseHealthChecksUI();

    // Cookie策略
    app.UseCookiePolicy();
    app.UseSecurityHeaders(builder =>
    {
        // 允许所有
        builder.PermissionsPolicySettings.Camera.AllowNone();

        // 默认允许所有
        builder.CspSettings.Defaults.AllowNone();
        // 允许自身
        builder.CspSettings.Connect.AllowSelf();
        builder.CspSettings.Manifest.AllowSelf();
        builder.CspSettings.Objects.AllowNone();
        builder.CspSettings.Frame.AllowNone();
        builder.CspSettings.Scripts.AllowSelf();

        // 允许自身，允许内联unsafe
        builder.CspSettings.Styles
            .AllowSelf()
            .AllowUnsafeInline();

        // 允许自身
        builder.CspSettings.Fonts.AllowSelf();

        // 允许自身，允许i2.wp.com，允许www.gravatar.com
        builder.CspSettings.Images
            .AllowSelf()
            .Allow("https://i2.wp.com")
            .Allow("https://www.gravatar.com");

        // 允许所有
        builder.CspSettings.BaseUri.AllowNone();
        // 允许自身
        builder.CspSettings.FormAction.AllowSelf();
        // 允许所有
        builder.CspSettings.FrameAncestors.AllowNone();

        // 禁止引用
        builder.ReferrerPolicy = ReferrerPolicies.NoReferrerWhenDowngrade;
    });

    // 限制IP访问频率
    app.UseIpRateLimiting();
    // 路由中间件
    app.UseRouting();

    // 缓存中间件
    app.UseResponseCaching();

    // app.UseAuthentication会启用Authentication中间件
    // 认证中间件，在UseAuthentication方法之后注册的中间件才能够从HttpContext.User中读取到值
    app.UseAuthentication();
    // 根据当前Http请求中的Cookie信息来设置HttpContext.User属性，在app.UseAuthentication方法之后注册的中间件才能够从HttpContext.User中读取到值
    // 授权中间件
    app.UseAuthorization();

#pragma warning disable ASP0014
    // 配置端点，配置Endpoints.FilmHouseEndpoints
    app.UseEndpoints(ConfigureEndpoints.FilmHouseEndpoints);
#pragma warning restore ASP0014

}

