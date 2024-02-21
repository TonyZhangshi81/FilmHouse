#nullable enable
using System.Text.Json;
using System.Text.Json.Serialization;
using FilmHouse.Core.Utils.Data;
using FilmHouse.Core.ValueObjects.Serialization;

namespace FilmHouse.Core.ValueObjects
{
    /// <summary>
    /// 配置键的值对象类。
    /// </summary>
    [JsonConverter(typeof(ConfigKeyJsonConverter))]
    [ValueConverter(typeof(ConfigKeyValueConverter), typeof(ConfigKeyArrayValueConverter))]
    [System.ComponentModel.TypeConverter(typeof(ConfigKeyTypeConverter))]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.Runtime.CompilerServices.CompilerGenerated]
    public partial class ConfigKeyVO : FilmHouse.Core.ValueObjects.TextBase, IEquatable<ConfigKeyVO>, IComparable<ConfigKeyVO>, IValue<string>, IValueObject
    {
        private readonly string _value;

        /// <summary>
        /// 取得型名。
        /// </summary>
        public new const string TypeName = "ConfigKey(size:50)";

        /// <summary>
        /// 取得位数。
        /// </summary>
        public const int Size = 50;

        /// <summary>
        /// 提供代码组持有的代码值的常数定义。
        /// </summary>
        public static class Keys
        {
            public static readonly ConfigKeyVO Name = new("WebSiteSettings:Name");
            public static readonly ConfigKeyVO SubTitle = new("WebSiteSettings:SubTitle");
            public static readonly ConfigKeyVO Version = new("WebSiteSettings:Version");
            public static readonly ConfigKeyVO WebpagesEnabled = new("WebSiteSettings:WebpagesEnabled");
            public static readonly ConfigKeyVO ClientValidationEnabled = new("WebSiteSettings:ClientValidationEnabled");
            public static readonly ConfigKeyVO UnobtrusiveJavaScriptEnabled = new("WebSiteSettings:UnobtrusiveJavaScriptEnabled");
            // 首頁每日發現件數限制
            public static readonly ConfigKeyVO HomeDiscoveryMaxPage = new("Home:Discovery:MaxPage");
            // 最新欄目顯示件數
            public static readonly ConfigKeyVO HomeDiscoveryNewMovies = new("Home:Discovery:NewMovies");
            // 熱門欄目顯示件數
            public static readonly ConfigKeyVO HomeDiscoveryMostMovies = new("Home:Discovery:MostMovies");
            // 评论缩略显示长度
            public static readonly ConfigKeyVO MovieSummaryShort = new("Movie:Summary:Short");
            // 影片页面上显示的最大评论件数
            public static readonly ConfigKeyVO MovieCommentMax = new("Movie:Comment:Max");
            // 电影卡片明星显示最大个数
            public static readonly ConfigKeyVO WorkItemCelebMax = new("CelebWorkItem:Celeb:Max");
            // 檢索頁單頁顯示的件數限制
            public static readonly ConfigKeyVO MovieSearchPageMax = new("Movie:Search:Max");
            // 影集頁單頁顯示的影片件數限制
            public static readonly ConfigKeyVO AlbumPageMovieMax = new("Album:Movie:Max");
        }

        /// <summary>
        /// <see cref="ConfigKeyVO"/>的新实例。
        /// </summary>
        /// <param name="value">值对象包含的原始类型</param>
        public ConfigKeyVO(string value)
            : base(value)
        {
            this.PreProcess(ref value);
            this._value = value;
            this.Validate();
        }

        partial void PreProcess(ref string value);

        partial void Validate();

        /// <summary>
        /// <see cref="string"/>向<see cref="ConfigKeyVO"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator string(ConfigKeyVO value)
        {
            return value._value;
        }

        /// <summary>
        /// <see cref="ConfigKeyVO"/>向<see cref="string"/>进行隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator ConfigKeyVO(string value)
        {
            return new ConfigKeyVO(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static bool Equals(in ConfigKeyVO? x, in ConfigKeyVO? y)
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
        public bool Equals(ConfigKeyVO? other)
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
            if (typeof(ConfigKeyVO).IsAssignableFrom(t))
            {
                return Equals((ConfigKeyVO)obj);
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
        public static bool operator ==(in ConfigKeyVO? x, in ConfigKeyVO? y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// 是否不相等
        /// </summary>
        public static bool operator !=(in ConfigKeyVO? x, in ConfigKeyVO? y)
        {
            return !Equals(x, y);
        }






        // UnitGenerateOptions.ComparableInterfaceOnly

        /// <summary>
        /// 将该实例<paramref name="other" />和比较。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(ConfigKeyVO? other)
        {
            if (other == null)
            {
                return 1;
            }
            return this._value.CompareTo(other._value);
        }


        // UnitGenerateOptions.JsonConverter
        private class ConfigKeyJsonConverter : JsonConverter<ConfigKeyVO>
        {
            public override void Write(Utf8JsonWriter writer, ConfigKeyVO value, JsonSerializerOptions options)
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

            public override ConfigKeyVO? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var converter = options.GetConverter(typeof(string)) as JsonConverter<string>;
                if (converter != null)
                {
                    try
                    {
                        var value = converter.Read(ref reader, typeToConvert, options);
                        return value != null ? new ConfigKeyVO(value.Replace("\r\n", "\n")) : null;
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
        public class ConfigKeyValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<ConfigKeyVO?, string?>
        {
            /// <summary>
            /// <see cref="ConfigKeyValueConverter"/>的新实例。
            /// </summary>
            public ConfigKeyValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="ConfigKeyValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public ConfigKeyValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x != null ? x._value : null,
                        convertFromProviderExpression: x => x != null ? new ConfigKeyVO(x) : null,
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                string value => value,
                ConfigKeyVO value => value._value,
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                ConfigKeyVO value => value,
                string value => new ConfigKeyVO(value),
                _ => null,
            };
        }

        /// <summary>
        /// EntityFrameworkCore和值对象进行相互转换的转换器类。
        /// </summary>
        public class ConfigKeyArrayValueConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<ConfigKeyVO?[], string?[]>
        {
            /// <summary>
            /// <see cref="ConfigKeyArrayValueConverter"/>的新实例。
            /// </summary>
            public ConfigKeyArrayValueConverter()
                : this(null)
            {
            }

            /// <summary>
            /// <see cref="ConfigKeyArrayValueConverter"/>的新实例。
            /// </summary>
            /// <param name="mappingHints"></param>
            public ConfigKeyArrayValueConverter(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ConverterMappingHints? mappingHints = null)
                : base(
                        convertToProviderExpression: x => x.Select(_ => _ == null ? (string?)null : _._value).ToArray(),
                        convertFromProviderExpression: x => x.Select(_ => _ == null ? null : new ConfigKeyVO(_)).ToArray(),
                        mappingHints: mappingHints)
            {
            }

            /// <summary>
            /// 当将数据写入存储时，获取转换对象的函数，设置为处理空、装箱和非严格匹配的简单类型匹配。
            /// </summary>
            public override Func<object?, object?> ConvertToProvider => (x) => x switch
            {
                string?[] values => values,
                ConfigKeyVO?[] values => values.Select(_ => _?._value).ToArray(),
                IEnumerable<string?> values => values.ToArray(),
                IEnumerable<ConfigKeyVO?> values => values.Select(_ => _?._value).ToArray(),
                _ => null,
            };

            /// <summary>
            /// 当从存储中读取数据时，获取转换对象的函数。该函数设置为处理空、装箱和非严格匹配的简单类型的匹配。
            /// </summary>
            public override Func<object?, object?> ConvertFromProvider => (x) => x switch
            {
                ConfigKeyVO?[] values => values,
                string?[] values => values.Select(_ => _ == null ? null : new ConfigKeyVO(_)).ToArray(),
                IEnumerable<ConfigKeyVO?> values => values.ToArray(),
                IEnumerable<string?> values => values.Select(_ => _ == null ? null : new ConfigKeyVO(_)).ToArray(),
                _ => null,
            };
        }

        // Default
        private class ConfigKeyTypeConverter : System.ComponentModel.TypeConverter
        {
            private static readonly Type WrapperType = typeof(ConfigKeyVO);
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
                if (t == typeof(ConfigKeyVO))
                {
                    return (ConfigKeyVO)value;
                }
                if (t == typeof(string))
                {
                    return new ConfigKeyVO((string)value);
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object? ConvertTo(System.ComponentModel.ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
            {
                if (value == null)
                {
                    return null;
                }

                if (value is ConfigKeyVO wrappedValue)
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
