#nullable enable
using System.Collections;
using System.Globalization;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.DataAnnotations
{
    /// <summary>
    /// 实施数值检查的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public partial class NumberAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        /// <summary>
        /// 进行验证处理。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }
            // 对于值对象，获取原始类型进行判断
            if (value is IValueObject valueObject)
            {
                return this.IsValid(valueObject.AsPrimitiveObject());
            }
            // 文字的情况下根据是否可以转换为decimal来判断
            if (value is string stringValue)
            {
                return decimal.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal _);
            }
            // 在收集的情况下,对一个值进行验证
            if (value is IEnumerable collection)
            {
                var results = collection.AsEnumerable<object>().Select(_ => this.IsValid(_));
                return results.All(_ => _ == true);
            }

            // 不符合上述处理的情况下转换为文字型进行验证
            return this.IsValid($"{value}");
        }
    }
}