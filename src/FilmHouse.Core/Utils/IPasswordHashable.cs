using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FilmHouse.Core.Utils
{
    /// <summary>
    /// 是用来进行密码散列化的接口。
    /// </summary>
    /// <remarks>
    /// 通常你不会只使用这个接口。请务必使用<see cref="IPasswordHashable{T}"/>。
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IPasswordHashable
    {
    }

    /// <summary>
    /// 是用来进行密码散列化的接口。
    /// </summary>
    public interface IPasswordHashable<T> : IPasswordHashable
    {
        /// <summary>
        /// 获取在值对象内部保留的原始类型。
        /// </summary>
        /// <returns></returns>
        string AsPrimitive();
    }
}
