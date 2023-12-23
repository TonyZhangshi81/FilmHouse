using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Core.Utils.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.ValueConversion;

namespace FilmHouse.Data.PostgreSql.Infrastructure.Data.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class FilmHouseValueConverterSelector : NpgsqlValueConverterSelector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencies"></param>
        public FilmHouseValueConverterSelector(ValueConverterSelectorDependencies dependencies)
            : base(dependencies)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelClrType"></param>
        /// <param name="providerClrType"></param>
        /// <returns></returns>
        public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type? providerClrType = null)
        {
            Type? valueObjectType = null;
            if (modelClrType.IsArray && modelClrType.GetElementType()!.IsValueObject())
            {
                // 简单排列
                valueObjectType = modelClrType.GetElementType()!;
            }
            else if (modelClrType.IsGenericType &&
                modelClrType.GetInterfaces().Any(_ => _.IsConstructedGenericType && _.GetGenericTypeDefinition() == typeof(IEnumerable<>)) &&
                modelClrType.GetGenericArguments().First().IsValueObject())
            {
                valueObjectType = modelClrType.GetGenericArguments().First();
            }
            if (valueObjectType != null)
            {
                var attr = (ValueConverterAttribute)valueObjectType.GetCustomAttributes(typeof(ValueConverterAttribute), true).First();
                var arrayValueConverter = (ValueConverter)Activator.CreateInstance(attr.ValueArrayConverterType, (object?)null)!;
                var converterInfo = new ValueConverterInfo(arrayValueConverter.ModelClrType, arrayValueConverter.ProviderClrType, _ => arrayValueConverter);
                return new[] { converterInfo };
            }

            return base.Select(modelClrType, providerClrType);
        }
    }
}
