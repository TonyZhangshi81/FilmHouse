#nullable enable
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.Services.HttpClients.Models;

/// <summary>
/// 响应的元数据
/// </summary>
public interface IResponseMetadata
{
    /// <summary>
    /// 获取请求ID。
    /// </summary>
    RequestIdVO? RequestId { get; }

    /// <summary>
    /// 获取返还回复的时间。
    /// </summary>
    ApiResponseTimeVO? Timestamp { get; }

    /// <summary>
    /// 获取响应的HTTP状态码。
    /// </summary>
    HttpStatusCodeVO? Status { get; }

    /// <summary>
    /// 获取验证中发生的错误内容。
    /// </summary>
    ICollection<MessageTextVO>? Errors { get; }
}
