#nullable enable
namespace FilmHouse.Core.Services.HttpClients.Models;

/// <summary>
/// 定义传递到WebAPI的请求类型的类。
/// </summary>
/// <typeparam name="TMetadata">元数据的类型</typeparam>
public interface IRequestObject<out TMetadata>
    where TMetadata : IRequestMetadata
{
    /// <summary>
    /// 
    /// </summary>
    TMetadata? Metadata { get; }
}