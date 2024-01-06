#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FilmHouse.Core.ValueObjects.Serialization.Generics;

/// <summary>
/// 是为了能够用JsonConverter应对时刻型的班。
/// </summary>
public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    private readonly string? _serializationFormat;

    /// <summary>
    /// <see cref="TimeOnlyJsonConverter"/>的新实例。
    /// </summary>
    public TimeOnlyJsonConverter()
        : this(null)
    {
    }

    /// <summary>
    /// <see cref="DateOnlyJsonConverter"/>的新实例。
    /// </summary>
    /// <param name="serializationFormat"></param>
    public TimeOnlyJsonConverter(string? serializationFormat)
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
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return TimeOnly.Parse(value!);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(this._serializationFormat ?? "HH:mm:ss.fff"));
}
