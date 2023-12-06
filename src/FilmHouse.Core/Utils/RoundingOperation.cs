using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmHouse.Core.Utils
{
    /// <summary>
    /// 是表示尾数处理内容的列举型。
    /// </summary>
    public enum RoundingOperation
    {
        /// <summary>
        /// 四舍五入(绝对值计算)
        /// 将数值作为绝对值四舍五入，然后恢复到原来的符号。
        /// </summary>
        Round,
        /// <summary>
        /// 舍弃(绝对值计算)
        /// 将数值作为绝对值舍弃，然后恢复到原来的符号。
        /// </summary>
        RoundDown,
        /// <summary>
        /// 升值(计算绝对值)
        /// 将数值作为绝对值，然后恢复到原来的符号。
        /// </summary>
        RoundUp,
        /// <summary>
        /// 四舍五入(中间值判定)
        /// 将中间值以上的数值处理为大数值，未满中间值的数值处理为小数值。
        /// </summary>
        MRound,
        /// <summary>
        /// 舍弃(数值判断)
        /// 向数值小的方向处理。
        /// </summary>
        Floor,
        /// <summary>
        /// 升值(数值判定)
        /// 向数值大的方向处理。
        /// </summary>
        Ceiling,
    }
}
