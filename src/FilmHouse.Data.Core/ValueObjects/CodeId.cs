#nullable enable
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects.Serialization;
using FilmHouse.Data.Core.ValueObjects;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Core.Services.Codes;
using System.Text.RegularExpressions;

namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// 代码ID的值对象类。
    /// </summary>
    [JsonConverter(typeof(CodeIdJsonConverter))]
    [ValueConverter(typeof(CodeIdValueConverter), typeof(CodeIdArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(CodeIdTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class CodeId : IConvertible<string>, IEquatable<CodeId>, IComparable<CodeId>, IValue<string>, IValueObject
    {
        private readonly string _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public const string TypeName = "CodeId";

        /// <summary>
        /// "语言"区分的代码组。
        /// </summary>
        public static readonly CodeGroupVO Group = new("Group");

        /// <summary>
        /// 取得位数。
        /// </summary>
        public const int Size = 400;

        /// <summary>
        /// 获取值对象包含的原始类型。
        /// </summary>
        public string AsPrimitive() => this._value;
        /// <summary>
        /// 是不依赖句式而获取原始句式的方法。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public object AsPrimitiveObject() => this.AsPrimitive();

        /// <summary>
        /// <see cref="CodeId"/>的新实例。
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public CodeId(string value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        /// <summary>
        /// 转换为保持代码管理信息的<see cref="CodeElement"/>
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public IReadOnlyList<CodeElement>? AsCodeElement(ICodeProvider provider)
        {
            var codes = this._value.Split('/', StringSplitOptions.None);
            if (!codes.Any())
            {
                return null;
            }

            var group = provider.AvailableAt(Group);
            var items = group.Elements.Where(d => codes.Contains(d.Code.AsPrimitive())).ToList();
            if (!items.Any())
            {
                return null;
            }
            return items;
        }

        /// <summary>
        /// 获取代码组。
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public CodeContainer? GetCodeGroup(ICodeProvider provider)
        {
            var codes = this._value.Split('/', StringSplitOptions.None);
            if (!codes.Any())
            {
                return null;
            }

            var group = provider.AvailableAt(Group);
            return group;
        }

        partial void PreProcess(ref string value);

        partial void Validate();

        /// <summary>
        /// <see cref="string"/>向<see cref="CodeId"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator string(CodeId value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="CodeId"/>向<see cref="string"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator CodeId(string value)
        {
            return new CodeId(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in CodeId? x, in CodeId? y)
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
        /// <see cref="string"/>对句式和包含的原始句式进行比较处理。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CodeId? other)
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
            if (typeof(CodeId).IsAssignableFrom(t))
            {
                return Equals((CodeId)obj);
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
        /// 現在のオブジェクトを表す字符串を返します。
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}", this._value);
        }



        /// <summary>
        /// 等値演算子
        /// </summary>
        public static bool operator ==(in CodeId? x, in CodeId? y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in CodeId? x, in CodeId? y)
        {
            return !Equals(x, y);
        }






        // UnitGenerateOptions.ComparableInterfaceOnly

        /// <summary>
        /// このインスタンスを<paramref name="other"/>と比較します。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(CodeId? other)
        {
            if (other == null)
            {
                return 1;
            }
            return this._value.CompareTo(other._value);
        }


        // UnitGenerateOptions.JsonConverter
        private class CodeIdJsonConverter : JsonConverter<CodeId>
        {
            public override void Write(Utf8JsonWriter writer, CodeId value, JsonSerializerOptions options)
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

            public override CodeId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                if (converter != null)
                {
                    try
                    {
                        var value = converter.Read(ref reader, typeToConvert, options);
                        return value != null ? new CodeId(value.Replace("\r\n", "\n")) : null;
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
        public class CodeIdValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<CodeId?, string?>
        {
            /// <summary>
            /// <see cref="CodeIdValueConverter"/>的新实例。
            /// </summary>
            public CodeIdValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="CodeIdValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public CodeIdValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new CodeId(x) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// データをストアに書き込むときにオブジェクトを変換する関数を取得し、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                string value => value,
                CodeId value => value._value,
                _ => null,
            };

            /// <summary>
            /// ストア向データを読み取るときに、オブジェクトを変換する関数を取得します。この関数は、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                CodeId value => value,
                string value => new CodeId(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCoreと値オブジェクトの相互変換を行うためのコンバータクラスです。
        /// </summary>
        public class CodeIdArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<CodeId?[], string?[]>
        {
            /// <summary>
            /// <see cref="CodeIdArrayValueConverter"/>的新实例。
            /// </summary>
            public CodeIdArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="CodeIdArrayValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public CodeIdArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (string?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new CodeId(_)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// データをストアに書き込むときにオブジェクトを変換する関数を取得し、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                string?[] values => values,
                CodeId?[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<string?> values => values.ToArray(),
                IEnumerable<CodeId?> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// ストア向データを読み取るときに、オブジェクトを変換する関数を取得します。この関数は、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                CodeId?[] values => values,
                string?[] values => values.Select(_ => _ == null ? null : new CodeId(_)).ToArray(),
                IEnumerable<CodeId?> values => values.ToArray(),
                IEnumerable<string?> values => values.Select(_ => _ == null ? null : new CodeId(_)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class CodeIdTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(CodeId);
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
                if (t == typeof(CodeId))
                {
                    return (CodeId)value;
                }
                if (t == typeof(string))
                {
                    return new CodeId((string)value);
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is CodeId wrappedValue)
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
