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

namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// 评价状态的值对象类。进行与原始型的隐性分配。
    /// </summary>
    [JsonConverter(typeof(MarkTypeJsonConverter))]
    [ValueConverter(typeof(MarkTypeValueConverter), typeof(MarkTypeArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(MarkTypeTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class MarkTypeVO : IEquatable<MarkTypeVO>, IComparable<MarkTypeVO>, IFormattable, IConvertible, IValue<int>, IValueObject
    {
        private readonly int _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public const string TypeName = "MarkType";

        /// <summary>
        /// 取得作为数值的最大位数。
        /// </summary>
        public const int Precision = 1;

        /// <summary>
        /// 提供代码组持有的代码值的常数定义。
        /// </summary>
        public static class Codes
        {
            /// <summary>
            /// 「0:默认值」
            /// </summary>
            public static readonly MarkTypeVO MarkTypeCode0 = new(0);
            /// <summary>
            /// 「1:想看电影」
            /// </summary>
            public static readonly MarkTypeVO MarkTypeCode1 = new(1);
            /// <summary>
            /// 「2:看过电影」
            /// </summary>
            public static readonly MarkTypeVO MarkTypeCode2 = new(2);
            /// <summary>
            /// 「3:喜欢电影」
            /// </summary>
            public static readonly MarkTypeVO MarkTypeCode3 = new(3);
            /// <summary>
            /// 「4:收藏影人」
            /// </summary>
            public static readonly MarkTypeVO MarkTypeCode4 = new(4);
            /// <summary>
            /// 「5:赞资源」
            /// </summary>
            public static readonly MarkTypeVO MarkTypeCode5 = new(5);
            /// <summary>
            /// 「6:同求资源」
            /// </summary>
            public static readonly MarkTypeVO MarkTypeCode6 = new(6);
            /// <summary>
            /// 「7:关注专辑」
            /// </summary>
            public static readonly MarkTypeVO MarkTypeCode7 = new(7);
        }

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
        /// <see cref="MarkTypeVO"/>是不依赖句式而取得原始句式的方法。
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public MarkTypeVO(int value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        partial void PreProcess(ref int value);

        partial void Validate();

        /// <summary>
        /// <see cref="int"/>向<see cref="MarkTypeVO"/>对的隐性的角色扮演。
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator int(MarkTypeVO value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="MarkTypeVO"/>向<see cref="int"/>对的隐性的角色扮演。
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator MarkTypeVO(int value)
        {
            return new MarkTypeVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in MarkTypeVO? x, in MarkTypeVO? y)
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
        public bool Equals(MarkTypeVO? other)
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
            if (typeof(MarkTypeVO).IsAssignableFrom(t))
            {
                return Equals((MarkTypeVO)obj);
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
        public static bool operator ==(in MarkTypeVO? x, in MarkTypeVO? y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in MarkTypeVO? x, in MarkTypeVO? y)
        {
            return !Equals(x, y);
        }

        // UnitGenerateOptions.ParseMethod

        /// <summary>
        /// 将字符串形式的值转换为等价的<see cref="MarkTypeVO"/>型。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns><see cref="MarkTypeVO"/>型的值</returns>
        public static MarkTypeVO Parse(string s)
        {
            return new MarkTypeVO(int.Parse(s));
        }

        /// <summary>
        /// 将字符串形式的值转换为等价的<see cref="MarkTypeVO"/>型，返回表示转换成功与否的值。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="result"><see cref="MarkTypeVO"/>型的值</param>
        /// <returns>参数正常转换时为true。除此之外的情况是false。</returns>
        public static bool TryParse(string s, out MarkTypeVO? result)
        {
            if (int.TryParse(s, out var r))
            {
                result = new MarkTypeVO(r);
                return true;
            }
            else
            {
                result = default(MarkTypeVO);
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
        public static MarkTypeVO Min(MarkTypeVO x, MarkTypeVO y)
        {
            return new MarkTypeVO(Math.Min(x._value, y._value));
        }

        /// <summary>
        /// 返回大的值
        /// </summary>
        /// <param name="x">初值</param>
        /// <param name="y">第二个值</param>
        /// <returns>参数大的一方</returns>
        public static MarkTypeVO Max(MarkTypeVO x, MarkTypeVO y)
        {
            return new MarkTypeVO(Math.Max(x._value, y._value));
        }



        // UnitGenerateOptions.ValueArithmeticOperator

        /// <summary>
        /// 递增运算符
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static MarkTypeVO operator ++(in MarkTypeVO x)
        {
            checked
            {
                return new MarkTypeVO((int)(x._value + 1));
            }
        }

        /// <summary>
        /// 减缩运算符
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static MarkTypeVO operator --(in MarkTypeVO x)
        {
            checked
            {
                return new MarkTypeVO((int)(x._value - 1));
            }
        }

        /// <summary>
        /// 加法运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static MarkTypeVO operator +(in MarkTypeVO x, in int y)
        {
            checked
            {
                return new MarkTypeVO((int)(x._value + y));
            }
        }

        /// <summary>
        /// 减法运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static MarkTypeVO operator -(in MarkTypeVO x, in int y)
        {
            checked
            {
                return new MarkTypeVO((int)(x._value - y));
            }
        }

        /// <summary>
        /// 乘法运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static MarkTypeVO operator *(in MarkTypeVO x, in int y)
        {
            checked
            {
                return new MarkTypeVO((int)(x._value * y));
            }
        }

        /// <summary>
        /// 除法运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static MarkTypeVO operator /(in MarkTypeVO x, in int y)
        {
            checked
            {
                return new MarkTypeVO((int)(x._value / y));
            }
        }


        // UnitGenerateOptions.Comparable

        /// <summary>
        /// 将这个实例与<paramref name="other"/>进行比较。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(MarkTypeVO? other)
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
        public static bool operator >(in MarkTypeVO x, in MarkTypeVO y)
        {
            return x._value > y._value;
        }

        /// <summary>
        /// 小于运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <(in MarkTypeVO x, in MarkTypeVO y)
        {
            return x._value < y._value;
        }

        /// <summary>
        /// 大于等于运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >=(in MarkTypeVO x, in MarkTypeVO y)
        {
            return x._value >= y._value;
        }

        /// <summary>
        /// 小于等于运算符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <=(in MarkTypeVO x, in MarkTypeVO y)
        {
            return x._value <= y._value;
        }


        // UnitGenerateOptions.JsonConverter
        private class MarkTypeJsonConverter : JsonConverter<MarkTypeVO>
        {
            public override void Write(Utf8JsonWriter writer, MarkTypeVO value, JsonSerializerOptions options)
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

            public override MarkTypeVO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                            var typeConverter = TypeDescriptor.GetConverter(typeof(MarkTypeVO));
                            return (MarkTypeVO?)(stringValue == null ? null : typeConverter.ConvertFrom(stringValue));
                        }

                        var value = converter.Read(ref reader, typeToConvert, options);
                        return new MarkTypeVO(value);
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
        public class MarkTypeValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<MarkTypeVO?, int?>
        {
            /// <summary>
            /// <see cref="MarkTypeValueConverter"/>是不依赖句式而取得原始句式的方法。
            /// </summary>
            public MarkTypeValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="MarkTypeValueConverter"/>是不依赖句式而取得原始句式的方法。
            /// </summary>
            /// <param name="mappingHints"></param>
            public MarkTypeValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new MarkTypeVO(x.Value) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                int value => value,
                MarkTypeVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                MarkTypeVO value => value,
                int value => new MarkTypeVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class MarkTypeArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<MarkTypeVO?[], int?[]>
        {
            /// <summary>
            /// <see cref="MarkTypeArrayValueConverter"/>是不依赖句式而取得原始句式的方法。
            /// </summary>
            public MarkTypeArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="MarkTypeArrayValueConverter"/>是不依赖句式而取得原始句式的方法。
            /// </summary>
            /// <param name="mappingHints"></param>
            public MarkTypeArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (int?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new MarkTypeVO(_.Value)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                int?[] values => values,
                MarkTypeVO?[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<int?> values => values.ToArray(),
                IEnumerable<MarkTypeVO?> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                MarkTypeVO?[] values => values,
                int?[] values => values.Select(_ => _ == null ? null : new MarkTypeVO(_.Value)).ToArray(),
                IEnumerable<MarkTypeVO?> values => values.ToArray(),
                IEnumerable<int?> values => values.Select(_ => _ == null ? null : new MarkTypeVO(_.Value)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class MarkTypeTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(MarkTypeVO);
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
                if (t == typeof(MarkTypeVO))
                {
                    return (MarkTypeVO)value;
                }
                if (t == typeof(int))
                {
                    return new MarkTypeVO((int)value);
                }
                if (t == typeof(string))
                {
                    return new MarkTypeVO(int.Parse((string)value));
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is MarkTypeVO wrappedValue)
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
