using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// 值表示该接口是对象的接口。
    /// </summary>
    /// <remarks>
    /// 实现此接口的类型被视为值对象。
    /// 这个接口分支了屏幕和模型之间的绑定过程。
    /// </remarks>
    public interface IValueObject
    {
        /// <summary>
        /// 这是一种独立于类型获取基本类型的方法。
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        object AsPrimitiveObject();
    }
}
