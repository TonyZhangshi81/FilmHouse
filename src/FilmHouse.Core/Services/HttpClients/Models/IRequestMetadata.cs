using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.Services.HttpClients.Models;

/// <summary>
/// 通过请求接收的元数据
/// </summary>
public interface IRequestMetadata
{
    RequestIdVO? RequestId { get; }
}
