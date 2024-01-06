using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.Utils;

public static class ObjectExtension
{
    /// <summary>
    /// 将对象转换成JSON字符串。
    /// </summary>
    /// <param name="source"></param>
    /// <remarks>
    /// 将对象设置为JSON格式的字符串，然后保存。
    /// </remarks>
    public static string ToJsonString(this object source)
    {
        var options = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = false,
            AllowTrailingCommas = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
        };

        var json = JsonSerializer.Serialize(source, options);
        return json;
    }

    /// <summary>
    /// 返回到指定类型的转换结果。
    /// </summary>
    /// <param name="self"></param>
    /// <param name="castToType"></param>
    public static object CastTo(this object self, Type castToType)
    {
        if (!self.GetType().IsValueObject())
        {
            return Converter.ChangeType(self, castToType)!;
        }

        var valueObject = (IValueObject)self;
        if (!castToType.IsValueObject())
        {
            return Converter.ChangeType(valueObject.AsPrimitiveObject(), castToType)!;
        }
        return castToType.CreateValueObjectInstance(valueObject.AsPrimitiveObject());
    }

    /// <summary>
    /// 返回到指定类型的转换结果。
    /// </summary>
    /// <param name="self"></param>
    public static T CastTo<T>(this object self)
    {
        return (T)CastTo(self, typeof(T));
    }
}
