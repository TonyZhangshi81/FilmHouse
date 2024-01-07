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
using System.Net;

namespace FilmHouse.Core.ValueObjects
{
    /// <summary>
    /// HTTP状态码的值对象类。
    /// </summary>
    [JsonConverter(typeof(HttpStatusCodeVOJsonConverter))]
    [ValueConverter(typeof(HttpStatusCodeVOValueConverter), typeof(HttpStatusCodeVOArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(HttpStatusCodeVOTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class HttpStatusCodeVO : IEquatable<HttpStatusCodeVO>, IComparable<HttpStatusCodeVO>, IFormattable, IConvertible, IValue<int>, IValueObject
    {
        private readonly int _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public const string TypeName = "HttpStatusCode";

        /// <summary>
        /// 取得作为数值的最大位数。
        /// </summary>
        public const int Precision = 3;

        /// <summary>
        /// 获得验证过程中使用的正则表达式的图形字符串。
        /// </summary>
        public const string ValidationRegexPattern = @"^\d+$";

        /// <summary>
        /// 获取值对象包含的原始类型。
        /// </summary>
        public int AsPrimitive() => this._value;
        /// <summary>
        /// 是不依赖句式而获取原始句式的方法。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public object AsPrimitiveObject() => this.AsPrimitive();

        /// <summary>
        /// <see cref="HttpStatusCodeVO"/>是不依赖句式而取得原始句式的方法。
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public HttpStatusCodeVO(int value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }
        public HttpStatusCodeVO(System.Net.HttpStatusCode value)
        {
            var val = (int)value;
            this.PreProcess(ref val);
            this._value = val;
            this.Validate();
        }
        public HttpStatusCodeVO(string value)
        {
            int val = int.Parse(value);
            this.PreProcess(ref val);
            this._value = val;
            this.Validate();
        }

        partial void PreProcess(ref int value);

        partial void Validate();

        /// <summary>
        /// <see cref="int"/>向<see cref="HttpStatusCodeVO"/>对的隐性的角色扮演。
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator int(HttpStatusCodeVO value)
        {
            return value._value;
        }
        public static implicit operator string(HttpStatusCodeVO value)
        {
            return $"{value._value}";
        }

        /// <summary>
        /// <see cref="HttpStatusCodeVO"/>向<see cref="int"/>对的隐性的角色扮演。
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator HttpStatusCodeVO(int value)
        {
            return new HttpStatusCodeVO(value);
        }
        public static implicit operator HttpStatusCodeVO(string value)
        {
            return new HttpStatusCodeVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in HttpStatusCodeVO? x, in HttpStatusCodeVO? y)
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
        /// 对<see cref="int"/>型和包含的原始型进行比较处理。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(HttpStatusCodeVO? other)
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
            if (typeof(HttpStatusCodeVO).IsAssignableFrom(t))
            {
                return Equals((HttpStatusCodeVO)obj);
            }
            if (t == typeof(int))
            {
                return this._value.Equals((int)obj);
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
            return string.Format("{0}", this._value);
        }

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <param name="format">格式字符串</param>
        /// <returns>表示当前对象的字符串</returns>
        public virtual string ToString(string? format) => this.AsPrimitive().ToString(format);

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <param name="format">格式字符串</param>
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
        public static bool operator ==(in HttpStatusCodeVO? x, in HttpStatusCodeVO? y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in HttpStatusCodeVO? x, in HttpStatusCodeVO? y)
        {
            return !Equals(x, y);
        }

        // UnitGenerateOptions.ParseMethod

        /// <summary>
        /// 将字符串形式的值转换为等价的<see cref="HttpStatusCodeVO"/>型。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns><see cref="HttpStatusCodeVO"/>型的值</returns>
        public static HttpStatusCodeVO Parse(string s)
        {
            return new HttpStatusCodeVO(int.Parse(s));
        }

        /// <summary>
        /// 将字符串形式的值转换为等价的<see cref="HttpStatusCodeVO"/>型，返回表示转换成功与否的值。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="result"><see cref="HttpStatusCodeVO"/>型的值</param>
        /// <returns>参数正常转换时为true。除此之外的情况是false。</returns>
        public static bool TryParse(string s, out HttpStatusCodeVO? result)
        {
            if (int.TryParse(s, out var r))
            {
                result = new HttpStatusCodeVO(r);
                return true;
            }
            else
            {
                result = default(HttpStatusCodeVO);
                return false;
            }
        }


        // UnitGenerateOptions.MinMaxMethod

        /// <summary>
        /// 返回小的值
        /// </summary>
        /// <param name="x">初值</param>
        /// <param name="y">第二个值</param>
        /// <returns>参数小的一方</returns>
        public static HttpStatusCodeVO Min(HttpStatusCodeVO x, HttpStatusCodeVO y)
        {
            return new HttpStatusCodeVO(Math.Min(x._value, y._value));
        }

        /// <summary>
        /// 返回大的值
        /// </summary>
        /// <param name="x">初值</param>
        /// <param name="y">第二个值</param>
        /// <returns>参数大的一方</returns>
        public static HttpStatusCodeVO Max(HttpStatusCodeVO x, HttpStatusCodeVO y)
        {
            return new HttpStatusCodeVO(Math.Max(x._value, y._value));
        }



        // UnitGenerateOptions.ValueArithmeticOperator

        /// <summary>
        /// 递增运算符
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static HttpStatusCodeVO operator ++(in HttpStatusCodeVO x)
        {
            checked
            {
                return new HttpStatusCodeVO((int)(x._value + 1));
            }
        }

        /// <summary>
        /// 减缩运算符
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static HttpStatusCodeVO operator --(in HttpStatusCodeVO x)
        {
            checked
            {
                return new HttpStatusCodeVO((int)(x._value - 1));
            }
        }

        /// <summary>
        /// 加法运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static HttpStatusCodeVO operator +(in HttpStatusCodeVO x, in int y)
        {
            checked
            {
                return new HttpStatusCodeVO((int)(x._value + y));
            }
        }

        /// <summary>
        /// 减法运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static HttpStatusCodeVO operator -(in HttpStatusCodeVO x, in int y)
        {
            checked
            {
                return new HttpStatusCodeVO((int)(x._value - y));
            }
        }

        /// <summary>
        /// 乘法运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static HttpStatusCodeVO operator *(in HttpStatusCodeVO x, in int y)
        {
            checked
            {
                return new HttpStatusCodeVO((int)(x._value * y));
            }
        }

        /// <summary>
        /// 除法运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static HttpStatusCodeVO operator /(in HttpStatusCodeVO x, in int y)
        {
            checked
            {
                return new HttpStatusCodeVO((int)(x._value / y));
            }
        }


        // UnitGenerateOptions.Comparable

        /// <summary>
        /// 将这个实例与<paramref name="other"/>进行比较。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(HttpStatusCodeVO? other)
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
        public static bool operator >(in HttpStatusCodeVO x, in HttpStatusCodeVO y)
        {
            return x._value > y._value;
        }

        /// <summary>
        /// 小于运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <(in HttpStatusCodeVO x, in HttpStatusCodeVO y)
        {
            return x._value < y._value;
        }

        /// <summary>
        /// 大于等于运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >=(in HttpStatusCodeVO x, in HttpStatusCodeVO y)
        {
            return x._value >= y._value;
        }

        /// <summary>
        /// 小于等于运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <=(in HttpStatusCodeVO x, in HttpStatusCodeVO y)
        {
            return x._value <= y._value;
        }


        // UnitGenerateOptions.JsonConverter
        private class HttpStatusCodeVOJsonConverter : JsonConverter<HttpStatusCodeVO>
        {
            public override void Write(Utf8JsonWriter writer, HttpStatusCodeVO value, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(int)) as JsonConverter<int>;
                if (converter != null)
                {
                    converter.Write(writer, value._value, options);
                }
                else
                {
                    throw new JsonException($"{typeof(int)} converter does not found.");
                }
            }

            public override HttpStatusCodeVO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(int)) as JsonConverter<int>;
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
                            var typeConverter = TypeDescriptor.GetConverter(typeof(HttpStatusCodeVO));
                            return (HttpStatusCodeVO?)(stringValue == null ? null : typeConverter.ConvertFrom(stringValue));
                        }

                        var value = converter.Read(ref reader, typeToConvert, options);
                        return new HttpStatusCodeVO(value);
                    }
                    catch (Exception exception)
                    {
                        throw options.GetInvalidValueException(ref reader, typeof(int), exception);
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
        public class HttpStatusCodeVOValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<HttpStatusCodeVO?, int?>
        {
            /// <summary>
            /// <see cref="HttpStatusCodeVOValueConverter"/>是不依赖句式而取得原始句式的方法。
            /// </summary>
            public HttpStatusCodeVOValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="HttpStatusCodeVOValueConverter"/>是不依赖句式而取得原始句式的方法。
            /// </summary>
            /// <param name="mappingHints"></param>
            public HttpStatusCodeVOValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new HttpStatusCodeVO(x.Value) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                int value => value,
                HttpStatusCodeVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                HttpStatusCodeVO value => value,
                int value => new HttpStatusCodeVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class HttpStatusCodeVOArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<HttpStatusCodeVO?[], int?[]>
        {
            /// <summary>
            /// <see cref="HttpStatusCodeVOArrayValueConverter"/>是不依赖句式而取得原始句式的方法。
            /// </summary>
            public HttpStatusCodeVOArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="HttpStatusCodeVOArrayValueConverter"/>是不依赖句式而取得原始句式的方法。
            /// </summary>
            /// <param name="mappingHints"></param>
            public HttpStatusCodeVOArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (int?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new HttpStatusCodeVO(_.Value)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                int?[] values => values,
                HttpStatusCodeVO?[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<int?> values => values.ToArray(),
                IEnumerable<HttpStatusCodeVO?> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                HttpStatusCodeVO?[] values => values,
                int?[] values => values.Select(_ => _ == null ? null : new HttpStatusCodeVO(_.Value)).ToArray(),
                IEnumerable<HttpStatusCodeVO?> values => values.ToArray(),
                IEnumerable<int?> values => values.Select(_ => _ == null ? null : new HttpStatusCodeVO(_.Value)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class HttpStatusCodeVOTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(HttpStatusCodeVO);
            private static readonly Type ValueType = typeof(int);
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
                if (t == typeof(HttpStatusCodeVO))
                {
                    return (HttpStatusCodeVO)value;
                }
                if (t == typeof(int))
                {
                    return new HttpStatusCodeVO((int)value);
                }
                if (t == typeof(string))
                {
                    return new HttpStatusCodeVO(int.Parse((string)value));
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is HttpStatusCodeVO wrappedValue)
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
