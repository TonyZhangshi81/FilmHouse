using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FilmHouse.Core.Utils
{
    /// <summary>
    /// 是为值对象等追加尾数处理(舍入)处理的接口。
    /// </summary>
    /// <remarks>
    /// 通常你不会只使用这个接口。请一定使用<see cref="IRounding{T}"/>。
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IRounding
    {
    }

    /// <summary>
    /// 是为值对象等追加尾数处理(舍入)处理的接口。
    /// </summary>
    public interface IRounding<T> : IRounding
    {
        /// <summary>
        /// 获取在值对象内部保留的原始类型。
        /// </summary>
        /// <returns></returns>
        decimal AsPrimitive();
    }
}
