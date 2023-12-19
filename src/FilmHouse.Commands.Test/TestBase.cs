using System.ComponentModel;
using FilmHouse.Core.Services.Codes;
using FilmHouse.Core.Services.Configuration;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.Infrastructure.Services.Codes;
using FilmHouse.Data.Infrastructure.Services.Configuration;
using FilmHouse.Data.MySql;
using FilmHouse.Data.PostgreSql;
using FilmHouse.Data.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FilmHouse.Commands.Test
{
    public partial class TestBase
    {
        protected TestServer testServer;

        [OneTimeSetUp]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task SetUp()
        {
            this.testServer = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            _serviceScope = this.testServer.Services.CreateScope();

            var configuration = this.ServiceProvider.GetService<IConfiguration>();
            var dbType = configuration.GetValue<string>("ConnectionStrings:DatabaseType");

            _dbcontext = dbType.ToLowerInvariant() switch
            {
                "mysql" => this.ServiceProvider.GetRequiredService<MySqlFilmHouseDbContext>(),
                "sqlserver" => this.ServiceProvider.GetRequiredService<SqlServerFilmHouseDbContext>(),
                "postgresql" => this.ServiceProvider.GetRequiredService<PostgreSqlFilmHouseDbContext>(),
                _ => throw new ArgumentOutOfRangeException(nameof(dbType))
            };

            _mediator = this.ServiceProvider.GetRequiredService<IMediator>();

            await TestInitialiseAsync();
        }

        private async Task TestInitialiseAsync()
        {
            await _dbcontext.Database.EnsureCreatedAsync();

            bool isNew = !await _dbcontext.Configuration.AnyAsync();
            if (isNew)
            {
                await _dbcontext.ClearAllData();
                await this.SeedAsync();
            }

            var memoryCache = this.ServiceProvider.GetRequiredService<IMemoryCache>();

            var codeMast = this.ServiceProvider.GetRequiredService<IRepository<CodeMastEntity>>();
            var codeCaher = (ICodeProviderCacher)new CodeProvider(codeMast, memoryCache);
            // 代码管理的缓存化
            codeCaher.EnsureCache();

            var configuration = this.ServiceProvider.GetRequiredService<IRepository<ConfigurationEntity>>();
            var configCaher = (ISettingProviderCacher)new SettingProvider(configuration, memoryCache);
            // 配置管理的缓存化
            configCaher.EnsureCache();
        }

        private async Task SeedAsync()
        {
            var uuid = new RequestIdVO(Guid.NewGuid());
            var sysDate = new CreatedOnVO(System.DateTime.Now);

            await _dbcontext.Configuration.AddRangeAsync(GetInitConfigurationSettings(uuid, sysDate));
            await _dbcontext.CodeMast.AddRangeAsync(GetInitCodeMastSettings(uuid, sysDate));
            await _dbcontext.SaveChangesAsync();
        }

        /// <summary>
        /// 测试执行结束时，记载被实施的处理。
        /// </summary>
        [OneTimeTearDown]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void TeadDown()
        {
            if (this._serviceScope != null)
            {
                this._serviceScope.Dispose();
            }

            if (this.testServer != null)
            {
                this.testServer.Dispose();
            }

            if (this._dbcontext != null)
            {
                this._dbcontext.Dispose();
            }
        }

        private IServiceScope _serviceScope;
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

        private FilmHouseDbContext _dbcontext;

        protected FilmHouseDbContext DbContext
        {
            get => Guard.GetNotNull(this._dbcontext, nameof(this.DbContext));
            private set => this._dbcontext = value;
        }

        private IMediator _mediator;

        protected IMediator Mediator
        {
            get => Guard.GetNotNull(this._mediator, nameof(this.Mediator));
            private set => this._mediator = value;
        }

        /// <summary>
        /// 配置信息
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static IEnumerable<ConfigurationEntity> GetInitConfigurationSettings(RequestIdVO uuid, CreatedOnVO dateTime) =>
            new List<ConfigurationEntity>
            {
            new() { RequestId = uuid, Key = new("WebSiteSettings:Name"), Value = new("DEMO"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:SubTitle"), Value = new("DEMO"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:Version"), Value = new("0.2.0.0"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:WebpagesEnabled"), Value = new("false"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:ClientValidationEnabled"), Value = new("true"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("WebSiteSettings:UnobtrusiveJavaScriptEnabled"), Value = new("false"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = new("Home:Discovery:MaxPage"), Value = new("6"), CreatedOn = dateTime },
            };

        /// <summary>
        /// 代码信息管理表
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static IEnumerable<CodeMastEntity> GetInitCodeMastSettings(RequestIdVO uuid, CreatedOnVO dateTime) =>
            new List<CodeMastEntity>
            {
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("001"), Name = new CodeValueVO("剧情"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("002"), Name = new CodeValueVO("爱情"), Order = new SortOrderVO(2), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("003"), Name = new CodeValueVO("奇幻"), Order = new SortOrderVO(3), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("004"), Name = new CodeValueVO("惊悚"), Order = new SortOrderVO(4), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("005"), Name = new CodeValueVO("喜剧"), Order = new SortOrderVO(5), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("006"), Name = new CodeValueVO("动作"), Order = new SortOrderVO(6), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("007"), Name = new CodeValueVO("科幻"), Order = new SortOrderVO(7), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("008"), Name = new CodeValueVO("冒险"), Order = new SortOrderVO(8), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("GenreMovie"), Code = new CodeKeyVO("009"), Name = new CodeValueVO("悬疑"), Order = new SortOrderVO(9), CreatedOn  = dateTime },

            new() { RequestId = uuid, Group = new CodeGroupVO("Language"), Code = new CodeKeyVO("001"), Name = new CodeValueVO("英语"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("Language"), Code = new CodeKeyVO("002"), Name = new CodeValueVO("法语"), Order = new SortOrderVO(2), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("Language"), Code = new CodeKeyVO("003"), Name = new CodeValueVO("意大利语"), Order = new SortOrderVO(3), CreatedOn  = dateTime },

            new() { RequestId = uuid, Group = new CodeGroupVO("Country"), Code = new CodeKeyVO("001"), Name = new CodeValueVO("美国"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = new CodeGroupVO("Country"), Code = new CodeKeyVO("002"), Name = new CodeValueVO("澳大利亚"), Order = new SortOrderVO(2), CreatedOn  = dateTime },

            };
    }
}
