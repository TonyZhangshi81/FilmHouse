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
    [JsonConverter(typeof(ReviewStatusJsonConverter))]
    [ValueConverter(typeof(ReviewStatusValueConverter), typeof(ReviewStatusArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(ReviewStatusTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class ReviewStatusVO : IEquatable<ReviewStatusVO>, IComparable<ReviewStatusVO>, IFormattable, IConvertible, IValue<int>, IValueObject
    {
        private readonly int _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public const string TypeName = "ReviewStatus";

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
            public static readonly ReviewStatusVO ReviewStatusCode0 = new(0);
            /// <summary>
            /// 「1:评审不通过」
            /// </summary>
            public static readonly ReviewStatusVO ReviewStatusCode1 = new(1);
            /// <summary>
            /// 「2:通过」
            /// </summary>
            public static readonly ReviewStatusVO ReviewStatusCode2 = new(2);
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
        /// <see cref="ReviewStatusVO"/>是不依赖句式而取得原始句式的方法。
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public ReviewStatusVO(int value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        partial void PreProcess(ref int value);

        partial void Validate();

        /// <summary>
        /// <see cref="int"/>向<see cref="ReviewStatusVO"/>对的隐性的角色扮演。
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator int(ReviewStatusVO value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="ReviewStatusVO"/>向<see cref="int"/>对的隐性的角色扮演。
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ReviewStatusVO(int value)
        {
            return new ReviewStatusVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in ReviewStatusVO? x, in ReviewStatusVO? y)
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
        public bool Equals(ReviewStatusVO? other)
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
            if (typeof(ReviewStatusVO).IsAssignableFrom(t))
            {
                return Equals((ReviewStatusVO)obj);
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
        public static bool operator ==(in ReviewStatusVO? x, in ReviewStatusVO? y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in ReviewStatusVO? x, in ReviewStatusVO? y)
        {
            return !Equals(x, y);
        }

        // UnitGenerateOptions.ParseMethod

        /// <summary>
        /// 将字符串形式的值转换为等价的<see cref="ReviewStatusVO"/>型。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns><see cref="ReviewStatusVO"/>型的值</returns>
        public static ReviewStatusVO Parse(string s)
        {
            return new ReviewStatusVO(int.Parse(s));
        }

        /// <summary>
        /// 将字符串形式的值转换为等价的<see cref="ReviewStatusVO"/>型，返回表示转换成功与否的值。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="result"><see cref="ReviewStatusVO"/>型的值</param>
        /// <returns>参数正常转换时为true。除此之外的情况是false。</returns>
        public static bool TryParse(string s, out ReviewStatusVO? result)
        {
            if (int.TryParse(s, out var r))
            {
                result = new ReviewStatusVO(r);
                return true;
            }
            else
            {
                result = default(ReviewStatusVO);
                return false;
            }
        }


        // UnitGenerateOptions.MinMaxMethod

        /// <summary>
        /// 小さい方を返します。
        /// </summary>
        /// <param name="x">最初の値</param>
        /// <param name="y">2番目の値</param>
        /// <returns>パラメーターのいずれか小さい方</returns>
        public static ReviewStatusVO Min(ReviewStatusVO x, ReviewStatusVO y)
        {
            return new ReviewStatusVO(Math.Min(x._value, y._value));
        }

        /// <summary>
        /// 大きい方を返します。
        /// </summary>
        /// <param name="x">最初の値</param>
        /// <param name="y">2番目の値</param>
        /// <returns>パラメーターのいずれか大きい方</returns>
        public static ReviewStatusVO Max(ReviewStatusVO x, ReviewStatusVO y)
        {
            return new ReviewStatusVO(Math.Max(x._value, y._value));
        }



        // UnitGenerateOptions.ValueArithmeticOperator

        /// <summary>
        /// インクリメント演算子
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static ReviewStatusVO operator ++(in ReviewStatusVO x)
        {
            checked
            {
                return new ReviewStatusVO((int)(x._value + 1));
            }
        }

        /// <summary>
        /// デクリメント演算子
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static ReviewStatusVO operator --(in ReviewStatusVO x)
        {
            checked
            {
                return new ReviewStatusVO((int)(x._value - 1));
            }
        }

        /// <summary>
        /// 加算演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static ReviewStatusVO operator +(in ReviewStatusVO x, in int y)
        {
            checked
            {
                return new ReviewStatusVO((int)(x._value + y));
            }
        }

        /// <summary>
        /// 減算演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static ReviewStatusVO operator -(in ReviewStatusVO x, in int y)
        {
            checked
            {
                return new ReviewStatusVO((int)(x._value - y));
            }
        }

        /// <summary>
        /// 乗算演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static ReviewStatusVO operator *(in ReviewStatusVO x, in int y)
        {
            checked
            {
                return new ReviewStatusVO((int)(x._value * y));
            }
        }

        /// <summary>
        /// 除算演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static ReviewStatusVO operator /(in ReviewStatusVO x, in int y)
        {
            checked
            {
                return new ReviewStatusVO((int)(x._value / y));
            }
        }


        // UnitGenerateOptions.Comparable

        /// <summary>
        /// このインスタンスを<paramref name="other"/>と比較します。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(ReviewStatusVO? other)
        {
            if (other == null)
            {
                return 1;
            }
            return this._value.CompareTo(other._value);
        }

        /// <summary>
        /// 大なり演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >(in ReviewStatusVO x, in ReviewStatusVO y)
        {
            return x._value > y._value;
        }

        /// <summary>
        /// 小なり演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <(in ReviewStatusVO x, in ReviewStatusVO y)
        {
            return x._value < y._value;
        }

        /// <summary>
        /// 以上演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >=(in ReviewStatusVO x, in ReviewStatusVO y)
        {
            return x._value >= y._value;
        }

        /// <summary>
        /// 以下演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <=(in ReviewStatusVO x, in ReviewStatusVO y)
        {
            return x._value <= y._value;
        }


        // UnitGenerateOptions.JsonConverter
        private class ReviewStatusJsonConverter : JsonConverter<ReviewStatusVO>
        {
            public override void Write(Utf8JsonWriter writer, ReviewStatusVO value, JsonSerializerOptions options)
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

            public override ReviewStatusVO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                            var typeConverter = TypeDescriptor.GetConverter(typeof(ReviewStatusVO));
                            return (ReviewStatusVO?)(stringValue == null ? null : typeConverter.ConvertFrom(stringValue));
                        }

                        var value = converter.Read(ref reader, typeToConvert, options);
                        return new ReviewStatusVO(value);
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
        /// EntityFrameworkCoreと値オブジェクトの相互変換を行うためのコンバータクラスです。
        /// </summary>
        public class ReviewStatusValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<ReviewStatusVO?, int?>
        {
            /// <summary>
            /// <see cref="ReviewStatusValueConverter"/>是不依赖句式而取得原始句式的方法。
            /// </summary>
            public ReviewStatusValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="ReviewStatusValueConverter"/>是不依赖句式而取得原始句式的方法。
            /// </summary>
            /// <param name="mappingHints"></param>
            public ReviewStatusValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new ReviewStatusVO(x.Value) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// データをストアに書き込むときにオブジェクトを変換する関数を取得し、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                int value => value,
                ReviewStatusVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// ストアからデータを読み取るときに、オブジェクトを変換する関数を取得します。この関数は、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                ReviewStatusVO value => value,
                int value => new ReviewStatusVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCoreと値オブジェクトの相互変換を行うためのコンバータクラスです。
        /// </summary>
        public class ReviewStatusArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<ReviewStatusVO?[], int?[]>
        {
            /// <summary>
            /// <see cref="ReviewStatusArrayValueConverter"/>是不依赖句式而取得原始句式的方法。
            /// </summary>
            public ReviewStatusArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="ReviewStatusArrayValueConverter"/>是不依赖句式而取得原始句式的方法。
            /// </summary>
            /// <param name="mappingHints"></param>
            public ReviewStatusArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (int?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new ReviewStatusVO(_.Value)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// データをストアに書き込むときにオブジェクトを変換する関数を取得し、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                int?[] values => values,
                ReviewStatusVO?[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<int?> values => values.ToArray(),
                IEnumerable<ReviewStatusVO?> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// ストアからデータを読み取るときに、オブジェクトを変換する関数を取得します。この関数は、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                ReviewStatusVO?[] values => values,
                int?[] values => values.Select(_ => _ == null ? null : new ReviewStatusVO(_.Value)).ToArray(),
                IEnumerable<ReviewStatusVO?> values => values.ToArray(),
                IEnumerable<int?> values => values.Select(_ => _ == null ? null : new ReviewStatusVO(_.Value)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class ReviewStatusTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(ReviewStatusVO);
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
                if (t == typeof(ReviewStatusVO))
                {
                    return (ReviewStatusVO)value;
                }
                if (t == typeof(int))
                {
                    return new ReviewStatusVO((int)value);
                }
                if (t == typeof(string))
                {
                    return new ReviewStatusVO(int.Parse((string)value));
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is ReviewStatusVO wrappedValue)
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
