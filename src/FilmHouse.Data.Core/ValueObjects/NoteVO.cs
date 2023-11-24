#nullable enable
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
    /// Noteを表す値オブジェクトクラスです。
    /// </summary>
    [JsonConverter(typeof(NoteJsonConverter))]
    [ValueConverter(typeof(NoteValueConverter), typeof(NoteArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(NoteTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class NoteVO : IEquatable<NoteVO>, IComparable<NoteVO>, IValue<string>, IValueObject
    {
        private readonly string _value;

        /// <summary>
        /// 型名称を取得します。
        /// </summary>
        public const string TypeName = "Note";

        /// <summary>
        /// 値オブジェクトが内包するプリミティブ型を取得します。
        /// </summary>
        public string AsPrimitive() => this._value;
        /// <summary>
        /// 型に依存せずプリミティブ型を取得するためのメソッドです。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public object AsPrimitiveObject() => this.AsPrimitive();

        /// <summary>
        /// <see cref="NoteVO"/>の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="value">値オブジェクトが内包するプリミティブ型</param>
        public NoteVO(string value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        partial void PreProcess(ref string value);

        partial void Validate();

        /// <summary>
        /// <see cref="string"/>から<see cref="NoteVO"/>への明示的なキャスト行います。
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator string(NoteVO value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="NoteVO"/>から<see cref="string"/>への明示的なキャスト行います。
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator NoteVO(string value)
        {
            return new NoteVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in NoteVO? x, in NoteVO? y)
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
        /// <see cref="string"/>型と、内包するプリミティブ型との比較処理を行います。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NoteVO? other)
        {
            return Equals(this, other);
        }

        /// <summary>
        /// オブジェクト同士が一致しているかを判断します。
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
            if (typeof(NoteVO).IsAssignableFrom(t))
            {
                return Equals((NoteVO)obj);
            }
            if (t == typeof(string))
            {
                return this._value.Equals((string)obj);
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
        /// 等値演算子
        /// </summary>
        public static bool operator ==(in NoteVO? x, in NoteVO? y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 非等値演算子
        /// </summary>
        public static bool operator !=(in NoteVO? x, in NoteVO? y)
        {
            return !Equals(x, y);
        }






        // UnitGenerateOptions.ComparableInterfaceOnly

        /// <summary>
        /// このインスタンスを<paramref name="other"/>と比較します。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(NoteVO? other)
        {
            if (other == null)
            {
                return 1;
            }
            return this._value.CompareTo(other._value);
        }


        // UnitGenerateOptions.JsonConverter
        private class NoteJsonConverter : JsonConverter<NoteVO>
        {
            public override void Write(Utf8JsonWriter writer, NoteVO value, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                if (converter != null)
                {
                    converter.Write(writer, value._value.Replace("\r\n", "\n"), options);
                }
                else
                {
                    throw new JsonException($"{typeof(string)} converter does not found.");
                }
            }

            public override NoteVO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                if (converter != null)
                {
                    try
                    {
                        var value = converter.Read(ref reader, typeToConvert, options);
                        return value != null ? new NoteVO(value.Replace("\r\n", "\n")) : null;
                    }
                    catch (Exception exception)
                    {
                        throw options.GetInvalidValueException(ref reader, typeof(string), exception);
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
        public class NoteValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<NoteVO?, string?>
        {
            /// <summary>
            /// <see cref="NoteValueConverter"/>の新しいインスタンスを生成します。
            /// </summary>
            public NoteValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="NoteValueConverter"/>の新しいインスタンスを生成します。
            /// </summary>
            /// <param name="mappingHints"></param>
            public NoteValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new NoteVO(x) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// データをストアに書き込むときにオブジェクトを変換する関数を取得し、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                string value => value,
                NoteVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// ストアからデータを読み取るときに、オブジェクトを変換する関数を取得します。この関数は、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                NoteVO value => value,
                string value => new NoteVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCoreと値オブジェクトの相互変換を行うためのコンバータクラスです。
        /// </summary>
        public class NoteArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<NoteVO?[], string?[]>
        {
            /// <summary>
            /// <see cref="NoteArrayValueConverter"/>の新しいインスタンスを生成します。
            /// </summary>
            public NoteArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="NoteArrayValueConverter"/>の新しいインスタンスを生成します。
            /// </summary>
            /// <param name="mappingHints"></param>
            public NoteArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (string?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new NoteVO(_)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// データをストアに書き込むときにオブジェクトを変換する関数を取得し、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                string?[] values => values,
                NoteVO?[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<string?> values => values.ToArray(),
                IEnumerable<NoteVO?> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// ストアからデータを読み取るときに、オブジェクトを変換する関数を取得します。この関数は、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                NoteVO?[] values => values,
                string?[] values => values.Select(_ => _ == null ? null : new NoteVO(_)).ToArray(),
                IEnumerable<NoteVO?> values => values.ToArray(),
                IEnumerable<string?> values => values.Select(_ => _ == null ? null : new NoteVO(_)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class NoteTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(NoteVO);
            private static readonly Type ValueType = typeof(string);
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
                if (t == typeof(NoteVO))
                {
                    return (NoteVO)value;
                }
                if (t == typeof(string))
                {
                    return new NoteVO((string)value);
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is NoteVO wrappedValue)
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
