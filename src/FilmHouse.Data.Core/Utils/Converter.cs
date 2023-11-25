using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Data.Core.Utils
{
    /// <summary>
    /// <see cref="Convert.ChangeType(object, Type)"/>use、<see cref="DateOnly"/>A class with a transformation added.
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object ChangeType(object value, Type conversionType)
        {
            if (value == null)
            {
                return null;
            }
            var type = conversionType.UnwrapNullableType();
            if (type == typeof(DateOnly))
            {
                var stringValue = Convert.ToString(value)!;
                return DateOnly.Parse(stringValue);
            }
            if (type == typeof(TimeOnly))
            {
                var stringValue = Convert.ToString(value)!;
                return TimeOnly.Parse(stringValue);
            }
            if (type == typeof(decimal))
            {
                var stringValue = Convert.ToString(value)!;
                return decimal.Parse(stringValue, NumberStyles.Any);
            }
            return Convert.ChangeType(value, conversionType);
        }
    }
}
