using Isid.Ilex.Core.Infrastructure.Data.Query;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal;

namespace FilmHouse.Data.PostgreSql.Infrastructure.Data.Query
{
    /// <summary>
    /// 
    /// </summary>
#pragma warning disable EF1001 // Internal EF Core API usage.
    public class FilmHouseMethodCallTranslatorProvider : NpgsqlMethodCallTranslatorProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="mysqlOptions"></param>
        public FilmHouseMethodCallTranslatorProvider(
            RelationalMethodCallTranslatorProviderDependencies dependencies,
            IModel model,
            IDbContextOptions npgsqlOptions)
            : base(dependencies, model, npgsqlOptions)
        {
           // 获取NpgsqlSqlExpressionFactory实例
            var sqlExpressionFactory = (NpgsqlSqlExpressionFactory)dependencies.SqlExpressionFactory;
            // 获取NpgsqlTypeMappingSource实例
            var typeMappingSource = (NpgsqlTypeMappingSource)dependencies.RelationalTypeMappingSource;
            // 添加翻译器
            this.AddTranslators(new IMethodCallTranslator[]
            {
                //FilmHouseStringMethodTranslator：用于处理字符串方法
                new FilmHouseStringMethodTranslator(typeMappingSource, sqlExpressionFactory, model),
                //FilmHouseComparableExtensionMemberTranslator：用于处理可比较扩展成员
                new FilmHouseComparableExtensionMemberTranslator(typeMappingSource, sqlExpressionFactory),
                //FilmHouseConvertTranslator：用于处理转换
                new FilmHouseConvertTranslator(sqlExpressionFactory),
                //FilmHouseMathTranslator：用于处理数学运算
                new FilmHouseMathTranslator(typeMappingSource, sqlExpressionFactory, model),
            });
        }
    }
#pragma warning restore EF1001 // Internal EF Core API usage.
}
