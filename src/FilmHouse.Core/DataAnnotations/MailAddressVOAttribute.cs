#nullable enable
using System.ComponentModel.DataAnnotations;
using FilmHouse.Localization;

namespace FilmHouse.Core.DataAnnotations
{
    /// <summary>
    /// 实施邮件地址检查的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    [System.ComponentModel.Description("验证字符串是否为正确的邮件地址形式")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class MailAddressVOAttribute : FilmHouse.Core.DataAnnotations.RegularExpressionAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public MailAddressVOAttribute() : base("^[!-?A-~]+[@][!-?A-~]+$")
        {
        }

        void Validate(object? value, ref bool isValid)
        {
            isValid = base.IsValid(value);
        }

        /// <summary>
        /// 进行验证处理
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (!base.IsValid(value))
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// 进行验证处理
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object? value)
        {
            var isValid = true;
            this.Validate(value, ref isValid);
            return isValid;
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
            return string.Format(Resources.ValidationEMail, name);
        }
    }
}