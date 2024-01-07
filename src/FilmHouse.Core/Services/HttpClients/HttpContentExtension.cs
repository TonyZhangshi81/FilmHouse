using System.Text.Json;
using FilmHouse.Core.Utils;

namespace FilmHouse.Core.Services.HttpClients;

public static class HttpContentExtension
{
    public static TModel ToModel<TModel>(this HttpContent content)
        where TModel : class
    {
        var options = JsonSerializerOptionsFactory.GetInstance();
        return content.ToModel<TModel>(options);
    }

    public static TModel ToModel<TModel>(this HttpContent content, JsonSerializerOptions options)
        where TModel : class
    {

        var json = content.ReadAsStringAsync().Result;
        var model = JsonSerializer.Deserialize<TModel>(json, options);
        Guard.Requires<NotSupportedException>(model != null);

        return model;
    }

}
