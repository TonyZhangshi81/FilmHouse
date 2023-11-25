using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using FilmHouse.Data.Core.Utils;
using FilmHouse.Data.Core.ValueObjects.Serialization;

namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// 表示请求ID的值对象类。
    /// </summary>
    [JsonConverter(typeof(RequestIdJsonConverter))]
    [ValueConverter(typeof(RequestIdValueConverter), typeof(RequestIdArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(RequestIdTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class RequestIdVO : IEquatable<RequestIdVO>, IComparable<RequestIdVO>, IFormattable, IValue<System.Guid>, IValueObject
    {
        private readonly System.Guid _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public const string TypeName = "RequestID";

        /// <summary>
        /// 取得显示格式。
        /// </summary>
        public const string DisplayFormat = @"{0:D}";

        /// <summary>
        /// 获取值对象包含的原始类型。
        /// </summary>
        public System.Guid AsPrimitive() => this._value;
        /// <summary>
        /// 是不依赖句式而获取原始句式的方法。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public object AsPrimitiveObject() => this.AsPrimitive();

        /// <summary>
        /// <see cref="RequestIdVO"/>
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public RequestIdVO(System.Guid value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        partial void PreProcess(ref System.Guid value);

        partial void Validate();

        /// <summary>
        /// <see cref="System.Guid"/><see cref="RequestIdVO"/>
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator System.Guid(RequestIdVO value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="RequestIdVO"/><see cref="System.Guid"/>
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator RequestIdVO(System.Guid value)
        {
            return new RequestIdVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in RequestIdVO x, in RequestIdVO y)
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
        /// <see cref="System.Guid"/>对句式和包含的原始句式进行比较处理。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RequestIdVO other)
        {
            return Equals(this, other);
        }

        /// <summary>
        /// 判断对象之间是否一致。
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
            if (typeof(RequestIdVO).IsAssignableFrom(t))
            {
                return Equals((RequestIdVO)obj);
            }
            if (t == typeof(System.Guid))
            {
                return this._value.Equals((System.Guid)obj);
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
            return string.Format(DisplayFormat, this._value);
        }

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <param name="format">格式字符串</param>
        /// <returns>表示当前对象的字符串</returns>
        public virtual string ToString(string format) => this.AsPrimitive().ToString(format);

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <param name="format">格式字符串</param>
        /// <param name="provider">用于设定值格式的提供商</param>
        /// <returns>表示当前对象的字符串</returns>
        public virtual string ToString(string format, IFormatProvider provider) => this.AsPrimitive().ToString(format, provider);


        /// <summary>
        /// 是否等于
        /// </summary>
        public static bool operator ==(in RequestIdVO x, in RequestIdVO y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in RequestIdVO x, in RequestIdVO y)
        {
            return !Equals(x, y);
        }

        /// <summary>
        /// 将字符串形式的值转换为等价<see cref="RequestIdVO" />转换成句式。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns><see cref="RequestIdVO"/>型的值</returns>
        public static RequestIdVO Parse(string s)
        {
            return new RequestIdVO(System.Guid.Parse(s));
        }

        /// <summary>
        /// 将字符串形式的值转换为等价<see cref="RequestIdVO" />转换成句式，返回表示转换成功与否的值。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="result"><see cref="RequestIdVO"/>型的值</param>
        /// <returns>参数正常转换时为true。除此之外的情况是false。</returns>
        public static bool TryParse(string s, out RequestIdVO result)
        {
            if (System.Guid.TryParse(s, out var r))
            {
                result = new RequestIdVO(r);
                return true;
            }
            else
            {
                result = default(RequestIdVO);
                return false;
            }
        }






        /// <summary>
        /// 将该实例<paramref name="other" />和比较。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(RequestIdVO other)
        {
            if (other == null)
            {
                return 1;
            }
            return this._value.CompareTo(other._value);
        }


        private class RequestIdJsonConverter : JsonConverter<RequestIdVO>
        {
            public override void Write(Utf8JsonWriter writer, RequestIdVO value, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(System.Guid)) as JsonConverter<System.Guid>;
                if (converter != null)
                {
                    converter.Write(writer, value._value, options);
                }
                else
                {
                    throw new JsonException($"{typeof(System.Guid)} converter does not found.");
                }
            }

            public override RequestIdVO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(System.Guid)) as JsonConverter<System.Guid>;
                if (converter != null)
                {
                    try
                    {
                        var value = converter.Read(ref reader, typeToConvert, options);
                        return new RequestIdVO(value);
                    }
                    catch (Exception exception)
                    {
                        throw options.GetInvalidValueException(ref reader, typeof(System.Guid), exception);
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
        public class RequestIdValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<RequestIdVO, System.Guid?>
        {
            /// <summary>
            /// <see cref="RequestIdValueConverter"/>
            /// </summary>
            public RequestIdValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="RequestIdValueConverter"/>
            /// </summary>
            /// <param name="mappingHints"></param>
            public RequestIdValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new RequestIdVO(x.Value) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object, object> ConvertToProvider => (x) => x switch
            {
                System.Guid value => value,
                RequestIdVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object, object> ConvertFromProvider => (x) => x switch
            {
                RequestIdVO value => value,
                System.Guid value => new RequestIdVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class RequestIdArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<RequestIdVO[], System.Guid?[]>
        {
            /// <summary>
            /// <see cref="RequestIdArrayValueConverter"/>
            /// </summary>
            public RequestIdArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="RequestIdArrayValueConverter"/>
            /// </summary>
            /// <param name="mappingHints"></param>
            public RequestIdArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (System.Guid?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new RequestIdVO(_.Value)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object, object> ConvertToProvider => (x) => x switch
            {
                System.Guid?[] values => values,
                RequestIdVO[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<System.Guid?> values => values.ToArray(),
                IEnumerable<RequestIdVO> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object, object> ConvertFromProvider => (x) => x switch
            {
                RequestIdVO[] values => values,
                System.Guid?[] values => values.Select(_ => _ == null ? null : new RequestIdVO(_.Value)).ToArray(),
                IEnumerable<RequestIdVO> values => values.ToArray(),
                IEnumerable<System.Guid?> values => values.Select(_ => _ == null ? null : new RequestIdVO(_.Value)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class RequestIdTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(RequestIdVO);
            private static readonly Type ValueType = typeof(System.Guid);
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
                if (t == typeof(RequestIdVO))
                {
                    return (RequestIdVO)value;
                }
                if (t == typeof(System.Guid))
                {
                    return new RequestIdVO((System.Guid)value);
                }
                if (t == typeof(string))
                {
                    return new RequestIdVO(System.Guid.Parse((string)value));
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is RequestIdVO wrappedValue)
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
