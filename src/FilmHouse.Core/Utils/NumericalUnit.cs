using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmHouse.Core.Utils
{
    /// <summary>
    /// 是表示单位转换时的转换种类的列举型。
    /// </summary>
    public enum NumericalUnit
    {
        /// <summary>
        /// 转换成1 / 1的值。
        /// </summary>
        One = 1,
        /// <summary>
        /// 转换成千分之一的值。
        /// </summary>
        Thousand = 1000,
        /// <summary>
        /// 转换成百万分之一的值。
        /// </summary>
        Million = 1000000,
    }
}
