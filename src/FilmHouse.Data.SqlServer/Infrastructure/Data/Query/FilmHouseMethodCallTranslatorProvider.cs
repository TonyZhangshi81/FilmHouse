using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace FilmHouse.Data.SqlServer.Infrastructure.Data.Query
{
    /// <summary>
    /// 
    /// </summary>
#pragma warning disable EF1001 // Internal EF Core API usage.
    public class FilmHouseMethodCallTranslatorProvider : SqlServerMethodCallTranslatorProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencies"></param>
        public FilmHouseMethodCallTranslatorProvider(RelationalMethodCallTranslatorProviderDependencies dependencies)
            : base(dependencies)
        {
            var sqlExpressionFactory = (ISqlExpressionFactory)dependencies.SqlExpressionFactory;
            this.AddTranslators(new IMethodCallTranslator[]
            {
                new FilmHouseStringMethodTranslator(sqlExpressionFactory)
            });
        }
    }
#pragma warning restore EF1001 // Internal EF Core API usage.
}
