using System.Reflection;
using FilmHouse.Core.Utils;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace FilmHouse.Data.SqlServer.Infrastructure.Data.Query
{
#pragma warning disable EF1001 // Internal EF Core API usage.
    public class FilmHouseStringMethodTranslator : SqlServerStringMethodTranslator
    {
        private static readonly Dictionary<MethodInfo, MethodInfo> SupportedMethodTranslations = new()
        {
            // Contains
            {
                typeof(IStringValueExtension).GetMethod(nameof(IStringValueExtension.Contains), new[] { typeof(IValue<string>), typeof(string) })!,
                typeof(string).GetRuntimeMethod(nameof(string.Contains), new[] { typeof(string) })!
            },
            // StartsWith
            {
                typeof(IStringValueExtension).GetMethod(nameof(IStringValueExtension.StartsWith), new[] { typeof(IValue<string>), typeof(string) })!,
                typeof(string).GetRuntimeMethod(nameof(string.StartsWith), new[] { typeof(string) })!
            },
            // EndsWith
            {
                typeof(IStringValueExtension).GetMethod(nameof(IStringValueExtension.EndsWith), new[] { typeof(IValue<string>), typeof(string) })!,
                typeof(string).GetRuntimeMethod(nameof(string.EndsWith), new[] { typeof(string) })!
            },
        };

        private static readonly MethodInfo ExtensionConcatMethodInfo = typeof(IStringValueExtension).GetMethod(
            nameof(IStringValueExtension.Concat))!;

        private static readonly MethodInfo ExtensionLengthMethodInfo = typeof(IStringValueExtension).GetMethod(
            nameof(IStringValueExtension.Length))!;

        private static readonly Dictionary<MethodInfo, MethodInfo> SupportedGenericMethodTranslations = new()
        {
            // PadLeft
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.PadLeft) && _.GetParameters().Length == 2),
                typeof(string).GetRuntimeMethod(nameof(string.PadLeft), new[] { typeof(int) })!
            },
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.PadLeft) && _.GetParameters().Length == 3),
                typeof(string).GetRuntimeMethod(nameof(string.PadLeft), new[] { typeof(int), typeof(char) })!
            },
            // PadRight
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.PadRight) && _.GetParameters().Length == 2),
                typeof(string).GetRuntimeMethod(nameof(string.PadRight), new[] { typeof(int) })!
            },
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.PadRight) && _.GetParameters().Length == 3),
                typeof(string).GetRuntimeMethod(nameof(string.PadRight), new[] { typeof(int), typeof(char) })!
            },
            // Replace
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.Replace) && _.GetParameters().Length == 3),
                typeof(string).GetRuntimeMethod(nameof(string.Replace), new[] { typeof(string), typeof(string) })!
            },
            // Trim
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.Trim) && _.GetParameters().Length == 1),
                typeof(string).GetRuntimeMethod(nameof(string.Trim), Array.Empty<Type>())!
            },
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.Trim) && _.GetParameters().Length == 2),
                typeof(string).GetRuntimeMethod(nameof(string.Trim), new[] { typeof(char) })!
            },
            // TrimEnd
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.TrimEnd) && _.GetParameters().Length == 1),
                typeof(string).GetRuntimeMethod(nameof(string.TrimEnd), Array.Empty<Type>())!
            },
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.TrimEnd) && _.GetParameters().Length == 2),
                typeof(string).GetRuntimeMethod(nameof(string.TrimEnd), new[] { typeof(char) })!
            },
            // TrimStart
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.TrimStart) && _.GetParameters().Length == 1),
                typeof(string).GetRuntimeMethod(nameof(string.TrimStart), Array.Empty<Type>())!
            },
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.TrimStart) && _.GetParameters().Length == 2),
                typeof(string).GetRuntimeMethod(nameof(string.TrimStart), new[] { typeof(char) })!
            },
            // Substring
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.Substring) && _.GetParameters().Length == 2),
                typeof(string).GetRuntimeMethod(nameof(string.Substring), new[] { typeof(int) })!
            },
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.Substring) && _.GetParameters().Length == 3),
                typeof(string).GetRuntimeMethod(nameof(string.Substring), new[] { typeof(int), typeof(int) })!
            },
            // ToUpper
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.ToUpper) && _.GetParameters().Length == 1),
                typeof(string).GetRuntimeMethod(nameof(string.ToUpper), Array.Empty<Type>())!
            },
            // ToLower
            {
                typeof(IStringValueExtension).GetMethods().First(_ => _.Name == nameof(IStringValueExtension.ToLower) && _.GetParameters().Length == 1),
                typeof(string).GetRuntimeMethod(nameof(string.ToLower), Array.Empty<Type>())!
            },
        };

        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlExpressionFactory"></param>
        public FilmHouseStringMethodTranslator(ISqlExpressionFactory sqlExpressionFactory)
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
        /// <exception cref="NotImplementedException"></exception>
        public override SqlExpression? Translate(SqlExpression? instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            if (SupportedMethodTranslations.ContainsKey(method))
            {
                return base.Translate(arguments[0], SupportedMethodTranslations[method], arguments.ToArray()[1..^0], logger);
            }

            if (method.IsGenericMethod)
            {
                var genericMethod = method.GetGenericMethodDefinition();
                if (genericMethod == ExtensionConcatMethodInfo)
                {
                    return this._sqlExpressionFactory.Add(this._sqlExpressionFactory.Coalesce(arguments[0], this._sqlExpressionFactory.Constant("")), arguments[1]);
                }
                if (genericMethod == ExtensionLengthMethodInfo)
                {
                    return this._sqlExpressionFactory.Convert(
                                this._sqlExpressionFactory.Function(
                                    "length",
                                    new[] { arguments[0] },
                                    nullable: true,
                                    argumentsPropagateNullability: new[] { true },
                                    typeof(long)),
                                    method.ReturnType);
                }
                if (SupportedGenericMethodTranslations.ContainsKey(genericMethod))
                {
                    return base.Translate(arguments[0], SupportedGenericMethodTranslations[genericMethod], arguments.ToArray()[1..^0], logger);
                }
            }
            return null;
        }
    }
#pragma warning restore EF1001 // Internal EF Core API usage.
}
