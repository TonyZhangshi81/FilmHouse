using System.Text;
using System.Text.Json.Serialization;
using FilmHouse.App.Presentation.Web.Api;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Core.Presentation.Web.DependencyInjection;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Services.MongoBasicOperation;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects.Serialization.Generics;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Infrastructure.Services.Codes;
using FilmHouse.Data.Infrastructure.Services.Configuration;
using FilmHouse.Data.MySql;
using FilmHouse.Data.PostgreSql;
using FilmHouse.Data.SqlServer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NLog.Web;

Console.OutputEncoding = Encoding.UTF8;
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);
// 获取数据库类型
var dbType = builder.Configuration.GetConnectionString("DatabaseType");
// 获取数据库连接字符串
var connStr = builder.Configuration.GetConnectionString("FilmHouseDatabase");

// 配置配置文件
ConfigureConfiguration();

// 配置服务
ConfigureServices(builder.Services);

var app = builder.Build();

// 通用信息预加载缓存处理
InitSetMemoryCache(app, dbType);

// 配置中间件
ConfigureMiddleware();

// 获取应用程序的日志记录器
var logger = app.Services.GetRequiredService<ILogger<Program>>();
try
{
    // 获取应用程序的标识符
    var discriminator = app.Services.GetRequiredService<IOptions<DataProtectionOptions>>().Value.ApplicationDiscriminator;
    logger.LogInformation($"web api discriminator: {discriminator}");
    logger.LogInformation("starting web api");

    // 启动应用程序
    app.Run();

    logger.LogInformation("stopped web api");
}
catch (Exception exception)
{
    // 记录应用程序终止时出现的异常
    logger.LogError(exception, "web api terminated unexpectedly");
}
finally
{
    // 关闭日志记录器
    NLog.LogManager.Shutdown();
}

void ConfigureConfiguration()
{
    // 创建一个新的配置构建器，并添加三个JSON文件
    IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddJsonFile("appsettings.Development.json")
        .Build();
    // 将配置添加到服务中
    builder.Services.AddSingleton<IConfiguration>(configuration);

    // MongodbHost信息
    builder.Services.Configure<MongoDBContextOptions>(builder.Configuration.GetSection("MongodbHost"));
}

void ConfigureServices(IServiceCollection services)
{
    services.AddLogging(logging =>
    {
        logging.ClearProviders();
    });
    builder.Host.UseNLog();

    // .net core 启动反向代理支持（为了能够获取到由代理服务器传递的原始请求信息）
    services.Configure<ForwardedHeadersOptions>(options =>
    {
        // 处理转发的头信息（其中包括客戶端請求host名和客戶端請求協議類型）
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        options.KnownNetworks.Clear();
        options.KnownProxies.Clear();
    });

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

    // 缓存管理
    services.AddMemoryCache();

    // DI的批量登录设定
    var assemblies = StartupCore.GetAppAssemblies(true);
    services.AddLocalService(assemblies);

    // 添加MediatR服务
    services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

    // 配置数据库
    AddDataBaseSqlStorage(services, dbType, connStr);

    // WebAPI
    builder.Services.AddControllers()
                    .ConfigureApiBehaviorOptions(options =>
                     {
                         // 禁用模型状态无效筛选器，即不会自动返回模型验证错误信息的HTTP响应。
                         options.SuppressModelStateInvalidFilter = true;
                     })
                    .AddMvcOptions(options =>
                    {
                        options.Filters.AddFilters(services);
                    })
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                        options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
                    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}



void AddDataBaseSqlStorage(IServiceCollection services, string? dbType, string? connStr)
{
    Guard.GetNotNull(dbType, nameof(String));
    Guard.GetNotNull(connStr, nameof(String));

    switch (dbType!.ToLower())
    {
        case "mysql":
            services.AddMySqlStorage(connStr!);
            break;
        case "postgresql":
            services.AddPostgreSqlStorage(connStr!);
            break;
        case "sqlserver":
        default:
            services.AddSqlServerStorage(connStr!);
            break;
    }
}

void ConfigureMiddleware()
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger(c =>
        {
            c.PreSerializeFilters.Add((swagger, httpReq) =>
            {
                // 根据访问地址，设置swagger服务路径(以应对nginx反向代理)
                swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/{httpReq.Headers["X-Forwarded-Prefix"]}" } };
            });
        });
        app.UseSwaggerUI();
    }

    // 应用反向代理规则
    app.UseForwardedHeaders();

    app.UseHttpsRedirection();

    // 缓存中间件
    app.UseResponseCaching();

    app.UseAuthorization();
    app.MapControllers();
}


void InitSetMemoryCache(WebApplication app, string? dbType)
{
    Guard.GetNotNull(dbType, nameof(String));

    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    try
    {
        app.Logger.LogInformation("通用代码信息数据获取...");

        var memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();

        var codeMast = serviceProvider.GetRequiredService<IRepository<CodeMastEntity>>();
        var codeCaher = (ICodeProviderCacher)new CodeProvider(codeMast, memoryCache);
        // 代码管理的缓存化
        codeCaher.EnsureCache();

        var configuration = serviceProvider.GetRequiredService<IRepository<ConfigurationEntity>>();
        var configCaher = (ISettingProviderCacher)new SettingProvider(configuration, memoryCache);
        // 配置管理的缓存化
        configCaher.EnsureCache();

        app.Logger.LogInformation("通用代码信息数据已获取并设定缓存");
    }
    catch (Exception e)
    {
        app.Logger.LogCritical(e, e.Message);
        app.MapGet("/", _ => throw new("Start up failed:" + e.Message));
        app.Run();
    }
}