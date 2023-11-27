#nullable enable
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects.Serialization;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.ValueObjects
{
    /// <summary>
    /// 表示日期的值对象类。
    /// </summary>
    [JsonConverter(typeof(DateJsonConverter))]
    [ValueConverter(typeof(DateValueConverter), typeof(DateArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(DateTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class Date : FilmHouse.Core.ValueObjects.DateOnlyValueObjectBase, IEquatable<Date>, IComparable<Date>, IValue<System.DateOnly>, IValueObject
    {
        private readonly System.DateOnly _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public const string TypeName = "日期";

        /// <summary>
        /// 取得显示格式。
        /// </summary>
        public const string DisplayFormat = @"{0:yyyy/MM/dd}";

        /// <summary>
        /// 获取值对象包含的原始类型。
        /// </summary>
        public System.DateOnly AsPrimitive() => this._value;
        /// <summary>
        /// 是不依赖句式而获取原始句式的方法。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public object AsPrimitiveObject() => this.AsPrimitive();

        /// <summary>
        /// <see cref="Date"/>的新实例。
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public Date(System.DateOnly value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        /// <summary>
        /// <see cref="Date"/>的新实例。
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public Date(DateTime value)
            : this(DateOnly.FromDateTime(value))
        {
        }

        partial void PreProcess(ref System.DateOnly value);

        partial void Validate();

        /// <summary>
        /// <see cref="System.DateOnly"/>向<see cref="Date"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator System.DateOnly(Date value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="Date"/>向<see cref="System.DateOnly"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Date(System.DateOnly value)
        {
            return new Date(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in Date? x, in Date? y)
        {
            if (x is null && y is null)
            {
                return true;
            }
            if ((x is not null && y is null) || (x is null && y is not null))
            {
                return false;
            }
            return x!._value.Equals(y!._value);
        }

        /// <summary>
        /// 对<see cref="System.DateOnly"/>型和包含的原始型进行比较处理。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Date? other)
        {
            return Equals(this, other);
        }

        /// <summary>
        /// 判断对象之间是否一致。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            var t = obj.GetType();
            if (typeof(Date).IsAssignableFrom(t))
            {
                return Equals((Date)obj);
            }
            if (t == typeof(System.DateOnly))
            {
                return this._value.Equals((System.DateOnly)obj);
            }

            return this._value.Equals(obj);
        }

        /// <summary>
        /// 作为既定的散列函数发挥作用。
        /// </summary>
        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        public override string ToString()
        {
            return string.Format(DisplayFormat, this._value);
        }



        /// <summary>
        /// 是否相等
        /// </summary>
        public static bool operator ==(in Date? x, in Date? y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in Date? x, in Date? y)
        {
            return !Equals(x, y);
        }

        // UnitGenerateOptions.ParseMethod

        /// <summary>
        /// 将字符串形式的值转换为等价的<see cref="Date"/>型。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns><see cref="Date"/>字符串</returns>
        public static Date Parse(string s)
        {
            return new Date(System.DateOnly.Parse(s));
        }

        /// <summary>
        /// 将字串形式的值转换为等价的<see cref="Date"/>型，返回表示转换是否成功的值。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="result"><see cref="Date"/>字符串</param>
        /// <returns>参数正常转换时为true。除此之外的情况是false。</returns>
        public static bool TryParse(string s, out Date? result)
        {
            if (System.DateOnly.TryParse(s, out var r))
            {
                result = new Date(r);
                return true;
            }
            else
            {
                result = default(Date);
                return false;
            }
        }






        // UnitGenerateOptions.Comparable

        /// <summary>
        /// 将该实例<paramref name="other" />和比较。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Date? other)
        {
            if (other == null)
            {
                return 1;
            }
            return this._value.CompareTo(other._value);
        }

        /// <summary>
        /// 大于运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >(in Date x, in Date y)
        {
            return x._value > y._value;
        }

        /// <summary>
        /// 小于运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <(in Date x, in Date y)
        {
            return x._value < y._value;
        }

        /// <summary>
        /// 大于等于运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >=(in Date x, in Date y)
        {
            return x._value >= y._value;
        }

        /// <summary>
        /// 小于等于运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <=(in Date x, in Date y)
        {
            return x._value <= y._value;
        }


        // UnitGenerateOptions.JsonConverter
        private class DateJsonConverter : JsonConverter<Date>
        {
            public override void Write(Utf8JsonWriter writer, Date value, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(System.DateOnly)) as JsonConverter<System.DateOnly>;
                if (converter != null)
                {
                    converter.Write(writer, value._value, options);
                }
                else
                {
                    throw new JsonException($"{typeof(System.DateOnly)} converter does not found.");
                }
            }

            public override Date? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(System.DateOnly)) as JsonConverter<System.DateOnly>;
                if (converter != null)
                {
                    try
                    {
                        var value = converter.Read(ref reader, typeToConvert, options);
                        return new Date(value);
                    }
                    catch (Exception exception)
                    {
                        throw options.GetInvalidValueException(ref reader, typeof(System.DateOnly), exception);
                    }
                }
                else
                {
                    throw options.GetConvertFailureException(typeToConvert);
                }
            }
        }




        // UnitGenerateOptions.EntityFrameworkValueConverter
        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class DateValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<Date?, System.DateOnly?>
        {
            /// <summary>
            /// <see cref="DateValueConverter"/>的新实例。
            /// </summary>
            public DateValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="DateValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public DateValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new Date(x.Value) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                System.DateOnly value => value,
                Date value => value._value,
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                Date value => value,
                System.DateOnly value => new Date(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class DateArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<Date?[], System.DateOnly?[]>
        {
            /// <summary>
            /// <see cref="DateArrayValueConverter"/>的新实例。
            /// </summary>
            public DateArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="DateArrayValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public DateArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (System.DateOnly?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new Date(_.Value)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                System.DateOnly?[] values => values,
                Date?[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<System.DateOnly?> values => values.ToArray(),
                IEnumerable<Date?> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                Date?[] values => values,
                System.DateOnly?[] values => values.Select(_ => _ == null ? null : new Date(_.Value)).ToArray(),
                IEnumerable<Date?> values => values.ToArray(),
                IEnumerable<System.DateOnly?> values => values.Select(_ => _ == null ? null : new Date(_.Value)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class DateTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(Date);
            private static readonly Type ValueType = typeof(System.DateOnly);
            private static readonly Type BindingValueType = typeof(string);

            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, Type sourceType)
            {
                if (sourceType == WrapperType || sourceType == ValueType || sourceType == BindingValueType)
                {
                    return true;
                }

                return base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext? context, Type? destinationType)
            {
                if (destinationType != null)
                {
                    if (destinationType == WrapperType || destinationType == ValueType || destinationType == BindingValueType)
                    {
                        return true;
                    }
                }

                return base.CanConvertTo(context, destinationType);
            }

            public override object? ConvertFrom(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
            {
                var t = value.GetType();
                if (t == typeof(Date))
                {
                    return (Date)value;
                }
                if (t == typeof(System.DateOnly))
                {
                    return new Date((System.DateOnly)value);
                }
                if (t == typeof(string))
                {
                    return new Date(System.DateOnly.Parse((string)value));
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is Date wrappedValue)
                {
                    if (destinationType == WrapperType)
                    {
                        return wrappedValue;
                    }

                    if (destinationType == ValueType)
                    {
                        return wrappedValue.AsPrimitive();
                    }

                    if (destinationType == BindingValueType)
                    {
                        return $"{wrappedValue.AsPrimitive()}";
                    }
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
