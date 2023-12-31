#nullable enable
using System.Collections;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.DataAnnotations
{
    /// <summary>
    /// 实施日期检查的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public partial class DateAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        /// <summary>
        /// 验证处理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }
            if (value is IValue<DateTime> || value is IValue<DateOnly> ||
                value is DateTimeValueObjectBase || value is DateOnlyValueObjectBase ||
                value is DateTime || value is DateOnly)
            {
                return true;
            }
            if (value is string stringVal)
            {
                return DateTime.TryParse(stringVal, out var _) || DateOnly.TryParse(stringVal, out var _);
            }
            if (value is IEnumerable collection)
            {
                var results = collection.AsEnumerable<object>().Select(_ => this.IsValid(_));
                return results.All(_ => _ == true);
            }
            return false;
        }
    }
}