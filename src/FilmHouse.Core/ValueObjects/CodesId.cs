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
using FilmHouse.Core.Services.Codes;
using System.Text.RegularExpressions;

namespace FilmHouse.Core.ValueObjects
{
    /// <summary>
    /// 代码ID的值对象类。
    /// </summary>
    [JsonConverter(typeof(CodeIdJsonConverter))]
    [ValueConverter(typeof(CodeIdValueConverter), typeof(CodeIdArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(CodeIdTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class CodesId : IConvertible<string>, IEquatable<CodesId>, IComparable<CodesId>, IValue<string>, IValueObject
    {
        private readonly string _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public const string TypeName = "CodeId";

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
        /// <see cref="CodesId"/>的新实例。
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        /// <param name="group"></param>
        public CodesId(string value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        /// <summary>
        /// 转换为保持代码管理信息的<see cref="CodeElement"/>
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="codeGroup"></param>
        /// <returns></returns>
        public IReadOnlyList<CodeElement>? AsCodeElement(ICodeProvider provider, CodeGroupVO codeGroup)
        {
            var codes = this._value.Split('/', StringSplitOptions.None);
            if (!codes.Any())
            {
                return null;
            }

            var group = provider.AvailableAt(codeGroup);
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
        /// <param name="codeGroup"></param>
        /// <returns></returns>
        public CodeContainer? GetCodeGroup(ICodeProvider provider, CodeGroupVO codeGroup)
        {
            var codes = this._value.Split('/', StringSplitOptions.None);
            if (!codes.Any())
            {
                return null;
            }

            var group = provider.AvailableAt(codeGroup);
            return group;
        }

        partial void PreProcess(ref string value);

        partial void Validate();

        /// <summary>
        /// <see cref="string"/>向<see cref="CodesId"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator string(CodesId value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="CodesId"/>向<see cref="string"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator CodesId(string value)
        {
            return new CodesId(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in CodesId? x, in CodesId? y)
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
        public bool Equals(CodesId? other)
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
            if (typeof(CodesId).IsAssignableFrom(t))
            {
                return Equals((CodesId)obj);
            }
            if (t == typeof(string))
            {
                return this._value.Equals((string)obj);
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
        /// 返回表示当前对象的字串。
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}", this._value);
        }



        /// <summary>
        /// 是否相等
        /// </summary>
        public static bool operator ==(in CodesId? x, in CodesId? y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in CodesId? x, in CodesId? y)
        {
            return !Equals(x, y);
        }






        // UnitGenerateOptions.ComparableInterfaceOnly

        /// <summary>
        /// 将这个实例与<paramref name="other"/>进行比较。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(CodesId? other)
        {
            if (other == null)
            {
                return 1;
            }
            return this._value.CompareTo(other._value);
        }


        // UnitGenerateOptions.JsonConverter
        private class CodeIdJsonConverter : JsonConverter<CodesId>
        {
            public override void Write(Utf8JsonWriter writer, CodesId value, JsonSerializerOptions options)
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

            public override CodesId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                if (converter != null)
                {
                    try
                    {
                        var value = converter.Read(ref reader, typeToConvert, options);
                        return value != null ? new CodesId(value.Replace("\r\n", "\n")) : null;
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
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class CodeIdValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<CodesId?, string?>
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
                        convertFromProviderExpression: x => x != null ? new CodesId(x) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                string value => value,
                CodesId value => value._value,
                _ => null,
            };

            /// <summary>
            /// ストア向データを読み取るときに、オブジェクトを変換する関数を取得します。この関数は、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                CodesId value => value,
                string value => new CodesId(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class CodeIdArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<CodesId?[], string?[]>
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
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new CodesId(_)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                string?[] values => values,
                CodesId?[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<string?> values => values.ToArray(),
                IEnumerable<CodesId?> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// ストア向データを読み取るときに、オブジェクトを変換する関数を取得します。この関数は、null、ボックス化、および非厳密一致の単純型の一致を処理するように設定します。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                CodesId?[] values => values,
                string?[] values => values.Select(_ => _ == null ? null : new CodesId(_)).ToArray(),
                IEnumerable<CodesId?> values => values.ToArray(),
                IEnumerable<string?> values => values.Select(_ => _ == null ? null : new CodesId(_)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class CodeIdTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(CodesId);
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
                if (t == typeof(CodesId))
                {
                    return (CodesId)value;
                }
                if (t == typeof(string))
                {
                    return new CodesId((string)value);
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is CodesId wrappedValue)
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
