#nullable enable
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects.Serialization;

namespace FilmHouse.Core.ValueObjects
{
    /// <summary>
    /// 评分（3位整数1位小数）的值对象类。
    /// </summary>
    [JsonConverter(typeof(RatingJsonConverter))]
    [ValueConverter(typeof(RatingValueConverter), typeof(RatingArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(RatingTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class RatingVO : IRounding<RatingVO>, IEquatable<RatingVO>, IComparable<RatingVO>, IFormattable, IConvertible, IValue<decimal>, IValueObject
    {
        private readonly decimal _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public const string TypeName = "Rating";

        /// <summary>
        /// 取得作为数值的最大位数。
        /// </summary>
        public const int Precision = 4;
        /// <summary>
        /// 取得小数点以下的位数作为数值。
        /// </summary>
        public const int Scale = 1;
        /// <summary>
        /// 取得显示格式。
        /// </summary>
        public const string DisplayFormat = @"{0:##0.0}";

        /// <summary>
        /// 获取值对象包含的原始类型。
        /// </summary>
        public decimal AsPrimitive() => this._value;
        /// <summary>
        /// 是不依赖句式而获取原始句式的方法。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public object AsPrimitiveObject() => this.AsPrimitive();

        /// <summary>
        /// <see cref="RatingVO"/>的新实例。
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public RatingVO(decimal value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        partial void PreProcess(ref decimal value);

        partial void Validate();

        /// <summary>
        /// <see cref="decimal"/>向<see cref="RatingVO"/>对的隐性的角色扮演。
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator decimal(RatingVO value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="RatingVO"/>向<see cref="decimal"/>对的隐性的角色扮演。
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator RatingVO(decimal value)
        {
            return new RatingVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in RatingVO? x, in RatingVO? y)
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
        /// <see cref="decimal"/>对句式和包含的原始句式进行比较处理。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RatingVO? other)
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
            if (typeof(RatingVO).IsAssignableFrom(t))
            {
                return Equals((RatingVO)obj);
            }
            if (t == typeof(decimal))
            {
                return this._value.Equals((decimal)obj);
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
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <param name="format">字符串</param>
        /// <returns>表示当前对象的字符串</returns>
        public virtual string ToString(string? format) => this.AsPrimitive().ToString(format);

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <param name="format">字符串</param>
        /// <param name="provider">用于设定值格式的提供商</param>
        /// <returns>表示当前对象的字符串</returns>
        public virtual string ToString(string? format, IFormatProvider? provider) => this.AsPrimitive().ToString(format, provider);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TypeCode GetTypeCode() => this.AsPrimitive().GetTypeCode();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToBoolean(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToByte(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToChar(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToDateTime(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToDecimal(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToDouble(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToInt16(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToInt32(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToInt64(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToSByte(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToSingle(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        string IConvertible.ToString(IFormatProvider? provider) => this.AsPrimitive().ToString(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversionType"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToType(conversionType, provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToUInt16(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToUInt32(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)this.AsPrimitive()).ToUInt64(provider);

        /// <summary>
        /// 是否相等
        /// </summary>
        public static bool operator ==(in RatingVO? x, in RatingVO? y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in RatingVO? x, in RatingVO? y)
        {
            return !Equals(x, y);
        }

        // UnitGenerateOptions.ParseMethod

        /// <summary>
        /// 将字符串形式的值转换为等价的<see cref="RatingVO"/>类型。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns><see cref="RatingVO"/>型的值</returns>
        public static RatingVO Parse(string s)
        {
            return new RatingVO(decimal.Parse(s));
        }

        /// <summary>
        /// 将字符串形式的值转换为等价的<see cref="RatingVO"/>类型，并返回指示转换是否成功的值。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="result"><see cref="RatingVO"/>型的值</param>
        /// <returns>参数正常转换时为true。除此之外的情况是false。</returns>
        public static bool TryParse(string s, out RatingVO? result)
        {
            if (decimal.TryParse(s, out var r))
            {
                result = new RatingVO(r);
                return true;
            }
            else
            {
                result = default(RatingVO);
                return false;
            }
        }


        // UnitGenerateOptions.MinMaxMethod

        /// <summary>
        /// 返回小的值
        /// </summary>
        /// <param name="x">第一个值</param>
        /// <param name="y">第二个值</param>
        /// <returns>参数小的一方</returns>
        public static RatingVO Min(RatingVO x, RatingVO y)
        {
            return new RatingVO(Math.Min(x._value, y._value));
        }

        /// <summary>
        /// 返回大的值
        /// </summary>
        /// <param name="x">第一个值</param>
        /// <param name="y">第二个值</param>
        /// <returns>参数大的一方</returns>
        public static RatingVO Max(RatingVO x, RatingVO y)
        {
            return new RatingVO(Math.Max(x._value, y._value));
        }



        // UnitGenerateOptions.ValueArithmeticOperator

        /// <summary>
        /// 递增运算符
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static RatingVO operator ++(in RatingVO x)
        {
            checked
            {
                return new RatingVO((decimal)(x._value + 1));
            }
        }

        /// <summary>
        /// 递减运算符
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static RatingVO operator --(in RatingVO x)
        {
            checked
            {
                return new RatingVO((decimal)(x._value - 1));
            }
        }

        /// <summary>
        /// 加法运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static RatingVO operator +(in RatingVO x, in decimal y)
        {
            checked
            {
                return new RatingVO((decimal)(x._value + y));
            }
        }

        /// <summary>
        /// 减法运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static RatingVO operator -(in RatingVO x, in decimal y)
        {
            checked
            {
                return new RatingVO((decimal)(x._value - y));
            }
        }

        /// <summary>
        /// 乘法运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static RatingVO operator *(in RatingVO x, in decimal y)
        {
            checked
            {
                return new RatingVO((decimal)(x._value * y));
            }
        }

        /// <summary>
        /// 除法运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static RatingVO operator /(in RatingVO x, in decimal y)
        {
            checked
            {
                return new RatingVO((decimal)(x._value / y));
            }
        }


        // UnitGenerateOptions.Comparable

        /// <summary>
        /// 将这个实例与<paramref name="other"/>进行比较。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(RatingVO? other)
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
        public static bool operator >(in RatingVO x, in RatingVO y)
        {
            return x._value > y._value;
        }

        /// <summary>
        /// 小于运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <(in RatingVO x, in RatingVO y)
        {
            return x._value < y._value;
        }

        /// <summary>
        /// 大于等于运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >=(in RatingVO x, in RatingVO y)
        {
            return x._value >= y._value;
        }

        /// <summary>
        /// 小于等于运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <=(in RatingVO x, in RatingVO y)
        {
            return x._value <= y._value;
        }


        // UnitGenerateOptions.JsonConverter
        private class RatingJsonConverter : JsonConverter<RatingVO>
        {
            public override void Write(Utf8JsonWriter writer, RatingVO value, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(decimal)) as JsonConverter<decimal>;
                if (converter != null)
                {
                    converter.Write(writer, value._value, options);
                }
                else
                {
                    throw new JsonException($"{typeof(decimal)} converter does not found.");
                }
            }

            public override RatingVO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(decimal)) as JsonConverter<decimal>;
                if (converter != null)
                {
                    try
                    {
                        if (reader.TokenType == JsonTokenType.String && (JsonNumberHandling.AllowReadingFromString & options.NumberHandling) != 0)
                        {
                            var stringConverter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                            if (stringConverter == null)
                            {
                                throw options.GetConvertFailureException(typeToConvert);
                            }
                            var stringValue = stringConverter.Read(ref reader, typeToConvert, options);
                            var typeConverter = TypeDescriptor.GetConverter(typeof(RatingVO));
                            return (RatingVO?)(stringValue == null ? null : typeConverter.ConvertFrom(stringValue));
                        }

                        var value = converter.Read(ref reader, typeToConvert, options);
                        return new RatingVO(value);
                    }
                    catch (Exception exception)
                    {
                        throw options.GetInvalidValueException(ref reader, typeof(decimal), exception);
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
        public class RatingValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<RatingVO?, decimal?>
        {
            /// <summary>
            /// <see cref="RatingValueConverter"/>的新实例。
            /// </summary>
            public RatingValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="RatingValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public RatingValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new RatingVO(x.Value) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                decimal value => value,
                RatingVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// 当读取存储数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                RatingVO value => value,
                decimal value => new RatingVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class RatingArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<RatingVO?[], decimal?[]>
        {
            /// <summary>
            /// <see cref="RatingArrayValueConverter"/>的新实例。
            /// </summary>
            public RatingArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="RatingArrayValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public RatingArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (decimal?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new RatingVO(_.Value)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                decimal?[] values => values,
                RatingVO?[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<decimal?> values => values.ToArray(),
                IEnumerable<RatingVO?> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// 当读取存储数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                RatingVO?[] values => values,
                decimal?[] values => values.Select(_ => _ == null ? null : new RatingVO(_.Value)).ToArray(),
                IEnumerable<RatingVO?> values => values.ToArray(),
                IEnumerable<decimal?> values => values.Select(_ => _ == null ? null : new RatingVO(_.Value)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class RatingTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(RatingVO);
            private static readonly Type ValueType = typeof(decimal);
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
                if (t == typeof(RatingVO))
                {
                    return (RatingVO)value;
                }
                if (t == typeof(decimal))
                {
                    return new RatingVO((decimal)value);
                }
                if (t == typeof(string))
                {
                    return new RatingVO(decimal.Parse((string)value));
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is RatingVO wrappedValue)
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
