#nullable enable
using System.Collections;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Localization;

namespace FilmHouse.Core.DataAnnotations
{
    /// <summary>
    /// 实施字符数检查的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class StringLengthAttribute : System.ComponentModel.DataAnnotations.StringLengthAttribute
    {
        /// <summary>
        /// <see cref="StringLengthAttribute"/>
        /// </summary>
        /// <param name="maximumLength">最大文字长度</param>
        public StringLengthAttribute(int maximumLength)
            : base(maximumLength)
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
            if (value is string)
            {
                return base.IsValid(value);
            }
            if (value is IValueObject valueObject)
            {
                return base.IsValid(valueObject.AsPrimitiveObject());
            }
            if (value is IEnumerable collection)
            {
                var results = collection.AsEnumerable<object>().Select(_ => this.IsValid(_));
                return results.All(_ => _ == true);
            }
            return base.IsValid(value);
        }

        /// <summary>
        /// 获取验证错误时的错误信息
        /// </summary>
        /// <param name="name">属性名</param>
        /// <returns>错误信息</returns>
        public override string FormatErrorMessage(string name)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage) || !string.IsNullOrEmpty(this.ErrorMessageResourceName))
            {
                return base.FormatErrorMessage(name);
            }
            return string.Format(Resources.ValidationStringLength, name, this.MaximumLength);
        }
    }
}
