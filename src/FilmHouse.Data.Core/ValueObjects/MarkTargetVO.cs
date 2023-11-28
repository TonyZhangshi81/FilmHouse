using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.ValueObjects.Serialization;

namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// MarkTarget的值对象类。
    /// </summary>
    [JsonConverter(typeof(MarkTargetJsonConverter))]
    [ValueConverter(typeof(MarkTargetValueConverter), typeof(MarkTargetArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(MarkTargetTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class MarkTargetVO : IEquatable<MarkTargetVO>, IComparable<MarkTargetVO>, IFormattable, IValue<System.Guid>, IValueObject
    {
        private readonly System.Guid _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public const string TypeName = "MarkTarget";

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
        /// <see cref="MarkTargetVO"/>
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public MarkTargetVO(System.Guid value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        partial void PreProcess(ref System.Guid value);

        partial void Validate();

        /// <summary>
        /// <see cref="System.Guid"/><see cref="MarkTargetVO"/>
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator System.Guid(MarkTargetVO value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="MarkTargetVO"/><see cref="System.Guid"/>
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator MarkTargetVO(System.Guid value)
        {
            return new MarkTargetVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in MarkTargetVO x, in MarkTargetVO y)
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
        public bool Equals(MarkTargetVO other)
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
            if (typeof(MarkTargetVO).IsAssignableFrom(t))
            {
                return Equals((MarkTargetVO)obj);
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
        public static bool operator ==(in MarkTargetVO x, in MarkTargetVO y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in MarkTargetVO x, in MarkTargetVO y)
        {
            return !Equals(x, y);
        }

        /// <summary>
        /// 将字符串形式的值转换为等价<see cref="MarkTargetVO" />转换成句式。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns><see cref="MarkTargetVO"/>型的值</returns>
        public static MarkTargetVO Parse(string s)
        {
            return new MarkTargetVO(System.Guid.Parse(s));
        }

        /// <summary>
        /// 将字符串形式的值转换为等价<see cref="MarkTargetVO" />转换成句式，返回表示转换成功与否的值。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="result"><see cref="MarkTargetVO"/>型的值</param>
        /// <returns>参数正常转换时为true。除此之外的情况是false。</returns>
        public static bool TryParse(string s, out MarkTargetVO result)
        {
            if (System.Guid.TryParse(s, out var r))
            {
                result = new MarkTargetVO(r);
                return true;
            }
            else
            {
                result = default(MarkTargetVO);
                return false;
            }
        }






        /// <summary>
        /// 将该实例<paramref name="other" />和比较。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(MarkTargetVO other)
        {
            if (other == null)
            {
                return 1;
            }
            return this._value.CompareTo(other._value);
        }


        private class MarkTargetJsonConverter : JsonConverter<MarkTargetVO>
        {
            public override void Write(Utf8JsonWriter writer, MarkTargetVO value, JsonSerializerOptions options)
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

            public override MarkTargetVO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(System.Guid)) as JsonConverter<System.Guid>;
                if (converter != null)
                {
                    try
                    {
                        var value = converter.Read(ref reader, typeToConvert, options);
                        return new MarkTargetVO(value);
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
        public class MarkTargetValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<MarkTargetVO, System.Guid?>
        {
            /// <summary>
            /// <see cref="MarkTargetValueConverter"/>
            /// </summary>
            public MarkTargetValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="MarkTargetValueConverter"/>
            /// </summary>
            /// <param name="mappingHints"></param>
            public MarkTargetValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new MarkTargetVO(x.Value) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object, object> ConvertToProvider => (x) => x switch
            {
                System.Guid value => value,
                MarkTargetVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object, object> ConvertFromProvider => (x) => x switch
            {
                MarkTargetVO value => value,
                System.Guid value => new MarkTargetVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class MarkTargetArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<MarkTargetVO[], System.Guid?[]>
        {
            /// <summary>
            /// <see cref="MarkTargetArrayValueConverter"/>
            /// </summary>
            public MarkTargetArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="MarkTargetArrayValueConverter"/>
            /// </summary>
            /// <param name="mappingHints"></param>
            public MarkTargetArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (System.Guid?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new MarkTargetVO(_.Value)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object, object> ConvertToProvider => (x) => x switch
            {
                System.Guid?[] values => values,
                MarkTargetVO[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<System.Guid?> values => values.ToArray(),
                IEnumerable<MarkTargetVO> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object, object> ConvertFromProvider => (x) => x switch
            {
                MarkTargetVO[] values => values,
                System.Guid?[] values => values.Select(_ => _ == null ? null : new MarkTargetVO(_.Value)).ToArray(),
                IEnumerable<MarkTargetVO> values => values.ToArray(),
                IEnumerable<System.Guid?> values => values.Select(_ => _ == null ? null : new MarkTargetVO(_.Value)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class MarkTargetTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(MarkTargetVO);
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
                if (t == typeof(MarkTargetVO))
                {
                    return (MarkTargetVO)value;
                }
                if (t == typeof(System.Guid))
                {
                    return new MarkTargetVO((System.Guid)value);
                }
                if (t == typeof(string))
                {
                    return new MarkTargetVO(System.Guid.Parse((string)value));
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is MarkTargetVO wrappedValue)
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
