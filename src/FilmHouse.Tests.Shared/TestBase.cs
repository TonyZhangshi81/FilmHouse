using System.ComponentModel;
using System.Reflection;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Utils;
using FilmHouse.Data;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Infrastructure.Services.Codes;
using FilmHouse.Data.Infrastructure.Services.Configuration;
using FilmHouse.Data.MySql;
using FilmHouse.Data.PostgreSql;
using FilmHouse.Data.SqlServer;
using FilmHouse.Tests.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FilmHouse.Tests;

public partial class TestBase
{
    private ICollection<Mock> _mocks;
    private IServiceScope _serviceScope;

    /// <summary>
    /// 當所有測試開始之前執行
    /// </summary>
    [OneTimeSetUp]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task SetUp()
    {
        using (var scope = InitializeUtil.SetupServiceScope())
        {
            this._serviceScope = scope;

            var configuration = this.ServiceProvider.GetRequiredService<IConfiguration>();
            var dbType = configuration.GetValue<string>("ConnectionStrings:DatabaseType");

            FilmHouseDbContext dbcontext = dbType.ToLowerInvariant() switch
            {
                "mysql" => this.ServiceProvider.GetRequiredService<MySqlFilmHouseDbContext>(),
                "sqlserver" => this.ServiceProvider.GetRequiredService<SqlServerFilmHouseDbContext>(),
                "postgresql" => this.ServiceProvider.GetRequiredService<PostgreSqlFilmHouseDbContext>(),
                _ => throw new ArgumentOutOfRangeException(nameof(dbType))
            };

            // 構築測試用數據
            await dbcontext.Database.EnsureCreatedAsync();

            bool isNew = !await dbcontext.Configuration.AnyAsync();
            if (isNew)
            {
                await dbcontext.ClearAllData();
                // 測試前期數據導入(长期驻留)
                var logger = this.ServiceProvider.GetRequiredService<ILogger<TestBase>>();
                await Seed.SeedAsync(dbcontext, logger);
            }

            {
                var memoryCache = this.ServiceProvider.GetRequiredService<IMemoryCache>();

                // 代码管理的缓存化
                var codeMast = this.ServiceProvider.GetRequiredService<IRepository<CodeMastEntity>>();
                var codeCaher = (ICodeProviderCacher)new CodeProvider(codeMast, memoryCache);
                codeCaher.EnsureCache();

                // 配置管理的缓存化
                var configEntity = this.ServiceProvider.GetRequiredService<IRepository<ConfigurationEntity>>();
                var configCaher = (ISettingProviderCacher)new SettingProvider(configEntity, memoryCache);
                configCaher.EnsureCache();
            }
        }
    }

    /// <summary>
    /// 當單個測試方法開始之前執行
    /// </summary>
    [SetUp]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetupBase()
    {
        this._serviceScope = InitializeUtil.SetupServiceScope(this.SetupServices);
        this._mediator = this.ServiceProvider.GetRequiredService<IMediator>();

    }

    protected virtual void SetupServices(IServiceCollection services)
    {
        this._mocks = new List<Mock>();
        var testMethodName = TestContext.CurrentContext.Test.MethodName;
        if (testMethodName == null)
        {
            return;
        }
        var testMethodInfo = this.GetType().GetMethod(testMethodName!, BindingFlags.Public | BindingFlags.Instance);
        var aa = testMethodInfo?.GetCustomAttribute<ArrangeAttribute>();
        if (aa == null)
        {
            return;
        }
        var arrangeMethodInfo = this.GetType().GetMethod(aa.ArrangeMethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        if (arrangeMethodInfo == null)
        {
            return;
        }

        if (arrangeMethodInfo.ReturnType == typeof(void) &&
            arrangeMethodInfo.GetParameters().Length == 2 &&
            arrangeMethodInfo.GetParameters()[0].ParameterType == typeof(IServiceCollection) &&
            arrangeMethodInfo.GetParameters()[1].ParameterType == typeof(ICollection<Mock>))
        {
            arrangeMethodInfo.Invoke(null, new object[] { services, this._mocks });
        }
    }

    [TearDown]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void TearDownBase()
    {
        if (this._serviceScope != null)
        {
            this._serviceScope.Dispose();
        }
    }

    /// <summary>
    /// 黨所有测试执行结束时，记载被实施的处理。
    /// </summary>
    [OneTimeTearDown]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void TeadDown()
    {
    }

    /// <summary>
    /// 取得服务提供商。
    /// </summary>
    protected IServiceProvider ServiceProvider
    {
        get
        {
            return Guard.GetNotNull(this._serviceScope, nameof(IServiceProvider)).ServiceProvider;
        }
    }

    private IMediator _mediator;
    /// <summary>
    /// 進程內消息傳遞功能接口
    /// </summary>
    protected IMediator Mediator
    {
        get => Guard.GetNotNull(this._mediator, nameof(this.Mediator)); private set => this._mediator = value;
    }

    protected Mock[] Mocks
    {
        get
        {
            return Guard.GetNotNull(this._mocks, nameof(this.Mocks)).ToArray();
        }
    }

}
