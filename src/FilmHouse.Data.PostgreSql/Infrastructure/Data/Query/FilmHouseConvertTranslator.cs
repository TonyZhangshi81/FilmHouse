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

namespace FilmHouse.Data.PostgreSql.Infrastructure.Data.Query
{
    /// <summary>
    /// 
    /// </summary>
#pragma warning disable EF1001 // Internal EF Core API usage.
    public class FilmHouseConvertTranslator : NpgsqlConvertTranslator
    {
        private static readonly Dictionary<string, string> TypeMapping = new()
        {
            [nameof(Convert.ToBoolean)] = "bool",
            [nameof(Convert.ToByte)] = "smallint",
            [nameof(Convert.ToDecimal)] = "numeric",
            [nameof(Convert.ToDouble)] = "double precision",
            [nameof(Convert.ToInt16)] = "smallint",
            [nameof(Convert.ToInt32)] = "int",
            [nameof(Convert.ToInt64)] = "bigint",
            [nameof(Convert.ToString)] = "text"
        };

        private static readonly List<Type> SupportedTypes = new()
        {
            typeof(object),
        };

        private static readonly List<MethodInfo> SupportedMethods
            = TypeMapping.Keys
                .SelectMany(
                    t => typeof(Convert).GetTypeInfo().GetDeclaredMethods(t)
                        .Where(
                            m => m.GetParameters().Length == 1
                                && SupportedTypes.Contains(m.GetParameters().First().ParameterType)))
                .ToList();

        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlExpressionFactory"></param>
        public FilmHouseConvertTranslator(ISqlExpressionFactory sqlExpressionFactory)
            : base(sqlExpressionFactory)
        {
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
        public override SqlExpression? Translate(SqlExpression? instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            if (SupportedMethods.Contains(method) && arguments.Count == 1 && arguments[0].Type.IsValueObject())
            {
                return this._sqlExpressionFactory.Convert(arguments[0], method.ReturnType);
            }
            return base.Translate(instance, method, arguments, logger);
        }
    }
#pragma warning restore EF1001 // Internal EF Core API usage.
}
