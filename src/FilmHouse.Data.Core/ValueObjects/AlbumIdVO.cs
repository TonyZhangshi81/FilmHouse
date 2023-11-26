using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.ValueObjects.Serialization;

namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// AlbumId的值对象类。
    /// </summary>
    [JsonConverter(typeof(AlbumIdJsonConverter))]
    [ValueConverter(typeof(AlbumIdValueConverter), typeof(AlbumIdArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(AlbumIdTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class AlbumIdVO : IEquatable<AlbumIdVO>, IComparable<AlbumIdVO>, IFormattable, IValue<System.Guid>, IValueObject
    {
        private readonly System.Guid _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public const string TypeName = "AlbumId";

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
        /// <see cref="AlbumIdVO"/>
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public AlbumIdVO(System.Guid value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        partial void PreProcess(ref System.Guid value);

        partial void Validate();

        /// <summary>
        /// <see cref="System.Guid"/><see cref="AlbumIdVO"/>
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator System.Guid(AlbumIdVO value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="AlbumIdVO"/><see cref="System.Guid"/>
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator AlbumIdVO(System.Guid value)
        {
            return new AlbumIdVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in AlbumIdVO x, in AlbumIdVO y)
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
        public bool Equals(AlbumIdVO other)
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
            if (typeof(AlbumIdVO).IsAssignableFrom(t))
            {
                return Equals((AlbumIdVO)obj);
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
        public static bool operator ==(in AlbumIdVO x, in AlbumIdVO y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in AlbumIdVO x, in AlbumIdVO y)
        {
            return !Equals(x, y);
        }

        /// <summary>
        /// 将字符串形式的值转换为等价<see cref="AlbumIdVO" />转换成句式。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns><see cref="AlbumIdVO"/>型的值</returns>
        public static AlbumIdVO Parse(string s)
        {
            return new AlbumIdVO(System.Guid.Parse(s));
        }

        /// <summary>
        /// 将字符串形式的值转换为等价<see cref="AlbumIdVO" />转换成句式，返回表示转换成功与否的值。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="result"><see cref="AlbumIdVO"/>型的值</param>
        /// <returns>参数正常转换时为true。除此之外的情况是false。</returns>
        public static bool TryParse(string s, out AlbumIdVO result)
        {
            if (System.Guid.TryParse(s, out var r))
            {
                result = new AlbumIdVO(r);
                return true;
            }
            else
            {
                result = default(AlbumIdVO);
                return false;
            }
        }






        /// <summary>
        /// 将该实例<paramref name="other" />和比较。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(AlbumIdVO other)
        {
            if (other == null)
            {
                return 1;
            }
            return this._value.CompareTo(other._value);
        }


        private class AlbumIdJsonConverter : JsonConverter<AlbumIdVO>
        {
            public override void Write(Utf8JsonWriter writer, AlbumIdVO value, JsonSerializerOptions options)
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

            public override AlbumIdVO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(System.Guid)) as JsonConverter<System.Guid>;
                if (converter != null)
                {
                    try
                    {
                        var value = converter.Read(ref reader, typeToConvert, options);
                        return new AlbumIdVO(value);
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
        public class AlbumIdValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<AlbumIdVO, System.Guid?>
        {
            /// <summary>
            /// <see cref="AlbumIdValueConverter"/>
            /// </summary>
            public AlbumIdValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="AlbumIdValueConverter"/>
            /// </summary>
            /// <param name="mappingHints"></param>
            public AlbumIdValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new AlbumIdVO(x.Value) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object, object> ConvertToProvider => (x) => x switch
            {
                System.Guid value => value,
                AlbumIdVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object, object> ConvertFromProvider => (x) => x switch
            {
                AlbumIdVO value => value,
                System.Guid value => new AlbumIdVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class AlbumIdArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<AlbumIdVO[], System.Guid?[]>
        {
            /// <summary>
            /// <see cref="AlbumIdArrayValueConverter"/>
            /// </summary>
            public AlbumIdArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="AlbumIdArrayValueConverter"/>
            /// </summary>
            /// <param name="mappingHints"></param>
            public AlbumIdArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (System.Guid?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new AlbumIdVO(_.Value)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object, object> ConvertToProvider => (x) => x switch
            {
                System.Guid?[] values => values,
                AlbumIdVO[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<System.Guid?> values => values.ToArray(),
                IEnumerable<AlbumIdVO> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object, object> ConvertFromProvider => (x) => x switch
            {
                AlbumIdVO[] values => values,
                System.Guid?[] values => values.Select(_ => _ == null ? null : new AlbumIdVO(_.Value)).ToArray(),
                IEnumerable<AlbumIdVO> values => values.ToArray(),
                IEnumerable<System.Guid?> values => values.Select(_ => _ == null ? null : new AlbumIdVO(_.Value)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class AlbumIdTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(AlbumIdVO);
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
                if (t == typeof(AlbumIdVO))
                {
                    return (AlbumIdVO)value;
                }
                if (t == typeof(System.Guid))
                {
                    return new AlbumIdVO((System.Guid)value);
                }
                if (t == typeof(string))
                {
                    return new AlbumIdVO(System.Guid.Parse((string)value));
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is AlbumIdVO wrappedValue)
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
