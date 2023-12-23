#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.Utils.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class EFMath
    {
        /// <summary>
        /// 回复绝对值。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short? Abs(IValue<short>? value) => value != null ? System.Math.Abs(value.AsPrimitive()) : null;
        /// <summary>
        /// 回复绝对值。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? Abs(IValue<int>? value) => value != null ? System.Math.Abs(value.AsPrimitive()) : null;
        /// <summary>
        /// 回复绝对值。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long? Abs(IValue<long>? value) => value != null ? System.Math.Abs(value.AsPrimitive()) : null;
        /// <summary>
        /// 回复绝对值。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal? Abs(IValue<decimal>? value) => value != null ? System.Math.Abs(value.AsPrimitive()) : null;

        /// <summary>
        /// 返回指定的十进制以上的数中最小的整数值。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal? Ceiling(IValue<decimal>? value) => value != null ? System.Math.Ceiling(value.AsPrimitive()) : null;

        /// <summary>
        /// 返回指定的十进制以下的数中最大的整数值。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal? Floor(IValue<decimal>? value) => value != null ? System.Math.Floor(value.AsPrimitive()) : null;

        /// <summary>
        /// 十进制的值被舍入最近的整数值，中间值被舍入最近的偶数值。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal? Round(IValue<decimal>? value) => value != null ? System.Math.Round(value.AsPrimitive()) : null;
        /// <summary>
        /// 将十进制的值舍入小数点以下的位数。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals">舍入后的数值小数点以下的位数是从0到28的值。</param>
        /// <returns></returns>
        public static decimal? Round(IValue<decimal>? value, int decimals) => value != null ? System.Math.Round(value.AsPrimitive(), decimals) : null;

        /// <summary>
        /// 计算指定的十进制的整数部分。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal? Truncate(IValue<decimal>? value) => value != null ? System.Math.Truncate(value.AsPrimitive()) : null;
    }
}
