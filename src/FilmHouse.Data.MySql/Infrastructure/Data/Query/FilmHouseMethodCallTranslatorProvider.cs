using Microsoft.EntityFrameworkCore.Query;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;

namespace FilmHouse.Data.MySql.Infrastructure.Data.Query
{
    /// <summary>
    /// 
    /// </summary>
#pragma warning disable EF1001 // Internal EF Core API usage.
    public class FilmHouseMethodCallTranslatorProvider : MySqlMethodCallTranslatorProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="mysqlOptions"></param>
        public FilmHouseMethodCallTranslatorProvider(RelationalMethodCallTranslatorProviderDependencies dependencies, IMySqlOptions mysqlOptions)
            : base(dependencies, mysqlOptions)
        {
            var sqlExpressionFactory = (MySqlSqlExpressionFactory)dependencies.SqlExpressionFactory;
            var typeMappingSource = (MySqlTypeMappingSource)dependencies.RelationalTypeMappingSource;
            this.AddTranslators(new IMethodCallTranslator[]
            {
                new FilmHouseStringMethodTranslator(sqlExpressionFactory, typeMappingSource, mysqlOptions)
            });
        }
    }
#pragma warning restore EF1001 // Internal EF Core API usage.
}
