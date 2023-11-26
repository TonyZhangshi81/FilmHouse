using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FilmHouse.Core.ValueObjects
{
    /// <summary>
    /// <see cref="DateOnly"/>包含的值对象的，包含默认实现的接口。
    /// </summary>
    /// <remarks>
    /// <see cref="DateOnly"/>在这个界面默认实现拥有的属性和方法提供。
    /// </remarks>
    public abstract class DateOnlyValueObjectBase : IFormattable
    {
        /// <summary>
        /// <see cref="IValue{TValue}.AsPrimitive()"/>内部调用方法的方法。
        /// <see cref="DateOnlyValueObjectBase"/>在类中的处理中使用原始类型时被调用。
        /// </summary>
        /// <returns>原始型</returns>
        protected virtual DateOnly AsPrimitiveCore() => ((IValue<System.DateOnly>)this).AsPrimitive();

        /// <summary>
        /// 获取这个实例所表示的月份的日期。
        /// </summary>
        public int Day { get => this.AsPrimitiveCore().Day; }

        /// <summary>
        /// 获取这个实例所表示的星期几。
        /// </summary>
        public DayOfWeek DayOfWeek { get => this.AsPrimitiveCore().DayOfWeek; }

        /// <summary>
        /// 获得这个实例所表示的年累计日。
        /// </summary>
        public int DayOfYear { get => this.AsPrimitiveCore().DayOfYear; }

        /// <summary>
        /// 获得这个实例所表示的日期的月份部分。
        /// </summary>
        public int Month { get => this.AsPrimitiveCore().Month; }

        /// <summary>
        /// 获得这个实例所表示的日期的年份部分。
        /// </summary>
        public int Year { get => this.AsPrimitiveCore().Year; }

        /// <summary>
        /// 该实例的值加上指定天数的新的<see cref="DateOnly" />返还。
        /// </summary>
        /// <param name="value">整数部分和小数部分所组成的天数。有正负两种情况。</param>
        /// <returns>在这个实例所表示的日期和时间<paramref name="value" />中表示的天数相加后的值的对象。</returns>
        public DateOnly AddDays(int value) => this.AsPrimitiveCore().AddDays(value);

        /// <summary>
        /// 这个实例的值加上指定的月数，新的<see cref="DateOnly" />返还。
        /// </summary>
        /// <param name="months">月数正确或负的任何情况都有。</param>
        /// <returns>这个实例所表示的日期、时间和<paramref name="months" />的总和为值的对象。</returns>
        public DateOnly AddMonths(int months) => this.AsPrimitiveCore().AddMonths(months);

        /// <summary>
        /// 将该实例的值加上指定年限的新的<see cref="DateOnly" />返还。
        /// </summary>
        /// <param name="value">年数有正负两种情况。</param>
        /// <returns>在这个实例中表示的日期和时间保持着表示表的年数的对象。</returns>
        public DateOnly AddYears(int value) => this.AsPrimitiveCore().AddYears(value);

        /// <summary>
        /// 将这个实例的日期指定的输入时间设定的值。
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>被组成的日期</returns>
        public virtual DateTime ToDateTime(TimeOnly? time) => this.AsPrimitiveCore().ToDateTime(time ?? TimeOnly.MinValue);

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <param name="format">格式字符串</param>
        /// <returns>表示当前对象的字符串</returns>
        public virtual string ToString(string format) => this.AsPrimitiveCore().ToString(format);

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <param name="format">格式字符串</param>
        /// <param name="provider">将值的格式设定使用的供应商</param>
        /// <returns>表示当前对象的字符串</returns>
        public virtual string ToString(string format, IFormatProvider provider) => this.AsPrimitiveCore().ToString(format, provider);
    }
}
