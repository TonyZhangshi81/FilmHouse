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
            var sqlExpressionFactory = (NpgsqlSqlExpressionFactory)dependencies.SqlExpressionFactory;
            var typeMappingSource = (NpgsqlTypeMappingSource)dependencies.RelationalTypeMappingSource;
            this.AddTranslators(new IMethodCallTranslator[]
            {
                new FilmHouseStringMethodTranslator(typeMappingSource, sqlExpressionFactory, model),
                new FilmHouseComparableExtensionMemberTranslator(typeMappingSource, sqlExpressionFactory),
                new FilmHouseConvertTranslator(sqlExpressionFactory),
                new FilmHouseMathTranslator(typeMappingSource, sqlExpressionFactory, model),
            });
        }
    }
#pragma warning restore EF1001 // Internal EF Core API usage.
}
