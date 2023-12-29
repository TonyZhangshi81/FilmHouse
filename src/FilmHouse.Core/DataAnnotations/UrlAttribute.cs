#nullable enable
using System.Collections;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.DataAnnotations
{
    /// <summary>
    /// 实施URL检查的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    [System.ComponentModel.Description("根据RFC3986和RFC3987验证正确的URI。uri.iswellformeduristring()上进行验证。")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class UrlAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        /// <summary>
        /// 实现验证处理的分离方法。验证失败的情况下排除例外
        /// </summary>
        void Validate(object? value, ref bool isValid)
        {
            if (value == null)
            {
                return;
            }
            if (value is string stringValue)
            {
                isValid &= Uri.IsWellFormedUriString(stringValue, UriKind.RelativeOrAbsolute);
            }
            if (value is IValue<string> sv)
            {
                isValid &= Uri.IsWellFormedUriString(sv.AsPrimitive(), UriKind.RelativeOrAbsolute);
            }
            if (value is IEnumerable collection)
            {
                foreach (var val in collection)
                {
                    this.Validate(val, ref isValid);
                }
            }
        }

        /// <summary>
        /// 进行验证处理
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>验证结果</returns>
        public override bool IsValid(object? value)
        {
            var isValid = true;
            this.Validate(value, ref isValid);
            return isValid;
        }
    }
}