#nullable enable
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Localization;

namespace FilmHouse.Core.DataAnnotations
{
    /// <summary>
    /// 实施字符数检查的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class PasswordCompareAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        public string OtherProperty { get; }

        /// <summary>
        /// <see cref="StringLengthAttribute"/>
        /// </summary>
        /// <param name="maximumLength">最大文字长度</param>
        public PasswordCompareAttribute(string otherProperty)
        {
            this.OtherProperty = otherProperty;
        }

        /// <summary>
        /// 进行验证处理
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetRuntimeProperty(OtherProperty);
            if (otherPropertyInfo == null)
            {
                return new ValidationResult("The specified comparison object does not exist");
            }
            if (otherPropertyInfo.GetIndexParameters().Length > 0)
            {
                throw new ArgumentException("Multiple comparison objects were found. Procedure");
            }

            if (value == null)
            {
                return null;
            }
            if (value is string)
            {
                return null;
            }

            object? otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
            if (value is IValueObject valueObject && otherPropertyValue is IValueObject otherValueObject)
            {
                if (!Equals(valueObject.AsPrimitiveObject(), otherValueObject.AsPrimitiveObject()))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return null;
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
            return string.Format(Resources.ValidationConfirmPassword);
        }
    }
}
