using System;

namespace FilmHouse.Core.Utils
{
    /// <summary>
    /// 关于<see cref="decimal"/>的扩展方法的实用程序类。
    /// </summary>
    /// <remarks>
    /// 其他，ValueObject性地把decimal作为内部值持有的东西也一律作为这个方法的对象。
    /// </remarks>
    public static class DecimalExtension
    {
        /// <summary>
        /// 进行尾数处理。
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="operation">尾数处理内容</param>
        /// <param name="decimals">有效少数位数</param>
        /// <returns></returns>
        public static decimal Round(this decimal value, RoundingOperation operation, int decimals) =>
            operation switch
            {
                RoundingOperation.Ceiling => value.FractionCeiling(decimals),
                RoundingOperation.Floor => value.FractionFloor(decimals),
                RoundingOperation.MRound => value.FractionMRound(decimals),
                RoundingOperation.Round => value.FractionRound(decimals),
                RoundingOperation.RoundDown => value.FractionRoundDown(decimals),
                RoundingOperation.RoundUp => value.FractionRoundUp(decimals),
                _ => throw new ArgumentException($"The invalid value is specified as {nameof(RoundingOperation)}. Search for localized strings similar to [{operation}].")
            };

        /// <summary>
        /// 四舍五入(绝对值计算)
        /// 将数值作为绝对值四舍五入，然后恢复到原来的符号。
        /// </summary>
        /// <remarks>
        /// 按指定的小数点以下位数四舍五入。将数值作为绝对值四舍五入，然后恢复到原来的符号。
        /// 与<see cref="FractionMRound"/>在处理正数值上没有区别。
        /// </remarks>
        /// <example>
        /// -9.44 → -9.4
        /// -9.45 → -9.5
        /// </example>
        /// <param name="value">decimal值</param>
        /// <param name="decimals">有效少数位数</param>
        /// <exception cref="ArgumentException">有效的少数位数被设定为0 ~ 28位以外的值的情况下被抛出。</exception>
        public static decimal FractionRound(this decimal value, int decimals)
        {
            AssertDecimalsRange(decimals);

            if (decimal.Remainder(Convert.ToDecimal(Math.Pow(10, decimals)) * value, 1) != 0)
            {
                value += 5.0m / Convert.ToDecimal(Math.Pow(10, decimals + 1)) * Math.Sign(value);
                value -= decimal.Remainder(value, 1.0m / Convert.ToDecimal(Math.Pow(10, decimals)));
            }

            return value.FractionFloor(decimals);
        }

        /// <summary>
        /// 舍弃(绝对值计算)
        /// 将数值作为绝对值舍弃，然后恢复到原来的符号。
        /// </summary>
        /// <remarks>
        /// 按指定的小数点以下位数舍去。将数值作为绝对值舍弃，然后恢复到原来的符号。
        /// 与<see cref="FractionFloor"/>在处理正数值上没有区别。
        /// </remarks>
        /// <example>
        /// -9.44 → -9.4
        /// </example>
        /// <param name="value">decimal值</param>
        /// <param name="decimals">有效少数位数</param>
        /// <exception cref="ArgumentException">有效的少数位数被设定为0 ~ 28位以外的值的情况下被抛出。</exception>
        public static decimal FractionRoundDown(this decimal value, int decimals)
        {
            AssertDecimalsRange(decimals);

            value *= Convert.ToDecimal(Math.Pow(10, decimals));
            value = decimal.Truncate(value);
            value /= Convert.ToDecimal(Math.Pow(10, decimals));
            return value;
        }

        /// <summary>
        /// 升值(计算绝对值)
        /// 将数值作为绝对值，然后恢复到原来的符号。
        /// </summary>
        /// <remarks>
        /// 按指定的小数点以下位数舍去。将数值作为绝对值，然后恢复到原来的符号。
        /// 与<see cref="FractionCeiling"/>在处理正数值上没有区别。
        /// </remarks>
        /// <example>
        /// -9.44 → -9.5
        /// </example>
        /// <param name="value">decimal值</param>
        /// <param name="decimals">有效少数位数</param>
        /// <exception cref="ArgumentException">有效的少数位数被设定为0 ~ 28位以外的值的情况下被抛出。</exception>
        public static decimal FractionRoundUp(this decimal value, int decimals)
        {
            AssertDecimalsRange(decimals);

            // 为了升值处理而采取绝对值
            var d = Math.Abs(value);

            // 结束判定预处理
            d *= Convert.ToDecimal(Math.Pow(10, decimals + 1));
            d = decimal.Truncate(d) / 10;
            var decAbs = d;
            d = decimal.Truncate(decAbs);

            // 升值判定
            if (d != decAbs)
            {
                d += 1;
            }

            // 恢复到原来的位数
            d /= Convert.ToDecimal(Math.Pow(10, decimals));

            // 判断原值是正还是负
            if (value < 0)
            {
                // 如果是负数，就回到负数
                d *= -1;
            }

            return d;
        }

        /// <summary>
        /// 四舍五入(中间值判定)
        /// 将中间值以上的数值处理为大数值，未满中间值的数值处理为小数值。
        /// </summary>
        /// <remarks>
        /// 按指定的小数点以下位数四舍五入。准确地说，中间值以上处理为大数值，中间值以下处理为小数值。
        /// 四和五这个数值没有太大的意义，要通过如何处理中间值来实施处理。
        /// 与<see cref="FractionRound"/>在处理正数值上没有区别。
        /// </remarks>
        /// <example>
        /// -9.45 → -9.4
        /// -9.46 → -9.5
        /// </example>
        /// <param name="value">decimal值</param>
        /// <param name="decimals">有效少数位数</param>
        /// <exception cref="ArgumentException">有效的少数位数被设定为0 ~ 28位以外的值的情况下被抛出。</exception>
        public static decimal FractionMRound(this decimal value, int decimals)
        {
            AssertDecimalsRange(decimals);

            if (decimal.Remainder(Convert.ToDecimal(Math.Pow(10, decimals)) * value, 1) != 0)
            {
                value += 5.0m / Convert.ToDecimal(Math.Pow(10, decimals + 1));
                value -= decimal.Remainder(value, 1.0m / Convert.ToDecimal(Math.Pow(10, decimals + 1)));
            }

            return value.FractionFloor(decimals);
        }

        /// <summary>
        /// 舍弃(数值判断)
        /// 向数值小的方向处理。
        /// </summary>
        /// <remarks>
        /// 按指定的小数点以下位数舍去。舍弃向数值小的方向处理。
        /// 与<see cref="FractionRoundDown"/>在处理正数值上没有区别。
        /// </remarks>
        /// <example>
        /// -9.44 → -9.5
        /// </example>
        /// <param name="value">decimal值</param>
        /// <param name="decimals">有效少数位数</param>
        /// <exception cref="ArgumentException">有效的少数位数被设定为0 ~ 28位以外的值的情况下被抛出。</exception>
        public static decimal FractionFloor(this decimal value, int decimals)
        {
            AssertDecimalsRange(decimals);

            if (decimals == 0)
            {
                value = decimal.Floor(value);
            }
            else
            {
                var d = decimal.Floor(Convert.ToDecimal(Math.Pow(10, decimals)) * value);
                value = d / Convert.ToDecimal(Math.Pow(10, decimals));
            }

            return value;
        }

        /// <summary>
        /// 升值(数值判定)
        /// 向数值大的方向处理。
        /// </summary>
        /// <remarks>
        /// 以指定的小数点以下的位数结束。升值是朝着数值大的方向处理的。
        /// 与<see cref="FractionRoundUp"/>在处理正数值上没有区别。
        /// </remarks>
        /// <example>
        /// -9.44 → -9.4
        /// </example>
        /// <param name="value">decimal值</param>
        /// <param name="decimals">有效少数位数</param>
        /// <exception cref="ArgumentException">有效的少数位数被设定为0 ~ 28位以外的值的情况下被抛出。</exception>
        public static decimal FractionCeiling(this decimal value, int decimals)
        {
            AssertDecimalsRange(decimals);

            value *= Convert.ToDecimal(Math.Pow(10, decimals));
            value = decimal.Ceiling(value);
            value /= Convert.ToDecimal(Math.Pow(10, decimals));

            return value;
        }

        private static void AssertDecimalsRange(int decimals)
        {
            if (decimals < 0 || decimals > 28)
            {
                throw new ArgumentException($"The value deviates from the number of significant digits. Specify values in the range 0 to 28. Searches localized strings that are similar to.");
            }
        }
    }
}
