#nullable enable
using System.Collections;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Localization;

namespace FilmHouse.Core.DataAnnotations
{
    /// <summary>
    /// 实施通用的正则表达式检查的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RegularExpressionAttribute : System.ComponentModel.DataAnnotations.RegularExpressionAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern">验证时的正规表现字符串</param>
        public RegularExpressionAttribute(string pattern) : base(pattern)
        {
        }

        /// <summary>
        /// 进行验证处理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return base.IsValid(value);
            }
            if (value is IValueObject valueObject)
            {
                return base.IsValid(valueObject.AsPrimitiveObject());
            }
            if (value is not string && value is IEnumerable collection)
            {
                var results = collection.AsEnumerable<object>().Select(_ => this.IsValid(_));
                return results.All(_ => _ == true);
            }
            return base.IsValid(value);
        }
    }
}
