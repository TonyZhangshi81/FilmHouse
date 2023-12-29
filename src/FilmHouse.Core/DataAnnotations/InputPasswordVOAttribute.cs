#nullable enable
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Localization;

namespace FilmHouse.Core.DataAnnotations
{
    /// <summary>
    /// 实施输入密码检查的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    [System.ComponentModel.Description("位数在8位以上，并且由英文大写字母、英文小写字母、数字、符号中最少3个以上组合而成。※记号如下所示!# $ % & ' () * +, - . /:;&爱尔蒂;= & gt;?@ [\\ \\] ^ _ ` {|} ~")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class InputPasswordVOAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        /// <summary>
        /// 正则表达式模式(其中3种使用)
        /// </summary>
        public static readonly Regex[] StringPattern = { new("[a-z]"), new("[A-Z]"), new("[0-9]"), new(@"[!#$%&'()*+,\-./:;<=>?@[\]^_`{|}~\\]") };

        /// <summary>
        /// 
        /// </summary>
        public Regex[] StringPatternProperty { get => StringPattern; }

        /// <summary>
        /// 正则表达式的匹配数
        /// </summary>
        public static readonly int RegularCount = 3;

        /// <summary>
        /// 
        /// </summary>
        public int RegularCountProperty { get => RegularCount; }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public static readonly Regex RegularPattern = new(@"^[a-zA-Z0-9!#$%&'()*+,\-./:;<=>?@[\]^_`{|}~\\]+$");

        /// <summary>
        /// 
        /// </summary>
        public Regex RegularPatternProperty { get => RegularPattern; }

        /// <summary>
        /// 最小文字长度
        /// </summary>
        public static readonly int MinLength = 8;

        /// <summary>
        /// 
        /// </summary>
        public int MinLengthProperty { get => MinLength; }

        /// <summary>
        /// 进行验证处理
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            if (value is IValueObject valueObject)
            {
                value = valueObject.AsPrimitiveObject();
            }
            if (value is string stringValue)
            {
                if (StringPattern.Count(_ => _.IsMatch(stringValue)) < RegularCount || stringValue.Length < MinLength || !RegularPattern.IsMatch(stringValue))
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            if (value is IEnumerable collection)
            {
                var results = collection.AsEnumerable<object>().Select(_ => this.IsValid(_, validationContext));
                if (results.All(_ => _ == ValidationResult.Success))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
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
            return string.Format(Resources.ValidationPassword, name);
        }
    }
}