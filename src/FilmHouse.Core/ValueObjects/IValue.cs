using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmHouse.Core.ValueObjects
{
    /// <summary>
    /// 它是一个从值对象获取值的接口。
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IValue<TValue> : IValueObject
        where TValue : notnull
    {
        /// <summary>
        /// 这是一个获取基本类型的方法。
        /// </summary>
        /// <returns></returns>
        TValue AsPrimitive();
    }
}
