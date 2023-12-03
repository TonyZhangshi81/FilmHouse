#nullable enable
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.ValueObjects.Serialization;

namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// 代表输入密码的值对象类。
    /// </summary>
    [JsonConverter(typeof(PasswordVOJsonConverter))]
    [ValueConverter(typeof(PasswordVOValueConverter), typeof(PasswordVOArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(PasswordVOTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class PasswordVO : FilmHouse.Core.ValueObjects.TextBase, FilmHouse.Core.Utils.IPasswordHashable<PasswordVO>, IEquatable<PasswordVO>, IComparable<PasswordVO>, IValue<string>, IValueObject
    {
        private readonly string _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public new const string TypeName = "Password";

        /// <summary>
        /// 取得位数。
        /// </summary>
        public const int Size = 200;

        /// <summary>
        /// <see cref="PasswordVO"/>的新实例。
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public PasswordVO(string value)
            : base(value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        partial void PreProcess(ref string value);

        partial void Validate();

        /// <summary>
        /// <see cref="string"/>向<see cref="PasswordVO"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator string(PasswordVO value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="PasswordVO"/>向<see cref="string"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator PasswordVO(string value)
        {
            return new PasswordVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in PasswordVO? x, in PasswordVO? y)
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
        /// 对<see cref="string"/>型和包含的原始型进行比较处理。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PasswordVO? other)
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
            if (typeof(PasswordVO).IsAssignableFrom(t))
            {
                return Equals((PasswordVO)obj);
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
        /// 是否相等
        /// </summary>
        public static bool operator ==(in PasswordVO? x, in PasswordVO? y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in PasswordVO? x, in PasswordVO? y)
        {
            return !Equals(x, y);
        }

        // UnitGenerateOptions.ComparableInterfaceOnly

        /// <summary>
        /// 将这个实例与<paramref name="other"/>进行比较。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(PasswordVO? other)
        {
            if (other == null)
            {
                return 1;
            }
            return this._value.CompareTo(other._value);
        }


        // UnitGenerateOptions.JsonConverter
        private class PasswordVOJsonConverter : JsonConverter<PasswordVO>
        {
            public override void Write(Utf8JsonWriter writer, PasswordVO value, JsonSerializerOptions options)
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

            public override PasswordVO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                if (converter != null)
                {
                    try
                    {
                        var value = converter.Read(ref reader, typeToConvert, options);
                        return value != null ? new PasswordVO(value.Replace("\r\n", "\n")) : null;
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
        public class PasswordVOValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<PasswordVO?, string?>
        {
            /// <summary>
            /// <see cref="PasswordVOValueConverter"/>的新实例。
            /// </summary>
            public PasswordVOValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="PasswordVOValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public PasswordVOValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new PasswordVO(x) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                string value => value,
                PasswordVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// 当读取存储数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                PasswordVO value => value,
                string value => new PasswordVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class PasswordVOArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<PasswordVO?[], string?[]>
        {
            /// <summary>
            /// <see cref="PasswordVOArrayValueConverter"/>的新实例。
            /// </summary>
            public PasswordVOArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="PasswordVOArrayValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public PasswordVOArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (string?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new PasswordVO(_)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                string?[] values => values,
                PasswordVO?[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<string?> values => values.ToArray(),
                IEnumerable<PasswordVO?> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// 当读取存储数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                PasswordVO?[] values => values,
                string?[] values => values.Select(_ => _ == null ? null : new PasswordVO(_)).ToArray(),
                IEnumerable<PasswordVO?> values => values.ToArray(),
                IEnumerable<string?> values => values.Select(_ => _ == null ? null : new PasswordVO(_)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class PasswordVOTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(PasswordVO);
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
                if (t == typeof(PasswordVO))
                {
                    return (PasswordVO)value;
                }
                if (t == typeof(string))
                {
                    return new PasswordVO((string)value);
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is PasswordVO wrappedValue)
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
