using FilmHouse.Core.Exceptions;

namespace FilmHouse.Core.Presentation.Web.Filters;

/// <summary>
/// 例外类，用于在WebAPI的API密钥认证时抛出NG的理由。
/// </summary>
/// <remarks>
/// 实际上这个异常不会直接被抛出，一定会在catch的基础上作为其他异常的内部异常使用。
/// </remarks>
public class WebApiInvalidResonException : FilmHouseErrorException
{
    /// <summary>
    /// <see cref="WebApiInvalidResonException"/>
    /// </summary>
    /// <param name="message"></param>
    public WebApiInvalidResonException(string message) : base(message)
    {
    }
}
