#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FilmHouse.Core.ValueObjects.Serialization.Generics;

/// <summary>
/// 是为了能够用JsonConverter对应日期型的类。
/// </summary>
public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private readonly string? _serializationFormat;

    /// <summary>
    /// <see cref="DateOnlyJsonConverter"/>的新实例。
    /// </summary>
    public DateOnlyJsonConverter()
        : this(null)
    {
    }

    /// <summary>
    /// <see cref="DateOnlyJsonConverter"/>的新实例。
    /// </summary>
    /// <param name="serializationFormat"></param>
    public DateOnlyJsonConverter(string? serializationFormat)
    {
        this._serializationFormat = serializationFormat;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return DateOnly.Parse(value!);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(this._serializationFormat ?? "yyyy-MM-dd"));
}
