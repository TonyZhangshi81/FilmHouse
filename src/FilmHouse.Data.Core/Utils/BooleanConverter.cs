using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Data.Core.Utils
{
    /// <summary>
    /// 这是一个转换bool值和字符串值的转换器类。
    /// </summary>
    /// <remarks>
    /// 要转换为true的字符串是 "true", "1", "-1", "Y".
    /// 转换为false的字符有 "false", "0", "N".
    /// Strings ignore case.
    /// </remarks>
    public class BooleanConverter : System.ComponentModel.BooleanConverter
    {
        // 匹配时大小写不明感
        private readonly HashSet<string> _trueStrings = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _falseStrings = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 初始化实例。
        /// </summary>
        public BooleanConverter()
        {
            this._trueStrings.Add(bool.TrueString);
            this._trueStrings.Add("1");
            this._trueStrings.Add("-1");
            this._trueStrings.Add("Y");

            this._falseStrings.Add(bool.FalseString);
            this._falseStrings.Add("0");
            this._falseStrings.Add("N");
        }

        /// <summary>
        /// 将字符串值转换为bool值。
        /// </summary>
        /// <param name="context">提供格式化上下文(ITypeDescriptorContext)</param>
        /// <param name="culture">Cultureinfo用作当前区域性</param>
        /// <param name="value">要转换的字符串</param>
        /// <returns>表示转换值的bool值。<paramref name="value"/>如果为空，则为假。</returns>
        /// <exception cref="FormatException"><paramref name="value"/>如果格式不可转换，则抛出该函数。</exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                if (value == null)
                {
                    return false;
                }

                if (this._trueStrings.Contains($"{value}"))
                {
                    return true;
                }
                if (this._falseStrings.Contains($"{value}"))
                {
                    return false;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// 使用指定的上下文，它返回一个值，该值指示转换器是否可以将特定类型的对象转换为转换器的类型。
        /// </summary>
        /// <param name="context">提供格式化上下文(ITypeDescriptorContext)</param>
        /// <param name="destinationType">转换的类型(type)</param>
        /// <returns>如果转换器可以执行转换，则为True。否则,假的。</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType != null)
            {
                return destinationType == typeof(bool);
            }
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// 使用指定的上下文和区域性信息将指定的值对象转换为指定的类型。
        /// </summary>
        /// <param name="context">提供格式化上下文(ITypeDescriptorContext)</param>
        /// <param name="culture"祝辞的Cultureinfo对象。如果传递空引用(Visual Basic中为Nothing)，则使用当前区域性。</param>
        /// <param name="value">要转换的Bool值</param>
        /// <param name="destinationType">Value转换后的参数类型</param>
        /// <returns>true为“1”，false为“0”</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value == null)
            {
                return null;
            }
            if (value is bool && destinationType == typeof(string))
            {
                if ((bool)value)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
