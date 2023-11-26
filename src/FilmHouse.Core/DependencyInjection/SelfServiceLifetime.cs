using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Core.DependencyInjection
{
    /// <summary>
    /// 是用来指定DI的生命周期的列举型。
    /// </summary>
    public enum SelfServiceLifetime
    {
        /// <summary>
        /// 指定单一实例。
        /// </summary>
        Singleton = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton,
        /// <summary>
        /// 指定以作用域为单位的实例。
        /// </summary>
        Scoped = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped,
        /// <summary>
        /// 每次都指定生成的实例。
        /// </summary>
        Transient = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient,
        /// <summary>
        /// 指定为不作为DI生成实例的对象。
        /// </summary>
        None = -1,
    }
}
