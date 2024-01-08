using System.ComponentModel;

namespace FilmHouse.Core.Logging.Categories;

/// <summary>
/// 演示层的Web类日志输出时的类别的类。
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class Web
{
    /// <summary>
    /// 输出WebAPI日志时的类别。
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Api
    {
        /// <summary>
        /// 在输出WebAPI的开始日志时的类别。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public sealed class Begin
        {
            /// <summary>
            /// WebAPI的参数值的日志，表示信息输出时的类别的类。
            /// </summary>
            [EditorBrowsable(EditorBrowsableState.Never)]
            public sealed class Parameter
            {
                /// <summary>
                /// WebAPI的参数包含信息的同时输出日志的类别的类。
                /// </summary>
                [EditorBrowsable(EditorBrowsableState.Never)]
                public sealed class Sensitive
                {
                }
            }
        }

        /// <summary>
        /// 在输出WebAPI结束日志时的类别。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public sealed class End
        {
            /// <summary>
            /// WebAPI的参数值的日志，表示信息输出时的类别的类。
            /// </summary>
            [EditorBrowsable(EditorBrowsableState.Never)]
            public sealed class Parameter
            {
                /// <summary>
                /// WebAPI的参数包含信息的同时输出日志的类别的类。
                /// </summary>
                [EditorBrowsable(EditorBrowsableState.Never)]
                public sealed class Sensitive
                {
                }
            }
        }

        /// <summary>
        /// 指示在输出日志时跟踪WebAPI的API密钥认证的处理细节。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public sealed class Authorize
        {
            /// <summary>
            /// 是指示跟踪WebAPI的API密钥认证的处理细节的日志的，包含信息输出日志时的类别的类。
            /// </summary>
            [EditorBrowsable(EditorBrowsableState.Never)]
            public sealed class Sensitive
            {
            }
        }
    }
}
