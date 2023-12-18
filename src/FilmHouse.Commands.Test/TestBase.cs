using System.ComponentModel;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data;
using FilmHouse.Data.Entities;
using FilmHouse.Data.MySql;
using FilmHouse.Data.PostgreSql;
using FilmHouse.Data.SqlServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FilmHouse.Commands.Test
{
    public partial class TestBase
    {
        private IServiceScope _serviceScope;

        protected TestServer _testServer;
        protected FilmHouseDbContext _dbContext;

        [SetUp]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetupBase()
        {

            //var serviceProvider = this._serviceScope.ServiceProvider;

            /*
            var configuration = this.ServiceProvider.GetService<IConfiguration>();
            var dbType = configuration.GetValue<string>("ConnectionStrings:DatabaseType");

            dbContext = dbType.ToLowerInvariant() switch
            {
                "mysql" => this.ServiceProvider.GetRequiredService<MySqlFilmHouseDbContext>(),
                "sqlserver" => this.ServiceProvider.GetRequiredService<SqlServerFilmHouseDbContext>(),
                "postgresql" => this.ServiceProvider.GetRequiredService<PostgreSqlFilmHouseDbContext>(),
                _ => throw new ArgumentOutOfRangeException(nameof(dbType))
            };
            */
        }

        [OneTimeSetUp]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task SetUp()
        {
            this._testServer = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            this._serviceScope = this._testServer.Services.CreateScope();

            var configuration = this.ServiceProvider.GetService<IConfiguration>();
            var dbType = configuration.GetValue<string>("ConnectionStrings:DatabaseType");

            this.DbContext = dbType.ToLowerInvariant() switch
            {
                "mysql" => this.ServiceProvider.GetRequiredService<MySqlFilmHouseDbContext>(),
                "sqlserver" => this.ServiceProvider.GetRequiredService<SqlServerFilmHouseDbContext>(),
                "postgresql" => this.ServiceProvider.GetRequiredService<PostgreSqlFilmHouseDbContext>(),
                _ => throw new ArgumentOutOfRangeException(nameof(dbType))
            };

            //using (var scope = this._serviceScope)
            //{
                await TestInitialiseAsync();
            //}
        }

        private async Task TestInitialiseAsync()
        {
            await DbContext.Database.EnsureCreatedAsync();
            await DbContext.ClearAllData();
            //await this.SeedAsync();
        }

        private async Task SeedAsync()
        {
            var uuid = new RequestIdVO(Guid.NewGuid());
            var sysDate = new CreatedOnVO(System.DateTime.Now);

            await DbContext.Configuration.AddRangeAsync(GetInitConfigurationSettings(uuid, sysDate));
            await DbContext.CodeMast.AddRangeAsync(GetInitCodeMastSettings(uuid, sysDate));
            //await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        [OneTimeTearDown]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void TeadDown()
        {
            if (this._serviceScope != null)
            {
                this._serviceScope.Dispose();
            }

            if (this._testServer != null)
            {
                this._testServer.Dispose();
            }
        }

        /// <summary>
        /// 在测试方法的每次执行结束时，记载被实施的处理。
        /// </summary>
        [TearDown]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void TearDownBase()
        {
            /*
            if (this._serviceScope != null)
            {
                this._serviceScope.Dispose();
            }
            */
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

        private FilmHouseDbContext _dbcontext;

        protected FilmHouseDbContext DbContext
        {
            get => Guard.GetNotNull(this._dbcontext, nameof(this.DbContext));
            private set => this._dbcontext = value;
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
