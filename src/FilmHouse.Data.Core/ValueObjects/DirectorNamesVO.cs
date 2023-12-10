﻿#nullable enable
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

namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// 导演中文姓名集合（500位文本）的值对象类。
    /// </summary>
    [JsonConverter(typeof(DirectorNamesJsonConverter))]
    [ValueConverter(typeof(DirectorNamesValueConverter), typeof(DirectorNamesArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(DirectorNamesTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class DirectorNamesVO : FilmHouse.Core.ValueObjects.TextBase, IEquatable<DirectorNamesVO>, IComparable<DirectorNamesVO>, IValue<string>, IValueObject, IEnumeratorObject<CelebrityNameVO>
    {
        private readonly string _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public new const string TypeName = "DirectorNames(size:500)";

        /// <summary>
        /// 取得位数。
        /// </summary>
        public const int Size = 500;

        /// <summary>
        /// <see cref="DirectorNamesVO"/>的新实例。
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public DirectorNamesVO(string value)
            : base(value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        /// <summary>
        /// <see cref="CelebrityNameVO"/>的集合。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CelebrityNameVO>? ToEnumerable()
        {
            foreach (var value in this._value.Split('/', StringSplitOptions.RemoveEmptyEntries))
            {
                yield return (new CelebrityNameVO(value));
            }
        }

        partial void PreProcess(ref string value);

        partial void Validate();

        /// <summary>
        /// <see cref="string"/>向<see cref="DirectorNamesVO"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator string(DirectorNamesVO value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="DirectorNamesVO"/>向<see cref="string"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator DirectorNamesVO(string value)
        {
            return new DirectorNamesVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in DirectorNamesVO? x, in DirectorNamesVO? y)
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
        public bool Equals(DirectorNamesVO? other)
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
            if (typeof(DirectorNamesVO).IsAssignableFrom(t))
            {
                return Equals((DirectorNamesVO)obj);
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
        /// 返回表示当前对象的字符串。
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}", this._value);
        }



        /// <summary>
        /// 是否等于
        /// </summary>
        public static bool operator ==(in DirectorNamesVO? x, in DirectorNamesVO? y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in DirectorNamesVO? x, in DirectorNamesVO? y)
        {
            return !Equals(x, y);
        }






        // UnitGenerateOptions.ComparableInterfaceOnly

        /// <summary>
        /// 将该实例<paramref name="other" />和比较。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DirectorNamesVO? other)
        {
            if (other == null)
            {
                return 1;
            }
            return this._value.CompareTo(other._value);
        }


        // UnitGenerateOptions.JsonConverter
        private class DirectorNamesJsonConverter : JsonConverter<DirectorNamesVO>
        {
            public override void Write(Utf8JsonWriter writer, DirectorNamesVO value, JsonSerializerOptions options)
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

            public override DirectorNamesVO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                if (converter != null)
                {
                    try
                    {
                        var value = converter.Read(ref reader, typeToConvert, options);
                        return value != null ? new DirectorNamesVO(value.Replace("\r\n", "\n")) : null;
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
        public class DirectorNamesValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DirectorNamesVO?, string?>
        {
            /// <summary>
            /// <see cref="DirectorNamesValueConverter"/>的新实例。
            /// </summary>
            public DirectorNamesValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="DirectorNamesValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public DirectorNamesValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new DirectorNamesVO(x) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                string value => value,
                DirectorNamesVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                DirectorNamesVO value => value,
                string value => new DirectorNamesVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class DirectorNamesArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DirectorNamesVO?[], string?[]>
        {
            /// <summary>
            /// <see cref="DirectorNamesArrayValueConverter"/>的新实例。
            /// </summary>
            public DirectorNamesArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="DirectorNamesArrayValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public DirectorNamesArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (string?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new DirectorNamesVO(_)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                string?[] values => values,
                DirectorNamesVO?[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<string?> values => values.ToArray(),
                IEnumerable<DirectorNamesVO?> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                DirectorNamesVO?[] values => values,
                string?[] values => values.Select(_ => _ == null ? null : new DirectorNamesVO(_)).ToArray(),
                IEnumerable<DirectorNamesVO?> values => values.ToArray(),
                IEnumerable<string?> values => values.Select(_ => _ == null ? null : new DirectorNamesVO(_)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class DirectorNamesTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(DirectorNamesVO);
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
                if (t == typeof(DirectorNamesVO))
                {
                    return (DirectorNamesVO)value;
                }
                if (t == typeof(string))
                {
                    return new DirectorNamesVO((string)value);
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is DirectorNamesVO wrappedValue)
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
