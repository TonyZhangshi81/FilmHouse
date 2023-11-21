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
    /// This is a converter class that converts bool and string values.
    /// </summary>
    /// <remarks>
    /// String to be converted to true is "true", "1", "-1", "Y".
    /// Characters converted to false are "false", "0", "N".
    /// Strings ignore case.
    /// </remarks>
    public class BooleanConverter : System.ComponentModel.BooleanConverter
    {
        private readonly HashSet<string> _trueStrings = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _falseStrings = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Initialize the instance.
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
        /// Converts from string value to bool value.
        /// </summary>
        /// <param name="context">providing formatting context (IYitypedescriptorcontext)</param>
        /// <param name="culture">Cultureinfo to use as the current culture</param>
        /// <param name="value">String to be converted</param>
        /// <returns>The bool value representing the converted value. <paramref name="value"/>If is null, it is false.</returns>
        /// <exception cref="FormatException"><paramref name="value"/>It is thrown if the format is not convertible.</exception>
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
        /// Using the context specified, it returns a value indicating whether the converter can convert objects of a particular type to the converter's type.
        /// </summary>
        /// <param name="context">Provide formatting context (ITypeDescriptorContext)</param>
        /// <param name="destinationType">The type of the transformation (Type)</param>
        /// <returns>True if the converter can perform conversions. Otherwise, false.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType != null)
            {
                return destinationType == typeof(bool);
            }
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Use the context and culture information specified to convert the specified value object to the specified type.
        /// </summary>
        /// <param name="context">Provide formatting context (ITypeDescriptorContext)</param>
        /// <param name="culture">Cultureinfo object. If a null reference (Nothing in Visual Basic) is passed, the current culture is used.</param>
        /// <param name="value">Bool value to be converted</param>
        /// <param name="destinationType">Value the converted Type of the parameter</param>
        /// <returns>"1" if true, "0" if false</returns>
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
