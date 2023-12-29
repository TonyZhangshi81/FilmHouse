using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;

namespace FilmHouse.Data.PostgreSql.Infrastructure.Data.Query
{
    /// <summary>
    /// 
    /// </summary>
#pragma warning disable EF1001 // Internal EF Core API usage.
    public class FilmHouseMathTranslator : NpgsqlMathTranslator
    {
        private static readonly Dictionary<MethodInfo, MethodInfo> SupportedMethodTranslations = new()
        {
            // Abs
            {
                typeof(EFMath).GetRuntimeMethod(nameof(EFMath.Abs), new[] { typeof(IValue<short>) })!,
                typeof(Math).GetRuntimeMethod(nameof(Math.Abs), new[] { typeof(short) })!
            },
            {
                typeof(EFMath).GetRuntimeMethod(nameof(EFMath.Abs), new[] { typeof(IValue<int>) })!,
                typeof(Math).GetRuntimeMethod(nameof(Math.Abs), new[] { typeof(int) })!
            },
            {
                typeof(EFMath).GetRuntimeMethod(nameof(EFMath.Abs), new[] { typeof(IValue<long>) })!,
                typeof(Math).GetRuntimeMethod(nameof(Math.Abs), new[] { typeof(long) })!
            },
            {
                typeof(EFMath).GetRuntimeMethod(nameof(EFMath.Abs), new[] { typeof(IValue<decimal>) })!,
                typeof(Math).GetRuntimeMethod(nameof(Math.Abs), new[] { typeof(decimal) })!
            },
            // Ceiling
            {
                typeof(EFMath).GetRuntimeMethod(nameof(EFMath.Ceiling), new[] { typeof(IValue<decimal>) })!,
                typeof(Math).GetRuntimeMethod(nameof(Math.Ceiling), new[] { typeof(decimal) })!
            },
            // Floor
            {
                typeof(EFMath).GetRuntimeMethod(nameof(EFMath.Floor), new[] { typeof(IValue<decimal>) })!,
                typeof(Math).GetRuntimeMethod(nameof(Math.Floor), new[] { typeof(decimal) })!
            },
            // Round
            {
                typeof(EFMath).GetRuntimeMethod(nameof(EFMath.Round), new[] { typeof(IValue<decimal>) })!,
                typeof(Math).GetRuntimeMethod(nameof(Math.Round), new[] { typeof(decimal) })!
            },
            {
                typeof(EFMath).GetRuntimeMethod(nameof(EFMath.Round), new[] { typeof(IValue<decimal>), typeof(int) })!,
                typeof(Math).GetRuntimeMethod(nameof(Math.Round), new[] { typeof(decimal), typeof(int) })!
            },
            // Truncate
            {
                typeof(EFMath).GetRuntimeMethod(nameof(EFMath.Truncate), new[] { typeof(IValue<decimal>) })!,
                typeof(Math).GetRuntimeMethod(nameof(Math.Truncate), new[] { typeof(decimal) })!
            },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeMappingSource"></param>
        /// <param name="sqlExpressionFactory"></param>
        /// <param name="model"></param>
        public FilmHouseMathTranslator(
            IRelationalTypeMappingSource typeMappingSource,
            ISqlExpressionFactory sqlExpressionFactory,
            IModel model)
                : base(typeMappingSource, sqlExpressionFactory, model)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="method"></param>
        /// <param name="arguments"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public override SqlExpression? Translate(
            SqlExpression? instance,
            MethodInfo method,
            IReadOnlyList<SqlExpression> arguments,
            IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            if (SupportedMethodTranslations.ContainsKey(method))
            {
                return base.Translate(instance, SupportedMethodTranslations[method], arguments, logger);
            }
            return base.Translate(instance, method, arguments, logger);
        }
    }
#pragma warning restore EF1001 // Internal EF Core API usage.
}
