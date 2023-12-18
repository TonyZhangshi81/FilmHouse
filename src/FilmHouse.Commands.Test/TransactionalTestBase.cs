using System.ComponentModel;
using FilmHouse.Core.Utils;
using FilmHouse.Data;
using Microsoft.EntityFrameworkCore.Storage;
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
            this._transaction = this.DbContext.Database.BeginTransaction();
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
            if (this.DbContext != null)
            {
                this.DbContext.Dispose();
            }
        }
    }
}
