using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FilmHouse.Data.Core.ValueObjects.Serialization
{
    /// <summary>
    ///	是用于帮助JSON字符串的转换处理的扩展方法。
    /// </summary>
    public static class JsonSerializerOptionsExtension
    {
        /// <summary>
        /// 它生成一个异常，当JSON的识别过程中发生错误时，需要将其丢弃。
        /// </summary>
        /// <param name="options"></param>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="innerException"></param>
        /// <returns></returns>
        public static Exception GetInvalidValueException(this JsonSerializerOptions options, ref Utf8JsonReader reader, Type typeToConvert, Exception innerException)
        {
            var converter = options.GetConverter(typeof(object)) as JsonConverter<object>;
            if (converter != null)
            {
                try
                {
                    var value = converter.Read(ref reader, typeof(object), options);
                    return new JsonException($"{value}不能当作{typeToConvert}来处理。搜索类似的本地化字符串。", innerException);
                }
                catch (Exception exception)
                {
                    return options.GetConvertFailureException(typeToConvert, exception);
                }
            }
            else
            {
                return options.GetConvertFailureException(typeToConvert);
            }
        }

        /// <summary>
        /// 生成不能转换的情况下的例外。
        /// </summary>
        /// <param name="options"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="innerException"></param>
        /// <returns></returns>
        public static Exception GetConvertFailureException(this JsonSerializerOptions options, Type typeToConvert, Exception innerException = null)
        {
            return new JsonException($"{typeToConvert}的转换失败了。搜索类似的本地化字符串。", innerException);
        }
    }
}
