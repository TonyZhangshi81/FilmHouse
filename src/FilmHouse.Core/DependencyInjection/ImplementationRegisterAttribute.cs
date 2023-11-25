using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Core.DependencyInjection
{
    /// <summary>
    /// 对DI注册的实装型赋予。
    /// </summary>
    /// <remarks>
    /// 这个属性赋予要注册服务的实装类型，设定服务注册时的优先顺序。
    /// 但是这个优先顺序，只在同一组合件内有效。
    /// 即使没有设定优先顺序，注册服务本身也没有问题。
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public class ImplementationRegisterAttribute : Attribute
    {
        /// <summary>
        /// <see cref="ImplementationRegisterAttribute"/>的新实例。
        /// </summary>
        /// <param name="priority">注册的先后顺序数值越小，优先级越高。但是成为同一配件内的优先顺序。</param>
        /// <param name="enabled">DI的登记对象时设定为真。</param>
        public ImplementationRegisterAttribute(int priority = int.MaxValue, bool enabled = true)
        {
            this.Priority = priority;
            this.Enabled = enabled;
        }

        /// <summary>
        /// 取得这个服务的注册优先顺序。
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// 获取这个服务是否是DI的注册对象。
        /// </summary>
        public bool Enabled { get; }
    }
}
