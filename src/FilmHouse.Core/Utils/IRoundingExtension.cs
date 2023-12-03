using System;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Core.Utils
{
    /// <summary>
    /// 保存关于<see cref="decimal"/>的扩展方法的实用程序类。
    /// </summary>
    /// <remarks>
    /// 其他，ValueObject性地把decimal作为内部值持有的东西也一律作为这个方法的对象。
    /// </remarks>
    public static class IRoundingExtension
    {
        internal const string UnitPartsPerOfMethodName = nameof(UnitPartsPerOf);
        internal const string UnitTimesOfMethodName = nameof(UnitTimesOf);

        /// <summary>
        /// 进行数值的单位转换(千日元单位、百万日元单位)。
        /// 尾数根据小数点以下0位的绝对值舍弃。
        /// </summary>
        /// <typeparam name="T">显示数值的类型</typeparam>
        /// <param name="value">数值</param>
        /// <param name="unit">变换单位使用未定义的数值时，请将其分配给<see cref="NumericalUnit"/>。</param>
        /// <param name="operation">端数処理</param>
        /// <returns></returns>
        public static T UnitPartsPerOf<T>(this IRounding<T> value, NumericalUnit unit, RoundingOperation operation = RoundingOperation.RoundDown) =>
            (T)typeof(T).CreateValueObjectInstance((value.AsPrimitive() / (decimal)unit).Round(operation, 0));

        /// <summary>
        /// 进行数值的单位转换(千倍、百万倍)。
        /// 尾数根据小数点以下0位的绝对值舍弃。
        /// </summary>
        /// <typeparam name="T">显示数值的类型</typeparam>
        /// <param name="value">数值</param>
        /// <param name="unit">变换单位使用未定义的数值时，请将其分配给<see cref="NumericalUnit"/>。</param>
        /// <param name="operation">尾数处理</param>
        /// <returns></returns>
        public static T UnitTimesOf<T>(this IRounding<T> value, NumericalUnit unit, RoundingOperation operation = RoundingOperation.RoundDown) =>
            (T)typeof(T).CreateValueObjectInstance((value.AsPrimitive() * (decimal)unit).Round(operation, 0));

        /// <summary>
        /// 端数処理を行います。
        /// </summary>
        /// <typeparam name="T">显示数值的类型</typeparam>
        /// <param name="value">数值</param>
        /// <param name="operation">尾数处理内容</param>
        /// <param name="decimals">有效少数位数</param>
        /// <returns></returns>
        public static T Round<T>(this IRounding<T> value, RoundingOperation operation, int decimals) =>
            operation switch
            {
                RoundingOperation.Ceiling => value.FractionCeiling(decimals),
                RoundingOperation.Floor => value.FractionFloor(decimals),
                RoundingOperation.MRound => value.FractionMRound(decimals),
                RoundingOperation.Round => value.FractionRound(decimals),
                RoundingOperation.RoundDown => value.FractionRoundDown(decimals),
                RoundingOperation.RoundUp => value.FractionRoundUp(decimals),
                _ => throw new ArgumentException(message: $"invalid enum [{nameof(RoundingOperation)}] value", paramName: nameof(operation)),
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
        public static T FractionRound<T>(this IRounding<T> value, int decimals)
        {
            var d = value.AsPrimitive();
            var result = d.FractionRound(decimals);
            return (T)Activator.CreateInstance(typeof(T), result)!;
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
        public static T FractionRoundDown<T>(this IRounding<T> value, int decimals)
        {
            var d = value.AsPrimitive();
            var result = d.FractionRoundDown(decimals);
            return (T)Activator.CreateInstance(typeof(T), result)!;
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
        public static T FractionRoundUp<T>(this IRounding<T> value, int decimals)
        {
            var d = value.AsPrimitive();
            var result = d.FractionRoundUp(decimals);
            return (T)Activator.CreateInstance(typeof(T), result)!;
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
        public static T FractionMRound<T>(this IRounding<T> value, int decimals)
        {
            var d = value.AsPrimitive();
            var result = d.FractionMRound(decimals);
            return (T)Activator.CreateInstance(typeof(T), result)!;
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
        public static T FractionFloor<T>(this IRounding<T> value, int decimals)
        {
            var d = value.AsPrimitive();
            var result = d.FractionFloor(decimals);
            return (T)Activator.CreateInstance(typeof(T), result)!;
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
        public static T FractionCeiling<T>(this IRounding<T> value, int decimals)
        {
            var d = value.AsPrimitive();
            var result = d.FractionCeiling(decimals);
            return (T)Activator.CreateInstance(typeof(T), result)!;
        }
    }
}
