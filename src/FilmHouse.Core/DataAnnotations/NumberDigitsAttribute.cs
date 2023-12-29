#nullable enable
using System.Collections;
using System.Globalization;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.DataAnnotations
{
    /// <summary>
    /// 是为了实施数值的位数检查的属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public partial class NumberDigitsAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        /// <summary>
        /// <see cref="NumberDigitsAttribute"/>
        /// </summary>
        /// <param name="precision">整体精度(位数)</param>
        public NumberDigitsAttribute(int precision)
        {
            this.Precision = precision;
        }

        /// <summary>
        /// <see cref="NumberDigitsAttribute"/>
        /// </summary>
        /// <param name="precision">整体精度(位数)</param>
        /// <param name="scale">小数点以下的位数</param>
        public NumberDigitsAttribute(int precision, int scale)
            : this(precision)
        {
            this.Scale = scale;
        }

        /// <summary>
        /// 获得整体精度。
        /// </summary>
        public int Precision { get; }
        /// <summary>
        /// 取得小数点以下的位数。
        /// </summary>
        public int Scale { get; } = 0;

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
                value = valueObject.AsPrimitiveObject();
            }
            if (value is string stringValue)
            {
                value = stringValue;
            }
            else if (value is IEnumerable collection)
            {
                var results = collection.AsEnumerable<object>().Select(_ => this.IsValid(_));
                return results.All(_ => _ == true);
            }
            if (!decimal.TryParse($"{value}", NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalVal))
            {
                return false;
            }
            value = decimalVal;

            // 为了确认位数转换成字符串
            var d = (decimal)Converter.ChangeType(value, typeof(decimal))!;
            // 转换成绝对值来去除符号
            d = Math.Abs(d);
            var decimalString = string.Format(@"{0:############################0.#############################}", d);

            // 整体精度验证
            // 除去小数点的位数
            var precision = decimalString.Replace(".", "").Length;
            if (this.Precision < precision)
            {
                // 精度超标
                return false;
            }

            var index = decimalString.IndexOf('.');
            {
                // 整数位验证
                var integerDigit = precision;
                if (index > 0)
                {
                    integerDigit = index - 1;
                }
                if (this.Precision - this.Scale < integerDigit)
                {
                    return false;
                }
            }
            {
                // 小数部分的验证
                if (index < 0)
                {
                    return true;
                }
                var scale = precision - index;
                if (this.Scale < scale)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
