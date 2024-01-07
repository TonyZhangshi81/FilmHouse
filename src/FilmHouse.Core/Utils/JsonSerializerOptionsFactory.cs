using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using FilmHouse.Core.ValueObjects.Serialization.Generics;

namespace FilmHouse.Core.Utils;

/// <summary>
/// <see cref="JsonSerializerOptions"/>
/// </summary>
public class JsonSerializerOptionsFactory
{
    private static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new JsonSerializerOptions()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = false,
        AllowTrailingCommas = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
    };

    private static readonly JsonSerializerOptions WebJsonSerializerOptions = new JsonSerializerOptions()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = false,
        AllowTrailingCommas = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        PropertyNameCaseInsensitive = true,
    };

    private static readonly JsonSerializerOptions IndentedJsonSerializerOptions = new JsonSerializerOptions()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = false,
        AllowTrailingCommas = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
    };

    private static readonly JsonSerializerOptions LogJsonSerializerOptions = new JsonSerializerOptions()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = false,
        AllowTrailingCommas = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
    };

    /// <summary>
    /// 
    /// </summary>
    static JsonSerializerOptionsFactory()
    {
        AddCustomConverters(DefaultJsonSerializerOptions);
        AddCustomConverters(WebJsonSerializerOptions);
        AddCustomConverters(IndentedJsonSerializerOptions);
        AddCustomConverters(LogJsonSerializerOptions);
        LogJsonSerializerOptions.Converters.Add(new DictionaryStringObjectJsonConverter());
    }

    /// <summary>
    /// 获取符合指定选项的实例。
    /// </summary>
    /// <param name="writeIndented"></param>
    /// <returns></returns>
    public static JsonSerializerOptions GetInstance(bool writeIndented = false)
    {
        return writeIndented ? IndentedJsonSerializerOptions : DefaultJsonSerializerOptions;
    }

    /// <summary>
    /// 获取符合指定选项的实例。
    /// </summary>
    /// <returns></returns>
    public static JsonSerializerOptions GetWebInstance()
    {
        return WebJsonSerializerOptions;
    }

    /// <summary>
    /// 获取符合指定选项的实例。
    /// </summary>
    /// <returns></returns>
    public static JsonSerializerOptions GetLogInstance()
    {
        return LogJsonSerializerOptions;
    }

    /// <summary>
    /// 注册自定义转换器。
    /// </summary>
    /// <param name="options"></param>
    public static void AddCustomConverters(JsonSerializerOptions options)
    {
        options.Converters.Add(new DateOnlyJsonConverter());
        options.Converters.Add(new TimeOnlyJsonConverter());
    }
}
