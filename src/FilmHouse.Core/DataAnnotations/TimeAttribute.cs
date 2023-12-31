#nullable enable
using System.Collections;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.DataAnnotations
{
    /// <summary>
    /// 实施时刻检查的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public partial class TimeAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        /// <summary>
        /// 进行验证处理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }
            if (value is IValue<TimeOnly> ||
                value is TimeOnlyValueObjectBase ||
                value is TimeOnly)
            {
                return true;
            }
            if (value is string stringVal)
            {
                return TimeOnly.TryParse(stringVal, out var _);
            }
            if (value is IValue<string> sv)
            {
                return TimeOnly.TryParse(sv.AsPrimitive(), out var _);
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