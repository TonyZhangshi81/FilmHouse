using System.ComponentModel;

namespace FilmHouse.Core.Logging.Categories;

/// <summary>
/// 表示输出应用的整体日志时的类别的类。
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class Core
{
    /// <summary>
    /// 是表示输出错误日志时的类别的类。
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class SystemError
    {
    }

    /// <summary>
    /// 是表示输出错误日志时的类别的类。
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Error
    {
    }
}
