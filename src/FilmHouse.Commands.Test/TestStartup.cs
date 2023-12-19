using System.Reflection;
using FilmHouse.Commands.Test.Utils;
using FilmHouse.Core.DependencyInjection;
using FilmHouse.Data.Infrastructure;
using FilmHouse.Data.MySql;
using FilmHouse.Data.MySql.Infrastructure;
using FilmHouse.Data.PostgreSql;
using FilmHouse.Data.PostgreSql.Infrastructure;
using FilmHouse.Data.SqlServer;
using FilmHouse.Data.SqlServer.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace FilmHouse.Commands.Test
{
    public class TestStartup
    {
        public TestStartup(IConfiguration config)
        {
        }

        public void Configure(IApplicationBuilder builder)
        {
            builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // 缓存管理
            services.AddMemoryCache();

            // DI的批量登录设定
            var assemblies = this.GetAppAssemblies(true);
            services.AddLocalService(assemblies);

            services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            services.AddSingleton<IConfiguration>(configuration);

            var dbType = configuration.GetValue<string>("ConnectionStrings:DatabaseType");
            var connectionString = configuration.GetValue<string>("ConnectionStrings:FilmHouseDatabase");

            // (在测试工程中)以下DbContext禁止使用EnableRetryOnFailure的执行策略设定（提供"连接复原"操作）
            // https://learn.microsoft.com/zh-cn/ef/core/miscellaneous/connection-resiliency 提供"连接复原"的具体应用
            switch (dbType.ToLower())
            {
                case "mysql":
                    services.AddScoped(typeof(IRepository<>), typeof(MySqlDbContextRepository<>));
                    services.AddDbContext<MySqlFilmHouseDbContext>(optionsAction =>
                        optionsAction
                            .UseLazyLoadingProxies()
                            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                            .EnableDetailedErrors());
                    break;

                case "postgresql":
                    services.AddScoped(typeof(IRepository<>), typeof(PostgreSqlDbContextRepository<>));
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    services.AddDbContext<PostgreSqlFilmHouseDbContext>(optionsAction =>
                        optionsAction
                            .UseLazyLoadingProxies()
                            .UseNpgsql(connectionString)
                            .EnableDetailedErrors());
                    break;

                case "sqlserver":
                default:
                    services.AddScoped(typeof(IRepository<>), typeof(SqlServerDbContextRepository<>));
                    services.AddDbContext<SqlServerFilmHouseDbContext>(optionsAction =>
                        optionsAction
                            .UseLazyLoadingProxies()
                            .UseSqlServer(connectionString)
                            .EnableDetailedErrors());
                    break;
            }
        }

        /// <summary>
        /// 列举应用程序的运行路径和下面的文件夹中的配件。
        /// </summary>
        /// <param name="distinct">是否排除重复的组合件文件名</param>
        /// <returns>列举的组合件。列举顺序是按照组合件名称的长短顺序。(为了让FilmHouse系统最先被读取)</returns>
        internal IEnumerable<Assembly> GetAppAssemblies(bool distinct)
        {
            var rootDir = AppDomain.CurrentDomain.BaseDirectory;
            var rootDi = new DirectoryInfo(rootDir);
            var usedAssemblies = new HashSet<string>();

            var targetDll = $"FilmHouse.***.dll";
            var assemblyFiles = Directory.GetFiles(rootDir, targetDll, SearchOption.TopDirectoryOnly);
            foreach (var assemblyFile in assemblyFiles.OrderBy(_ => _.Length))
            {
                var fi = new FileInfo(assemblyFile);

                // 获取当前已经被读取的一组组件(已经被读取的用于返回而不Load)
                var asm = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(_ => _.FullName != null && _.FullName.StartsWith("FilmHouse."))
                    .Select(_ => new { Assembly = _, FileInfo = new FileInfo(_.Location) })
                    .FirstOrDefault(_ => _.FileInfo.Name == fi.Name)?.Assembly ?? Assembly.LoadFrom(assemblyFile);
                if (usedAssemblies.Contains(fi.Name))
                {
                    continue;
                }
                if (distinct)
                {
                    // 只存储重复去除的情况
                    usedAssemblies.Add(fi.Name);
                }
                yield return asm;
            }
        }
    }
}
