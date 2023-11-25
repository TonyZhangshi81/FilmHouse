using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using FilmHouse.Data.Core.Utils;
using FilmHouse.Data.Core.ValueObjects.Serialization;

namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// 是表示创建日期时间的值对象类。
    /// </summary>
    [JsonConverter(typeof(UpDatedOnJsonConverter))]
    [ValueConverter(typeof(UpDatedOnValueConverter), typeof(UpDatedOnArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(UpDatedOnTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class UpDatedOnVO : DateTimeValueObjectBase, IEquatable<UpDatedOnVO>, IComparable<UpDatedOnVO>, IValue<System.DateTime>, IValueObject
    {
        private readonly System.DateTime _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public const string TypeName = "UpDatedOn";

        /// <summary>
        /// 取得显示格式。
        /// </summary>
        public const string DisplayFormat = @"{0:yyyy/MM/dd HH:mm:ss.fff}";

        /// <summary>
        /// 获取值对象包含的原始类型。
        /// </summary>
        public System.DateTime AsPrimitive() => this._value;
        /// <summary>
        /// 是不依赖句式而获取原始句式的方法。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public object AsPrimitiveObject() => this.AsPrimitive();

        /// <summary>
        /// <see cref="UpDatedOnVO"/>
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public UpDatedOnVO(System.DateTime value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        partial void PreProcess(ref System.DateTime value);

        partial void Validate();

        /// <summary>
        /// <see cref="System.DateTime"/>向<see cref="UpDatedOnVO"/>显示转型
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator System.DateTime(UpDatedOnVO value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="UpDatedOnVO"/>向<see cref="System.DateTime"/>显示转型
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator UpDatedOnVO(System.DateTime value)
        {
            return new UpDatedOnVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in UpDatedOnVO x, in UpDatedOnVO y)
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
        /// <see cref="System.DateTime"/>对句式和包含的原始句式进行比较处理。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(UpDatedOnVO other)
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
            if (typeof(UpDatedOnVO).IsAssignableFrom(t))
            {
                return Equals((UpDatedOnVO)obj);
            }
            if (t == typeof(System.DateTime))
            {
                return this._value.Equals((System.DateTime)obj);
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
        /// 是否等于
        /// </summary>
        public static bool operator ==(in UpDatedOnVO x, in UpDatedOnVO y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in UpDatedOnVO x, in UpDatedOnVO y)
        {
            return !Equals(x, y);
        }

        // UnitGenerateOptions.ParseMethod

        /// <summary>
        /// 将字符串形式的值转换为等价<see cref="UpDatedOnVO" />转换成句式。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns><see cref="UpDatedOnVO"/>类型值</returns>
        public static UpDatedOnVO Parse(string s)
        {
            return new UpDatedOnVO(System.DateTime.Parse(s));
        }

        /// <summary>
        /// 将字符串形式的值转换为等价<see cref="UpDatedOnVO" />转换成句式，返回表示转换成功与否的值。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="result"><see cref="UpDatedOnVO"/>类型值</param>
        /// <returns>参数正常转换时为true。除此之外的情况是false。</returns>
        public static bool TryParse(string s, out UpDatedOnVO result)
        {
            if (System.DateTime.TryParse(s, out var r))
            {
                result = new UpDatedOnVO(r);
                return true;
            }
            else
            {
                result = default(UpDatedOnVO);
                return false;
            }
        }






        // UnitGenerateOptions.Comparable

        /// <summary>
        /// 将该实例与<paramref name="other" />比较。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(UpDatedOnVO other)
        {
            if (other == null)
            {
                return 1;
            }
            return this._value.CompareTo(other._value);
        }

        /// <summary>
        /// 大于运算
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >(in UpDatedOnVO x, in UpDatedOnVO y)
        {
            return x._value > y._value;
        }

        /// <summary>
        /// 小于运算
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <(in UpDatedOnVO x, in UpDatedOnVO y)
        {
            return x._value < y._value;
        }

        /// <summary>
        /// 大于等于运算
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >=(in UpDatedOnVO x, in UpDatedOnVO y)
        {
            return x._value >= y._value;
        }

        /// <summary>
        /// 小于等于运算
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <=(in UpDatedOnVO x, in UpDatedOnVO y)
        {
            return x._value <= y._value;
        }


        // UnitGenerateOptions.JsonConverter
        private class UpDatedOnJsonConverter : JsonConverter<UpDatedOnVO>
        {
            public override void Write(Utf8JsonWriter writer, UpDatedOnVO value, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(System.DateTime)) as JsonConverter<System.DateTime>;
                if (converter != null)
                {
                    converter.Write(writer, value._value.ToUniversalTime(), options);
                }
                else
                {
                    throw new JsonException($"{typeof(System.DateTime)} converter does not found.");
                }
            }

            public override UpDatedOnVO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(System.DateTime)) as JsonConverter<System.DateTime>;
                if (converter != null)
                {
                    try
                    {
                        var value = converter.Read(ref reader, typeToConvert, options);
                        return new UpDatedOnVO(value.ToLocalTime());
                    }
                    catch (Exception exception)
                    {
                        throw options.GetInvalidValueException(ref reader, typeof(System.DateTime), exception);
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
        public class UpDatedOnValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<UpDatedOnVO, System.DateTime?>
        {
            /// <summary>
            /// <see cref="UpDatedOnValueConverter"/>
            /// </summary>
            public UpDatedOnValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="UpDatedOnValueConverter"/>
            /// </summary>
            /// <param name="mappingHints"></param>
            public UpDatedOnValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new UpDatedOnVO(x.Value) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object, object> ConvertToProvider => (x) => x switch
            {
                System.DateTime value => value,
                UpDatedOnVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object, object> ConvertFromProvider => (x) => x switch
            {
                UpDatedOnVO value => value,
                System.DateTime value => new UpDatedOnVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class UpDatedOnArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<UpDatedOnVO[], System.DateTime?[]>
        {
            /// <summary>
            /// <see cref="UpDatedOnArrayValueConverter"/>
            /// </summary>
            public UpDatedOnArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="UpDatedOnArrayValueConverter"/>
            /// </summary>
            /// <param name="mappingHints"></param>
            public UpDatedOnArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (System.DateTime?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new UpDatedOnVO(_.Value)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object, object> ConvertToProvider => (x) => x switch
            {
                System.DateTime?[] values => values,
                UpDatedOnVO[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<System.DateTime?> values => values.ToArray(),
                IEnumerable<UpDatedOnVO> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object, object> ConvertFromProvider => (x) => x switch
            {
                UpDatedOnVO[] values => values,
                System.DateTime?[] values => values.Select(_ => _ == null ? null : new UpDatedOnVO(_.Value)).ToArray(),
                IEnumerable<UpDatedOnVO> values => values.ToArray(),
                IEnumerable<System.DateTime?> values => values.Select(_ => _ == null ? null : new UpDatedOnVO(_.Value)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class UpDatedOnTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(UpDatedOnVO);
            private static readonly Type ValueType = typeof(System.DateTime);
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
                if (t == typeof(UpDatedOnVO))
                {
                    return (UpDatedOnVO)value;
                }
                if (t == typeof(System.DateTime))
                {
                    return new UpDatedOnVO((System.DateTime)value);
                }
                if (t == typeof(string))
                {
                    return new UpDatedOnVO(System.DateTime.Parse((string)value));
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is UpDatedOnVO wrappedValue)
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
