using System.ComponentModel;
using FilmHouse.Core.Utils;
using FilmHouse.Data;
using FilmHouse.Data.MySql;
using FilmHouse.Data.PostgreSql;
using FilmHouse.Data.SqlServer;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FilmHouse.Commands.Test
{
    public abstract class TransactionalTestBase : TestBase
    {
        private IDbContextTransaction _transaction;

        /// <summary>
        /// 在测试开始时启动DB的事务。
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [SetUp]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetupTransactional()
        {
            var configuration = this.ServiceProvider.GetRequiredService<IConfiguration>();
            var dbType = configuration.GetValue<string>("ConnectionStrings:DatabaseType");

            this._dbcontext = dbType.ToLowerInvariant() switch
            {
                "mysql" => this.ServiceProvider.GetRequiredService<MySqlFilmHouseDbContext>(),
                "sqlserver" => this.ServiceProvider.GetRequiredService<SqlServerFilmHouseDbContext>(),
                "postgresql" => this.ServiceProvider.GetRequiredService<PostgreSqlFilmHouseDbContext>(),
                _ => throw new ArgumentOutOfRangeException(nameof(dbType))
            };

            this._transaction = this._dbcontext.Database.BeginTransaction();
        }

        /// <summary>
        /// 测试结束时，回滚DB的事务结束。
        /// </summary>
        [TearDown]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void TearDownTransactional()
        {
            try
            {
                // 考虑到已经明确地实施了滚回或提交的情况，用try ~ catch将其控制
                this._transaction.Rollback();
            }
            catch
            {
            }

            if (this._transaction != null)
            {
                this._transaction.Dispose();
            }

            if (this._dbcontext != null)
            {
                this._dbcontext.Dispose();
            }
        }

        private FilmHouseDbContext _dbcontext;

        /// <summary>
        /// 获得在这个测试类中使用的<see cref="DbContext"/的实例。
        /// </summary>
        protected FilmHouseDbContext DbContext
        {
            get => Guard.GetNotNull(this._dbcontext, nameof(this.DbContext)); private set => this._dbcontext = value;
        }
    }
}
