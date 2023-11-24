using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using FilmHouse.Data.Core.Utils;
using FilmHouse.Data.Core.ValueObjects.Serialization;

namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// 日数を表す値オブジェクトクラスです。プリミティブ型との暗黙的なキャストを行います。
    /// </summary>
    [JsonConverter(typeof(RequestWithConverter))]
    [ValueConverter(typeof(RequestWithValueConverter), typeof(RequestWithArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(RequestWithTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class RequestWithVO : IEquatable<RequestWithVO>, IComparable<RequestWithVO>, IFormattable, IConvertible, IValue<int>, IValueObject
    {
        private readonly int _value;

        /// <summary>
        /// 型名称を取得します。
        /// </summary>
        public const string TypeName = "RequestWith";

        /// <summary>
        /// 数値としての最大桁数を取得します。
        /// </summary>
        public const int Precision = 3;

        /// <summary>
        /// 値オブジェクトが内包するプリミティブ型を取得します。
        /// </summary>
        public int AsPrimitive() => this._value;
        /// <summary>
        /// 型に依存せずプリミティブ型を取得するためのメソッドです。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public object AsPrimitiveObject() => this.AsPrimitive();

        /// <summary>
        /// <see cref="RequestWithVO"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="value">値オブジェクトが内包するプリミティブ型</param>
        public RequestWithVO(int value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        partial void PreProcess(ref int value);

        partial void Validate();

        /// <summary>
        /// <see cref="int"/>から<see cref="RequestWithVO"/>への暗黙的なキャスト行います。
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator int(RequestWithVO value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="RequestWithVO"/>から<see cref="int"/>への暗黙的なキャスト行います。
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator RequestWithVO(int value)
        {
            return new RequestWithVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in RequestWithVO x, in RequestWithVO y)
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
        /// <see cref="int"/>型と、内包するプリミティブ型との比較処理を行います。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RequestWithVO other)
        {
            return Equals(this, other);
        }

        /// <summary>
        /// オブジェクト同士が一致しているかを判断します。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var t = obj.GetType();
            if (typeof(RequestWithVO).IsAssignableFrom(t))
            {
                return Equals((RequestWithVO)obj);
            }
            if (t == typeof(int))
            {
                return this._value.Equals((int)obj);
            }

            return this._value.Equals(obj);
        }

        /// <summary>
        /// 既定のハッシュ関数として機能します。
        /// </summary>
        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        /// <summary>
        /// 現在のオブジェクトを表す文字列を返します。
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}", this._value);
        }

        /// <summary>
        /// 現在のオブジェクトを表す文字列を返します。
        /// </summary>
        /// <param name="format">書式文字列</param>
        /// <returns>現在のオブジェクトを表す文字列</returns>
        public virtual string ToString(string format) => this.AsPrimitive().ToString(format);

        /// <summary>
        /// 現在のオブジェクトを表す文字列を返します。
        /// </summary>
        /// <param name="format">書式文字列</param>
        /// <param name="provider">値の書式設定に使用するプロバイダー</param>
        /// <returns>現在のオブジェクトを表す文字列</returns>
        public virtual string ToString(string format, IFormatProvider provider) => this.AsPrimitive().ToString(format, provider);

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
        bool IConvertible.ToBoolean(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToBoolean(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        byte IConvertible.ToByte(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToByte(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        char IConvertible.ToChar(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToChar(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        DateTime IConvertible.ToDateTime(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToDateTime(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        decimal IConvertible.ToDecimal(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToDecimal(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        double IConvertible.ToDouble(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToDouble(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        short IConvertible.ToInt16(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToInt16(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        int IConvertible.ToInt32(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToInt32(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        long IConvertible.ToInt64(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToInt64(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        sbyte IConvertible.ToSByte(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToSByte(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        float IConvertible.ToSingle(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToSingle(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        string IConvertible.ToString(IFormatProvider provider) => this.AsPrimitive().ToString(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversionType"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        object IConvertible.ToType(Type conversionType, IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToType(conversionType, provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        ushort IConvertible.ToUInt16(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToUInt16(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        uint IConvertible.ToUInt32(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToUInt32(provider);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        ulong IConvertible.ToUInt64(IFormatProvider provider) => ((IConvertible)this.AsPrimitive()).ToUInt64(provider);

        /// <summary>
        /// 等値演算子
        /// </summary>
        public static bool operator ==(in RequestWithVO x, in RequestWithVO y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 非等値演算子
        /// </summary>
        public static bool operator !=(in RequestWithVO x, in RequestWithVO y)
        {
            return !Equals(x, y);
        }

        // UnitGenerateOptions.ParseMethod

        /// <summary>
        /// 文字列形式の値を、等価の<see cref="RequestWithVO"/>型に変換します。
        /// </summary>
        /// <param name="s">文字列</param>
        /// <returns><see cref="RequestWithVO"/>型の値</returns>
        public static RequestWithVO Parse(string s)
        {
            return new RequestWithVO(int.Parse(s));
        }

        /// <summary>
        /// 文字列形式の値を、等価の<see cref="RequestWithVO"/>型に変換し、変換に成功したかどうかを示す値を返します。
        /// </summary>
        /// <param name="s">文字列</param>
        /// <param name="result"><see cref="RequestWithVO"/>型の値</param>
        /// <returns>パラメーターが正常に変換された場合は true。それ以外の場合は false。</returns>
        public static bool TryParse(string s, out RequestWithVO result)
        {
            if (int.TryParse(s, out var r))
            {
                result = new RequestWithVO(r);
                return true;
            }
            else
            {
                result = default(RequestWithVO);
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
        public static RequestWithVO Min(RequestWithVO x, RequestWithVO y)
        {
            return new RequestWithVO(Math.Min(x._value, y._value));
        }

        /// <summary>
        /// 大きい方を返します。
        /// </summary>
        /// <param name="x">最初の値</param>
        /// <param name="y">2番目の値</param>
        /// <returns>パラメーターのいずれか大きい方</returns>
        public static RequestWithVO Max(RequestWithVO x, RequestWithVO y)
        {
            return new RequestWithVO(Math.Max(x._value, y._value));
        }



        // UnitGenerateOptions.ValueArithmeticOperator

        /// <summary>
        /// インクリメント演算子
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static RequestWithVO operator ++(in RequestWithVO x)
        {
            checked
            {
                return new RequestWithVO((int)(x._value + 1));
            }
        }

        /// <summary>
        /// デクリメント演算子
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static RequestWithVO operator --(in RequestWithVO x)
        {
            checked
            {
                return new RequestWithVO((int)(x._value - 1));
            }
        }

        /// <summary>
        /// 加算演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static RequestWithVO operator +(in RequestWithVO x, in int y)
        {
            checked
            {
                return new RequestWithVO((int)(x._value + y));
            }
        }

        /// <summary>
        /// 減算演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static RequestWithVO operator -(in RequestWithVO x, in int y)
        {
            checked
            {
                return new RequestWithVO((int)(x._value - y));
            }
        }

        /// <summary>
        /// 乗算演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static RequestWithVO operator *(in RequestWithVO x, in int y)
        {
            checked
            {
                return new RequestWithVO((int)(x._value * y));
            }
        }

        /// <summary>
        /// 除算演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static RequestWithVO operator /(in RequestWithVO x, in int y)
        {
            checked
            {
                return new RequestWithVO((int)(x._value / y));
            }
        }


        // UnitGenerateOptions.Comparable

        /// <summary>
        /// このインスタンスを<paramref name="other"/>と比較します。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(RequestWithVO other)
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
        public static bool operator >(in RequestWithVO x, in RequestWithVO y)
        {
            return x._value > y._value;
        }

        /// <summary>
        /// 小なり演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <(in RequestWithVO x, in RequestWithVO y)
        {
            return x._value < y._value;
        }

        /// <summary>
        /// 以上演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >=(in RequestWithVO x, in RequestWithVO y)
        {
            return x._value >= y._value;
        }

        /// <summary>
        /// 以下演算子
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <=(in RequestWithVO x, in RequestWithVO y)
        {
            return x._value <= y._value;
        }


        // UnitGenerateOptions.JsonConverter
        private class RequestWithConverter : JsonConverter<RequestWithVO>
        {
            public override void Write(Utf8JsonWriter writer, RequestWithVO value, JsonSerializerOptions options)
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

            public override RequestWithVO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                            var typeConverter = TypeDescriptor.GetConverter(typeof(RequestWithVO));
                            return (RequestWithVO)(stringValue == null ? null : typeConverter.ConvertFrom(stringValue));
                        }

                        var value = converter.Read(ref reader, typeToConvert, options);
                        return new RequestWithVO(value);
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
        public class RequestWithValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<RequestWithVO, int?>
        {
            /// <summary>
            /// <see cref="RequestWithValueConverter"/>の新しいインスタンスを生成します。
            /// </summary>
            public RequestWithValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="RequestWithValueConverter"/>の新しいインスタンスを生成します。
            /// </summary>
            /// <param name="mappingHints"></param>
            public RequestWithValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new RequestWithVO(x.Value) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// データをストアに書き込むときにオブジェクトを変換する関数を取得し、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object, object> ConvertToProvider => (x) => x switch
            {
                int value => value,
                RequestWithVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// ストアからデータを読み取るときに、オブジェクトを変換する関数を取得します。この関数は、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object, object> ConvertFromProvider => (x) => x switch
            {
                RequestWithVO value => value,
                int value => new RequestWithVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCoreと値オブジェクトの相互変換を行うためのコンバータクラスです。
        /// </summary>
        public class RequestWithArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<RequestWithVO[], int?[]>
        {
            /// <summary>
            /// <see cref="RequestWithArrayValueConverter"/>の新しいインスタンスを生成します。
            /// </summary>
            public RequestWithArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="RequestWithArrayValueConverter"/>の新しいインスタンスを生成します。
            /// </summary>
            /// <param name="mappingHints"></param>
            public RequestWithArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (int?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new RequestWithVO(_.Value)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// データをストアに書き込むときにオブジェクトを変換する関数を取得し、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object, object> ConvertToProvider => (x) => x switch
            {
                int?[] values => values,
                RequestWithVO[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<int?> values => values.ToArray(),
                IEnumerable<RequestWithVO> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// ストアからデータを読み取るときに、オブジェクトを変換する関数を取得します。この関数は、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object, object> ConvertFromProvider => (x) => x switch
            {
                RequestWithVO[] values => values,
                int?[] values => values.Select(_ => _ == null ? null : new RequestWithVO(_.Value)).ToArray(),
                IEnumerable<RequestWithVO> values => values.ToArray(),
                IEnumerable<int?> values => values.Select(_ => _ == null ? null : new RequestWithVO(_.Value)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class RequestWithTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(RequestWithVO);
            private static readonly Type ValueType = typeof(int);
            private static readonly Type BindingValueType = typeof(string);

            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == WrapperType || sourceType == ValueType || sourceType == BindingValueType)
                {
                    return true;
                }

                return base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type destinationType)
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

            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                var t = value.GetType();
                if (t == typeof(RequestWithVO))
                {
                    return (RequestWithVO)value;
                }
                if (t == typeof(int))
                {
                    return new RequestWithVO((int)value);
                }
                if (t == typeof(string))
                {
                    return new RequestWithVO(int.Parse((string)value));
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is RequestWithVO wrappedValue)
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
