#nullable enable
using System.Collections;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Localization;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace FilmHouse.Core.DataAnnotations
{
    /// <summary>
    /// 实施必须检查的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
    {
        public RequiredAttribute()
        {
            // 允许空白
            base.AllowEmptyStrings = false;
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
                return this.IsValid(valueObject.AsPrimitiveObject());
            }
            if (value is string strinValue)
            {
                if (string.IsNullOrEmpty(strinValue))
                {
                    return false;
                }
                return base.IsValid(value);
            }
            if (value is IEnumerable collection)
            {
                return collection.AsEnumerable<object>().Any();
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
            return string.Format(Resources.ValidationRequired, name);
        }
    }
}
