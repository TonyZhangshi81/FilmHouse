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

namespace FilmHouse.Core.ValueObjects
{
    /// <summary>
    /// 是否有效的值对象类。
    /// </summary>
    [JsonConverter(typeof(IsEnabledVOJsonConverter))]
    [ValueConverter(typeof(IsEnabledVOValueConverter), typeof(IsEnabledVOArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(IsEnabledVOTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class IsEnabledVO : IEquatable<IsEnabledVO>, IComparable<IsEnabledVO>, IConvertible, IValue<bool>, IValueObject
    {
        private readonly bool _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public const string TypeName = "IsEnabled";


        /// <summary>
        /// 获取值对象包含的原始类型。
        /// </summary>
        public bool AsPrimitive() => this._value;
        /// <summary>
        /// 是不依赖句式而获取原始句式的方法。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public object AsPrimitiveObject() => this.AsPrimitive();

        /// <summary>
        /// <see cref="IsEnabledVO"/>的新实例。
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public IsEnabledVO(bool value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        partial void PreProcess(ref bool value);

        partial void Validate();

        /// <summary>
        /// <see cref="bool"/>向<see cref="IsEnabledVO"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator bool(IsEnabledVO value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="IsEnabledVO"/>向<see cref="bool"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator IsEnabledVO(bool value)
        {
            return new IsEnabledVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in IsEnabledVO? x, in IsEnabledVO? y)
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
        /// <see cref="bool"/>对句式和包含的原始句式进行比较处理。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IsEnabledVO? other)
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
            if (typeof(IsEnabledVO).IsAssignableFrom(t))
            {
                return Equals((IsEnabledVO)obj);
            }
            if (t == typeof(bool))
            {
                return this._value.Equals((bool)obj);
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
        /// 是否等于
        /// </summary>
        public static bool operator ==(in IsEnabledVO? x, in IsEnabledVO? y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不等于
        /// </summary>
        public static bool operator !=(in IsEnabledVO? x, in IsEnabledVO? y)
        {
            return !Equals(x, y);
        }

        // UnitGenerateOptions.ParseMethod

        /// <summary>
        /// 将字符串形式的值转换为等价<see cref="IsEnabledVO" />转换成句式。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns><see cref="IsEnabledVO"/>型的值</returns>
        public static IsEnabledVO Parse(string s)
        {
            return new IsEnabledVO(bool.Parse(s));
        }

        /// <summary>
        /// 将字符串形式的值转换为等价<see cref="IsEnabledVO" />转换成句式，返回表示转换成功与否的值。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="result"><see cref="IsEnabledVO"/>型的值</param>
        /// <returns>参数正常转换时为true。除此之外的情况是false。</returns>
        public static bool TryParse(string s, out IsEnabledVO? result)
        {
            if (bool.TryParse(s, out var r))
            {
                result = new IsEnabledVO(r);
                return true;
            }
            else
            {
                result = default(IsEnabledVO);
                return false;
            }
        }



        // Default

        /// <summary>
        /// true 运算符
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool operator true(IsEnabledVO x)
        {
            return x._value;
        }

        /// <summary>
        /// false 运算符
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool operator false(IsEnabledVO x)
        {
            return !x._value;
        }

        /// <summary>
        /// not 运算符
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool operator !(IsEnabledVO x)
        {
            return !x._value;
        }




        // UnitGenerateOptions.ComparableInterfaceOnly

        /// <summary>
        /// 将该实例<paramref name="other" />和比较。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(IsEnabledVO? other)
        {
            if (other == null)
            {
                return 1;
            }
            return this._value.CompareTo(other._value);
        }


        // UnitGenerateOptions.JsonConverter
        private class IsEnabledVOJsonConverter : JsonConverter<IsEnabledVO>
        {
            public override void Write(Utf8JsonWriter writer, IsEnabledVO value, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(bool)) as JsonConverter<bool>;
                if (converter != null)
                {
                    converter.Write(writer, value._value, options);
                }
                else
                {
                    throw new JsonException($"{typeof(bool)} converter does not found.");
                }
            }

            public override IsEnabledVO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(bool)) as JsonConverter<bool>;
                if (converter != null)
                {
                    try
                    {
                        if (reader.TokenType == JsonTokenType.String)
                        {
                            var stringConverter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                            if (stringConverter == null)
                            {
                                throw options.GetConvertFailureException(typeToConvert);
                            }
                            var stringValue = stringConverter.Read(ref reader, typeToConvert, options);
                            if (stringValue == null)
                            {
                                return null;
                            }
                            var typeConverter = new FilmHouse.Core.Utils.BooleanConverter();
                            var booleanValue = (bool?)typeConverter.ConvertFrom(stringValue);
                            return booleanValue == null ? null : new IsEnabledVO(booleanValue.Value);
                        }

                        var value = converter.Read(ref reader, typeToConvert, options);
                        return new IsEnabledVO(value);
                    }
                    catch (Exception exception)
                    {
                        throw options.GetInvalidValueException(ref reader, typeof(bool), exception);
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
        public class IsEnabledVOValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<IsEnabledVO?, bool?>
        {
            /// <summary>
            /// <see cref="IsEnabledVOValueConverter"/>的新实例。
            /// </summary>
            public IsEnabledVOValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="IsEnabledVOValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public IsEnabledVOValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new IsEnabledVO(x.Value) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                bool value => value,
                IsEnabledVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                IsEnabledVO value => value,
                bool value => new IsEnabledVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class IsEnabledVOArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<IsEnabledVO?[], bool?[]>
        {
            /// <summary>
            /// <see cref="IsEnabledVOArrayValueConverter"/>的新实例。
            /// </summary>
            public IsEnabledVOArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="IsEnabledVOArrayValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public IsEnabledVOArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (bool?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new IsEnabledVO(_.Value)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                bool?[] values => values,
                IsEnabledVO?[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<bool?> values => values.ToArray(),
                IEnumerable<IsEnabledVO?> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                IsEnabledVO?[] values => values,
                bool?[] values => values.Select(_ => _ == null ? null : new IsEnabledVO(_.Value)).ToArray(),
                IEnumerable<IsEnabledVO?> values => values.ToArray(),
                IEnumerable<bool?> values => values.Select(_ => _ == null ? null : new IsEnabledVO(_.Value)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class IsEnabledVOTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(IsEnabledVO);
            private static readonly Type ValueType = typeof(bool);
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
                if (t == typeof(IsEnabledVO))
                {
                    return (IsEnabledVO)value;
                }
                if (t == typeof(bool))
                {
                    return new IsEnabledVO((bool)value);
                }
                if (t == typeof(string))
                {
                    return new IsEnabledVO(bool.Parse((string)value));
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is IsEnabledVO wrappedValue)
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
