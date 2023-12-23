using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Core.Utils.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal;

namespace Isid.Ilex.Core.Infrastructure.Data.Query
{
    /// <summary>
    /// <see cref="IComparableExtension"/>
    /// </summary>
#pragma warning disable EF1001 // Internal EF Core API usage.
    public class FilmHouseComparableExtensionMemberTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo ExtensionBetweenMethodInfo = typeof(IComparableExtension).GetMethod(
            nameof(IComparableExtension.Between),
            BindingFlags.Static | BindingFlags.Public)!;

        private readonly NpgsqlTypeMappingSource _typeMappingSource;
        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeMappingSource"></param>
        /// <param name="sqlExpressionFactory"></param>
        public FilmHouseComparableExtensionMemberTranslator(NpgsqlTypeMappingSource typeMappingSource, ISqlExpressionFactory sqlExpressionFactory)
        {
            this._typeMappingSource = typeMappingSource;
            this._sqlExpressionFactory = sqlExpressionFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="method"></param>
        /// <param name="arguments"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual SqlExpression? Translate(SqlExpression? instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            if (method.IsGenericMethod && method.GetGenericMethodDefinition() == ExtensionBetweenMethodInfo)
            {
                var left = arguments[0];
                var right1 = arguments[1];
                var right2 = arguments[2];
                var g = this._sqlExpressionFactory.GreaterThanOrEqual(left, right1);
                var l = this._sqlExpressionFactory.LessThanOrEqual(left, right2);
                return this._sqlExpressionFactory.And(g, l);
            }
            return null;
        }
    }
#pragma warning restore EF1001 // Internal EF Core API usage.
}
