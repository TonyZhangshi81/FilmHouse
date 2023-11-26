using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FilmHouse.Core.ValueObjects
{
    /// <summary>
    /// <see cref="DateTime"/>包含的值对象的，包含默认实现的接口。
    /// </summary>
    /// <remarks>
    /// <see cref="DateTime"/>在这个界面默认实现拥有的属性和方法提供。
    /// </remarks>
    public abstract class TimeOnlyValueObjectBase : IFormattable
    {
        /// <summary>
        /// <see cref="IValue{TValue}.AsPrimitive()"/>内部调用方法的方法。
        /// <see cref="TimeOnlyValueObjectBase"/>在类中的处理中使用原始类型时被调用。
        /// </summary>
        /// <returns>原始型</returns>
        protected virtual TimeOnly AsPrimitiveCore() => ((IValue<TimeOnly>)this).AsPrimitive();

        /// <summary>
        /// 获得这个实例所表示的日期的时间部分。
        /// </summary>
        public int Hour { get => this.AsPrimitiveCore().Hour; }

        /// <summary>
        /// 获取这个实例所表示的日期的毫秒部分。
        /// </summary>
        public int Millisecond { get => this.AsPrimitiveCore().Millisecond; }

        /// <summary>
        /// 获得这个实例中所表示的日期的部分。
        /// </summary>
        public int Minute { get => this.AsPrimitiveCore().Minute; }

        /// <summary>
        /// 获得这个实例所表示的日期的秒的部分。
        /// </summary>
        public int Second { get => this.AsPrimitiveCore().Second; }

        /// <summary>
        /// 获取这个实例的日期和时间的计时器刻度。
        /// </summary>
        public long Ticks { get => this.AsPrimitiveCore().Ticks; }

        /// <summary>
        /// 将该实例的值加上指定的时间数的新的<see cref="TimeOnly" / ></see>
        /// </summary>
        /// <param name="value">将该实例的值加上指定的时间数的新的<cref="TimeOnly" />返还。</param>
        /// <returns>在这个实例所表示的日期和时间<paramref name="value" />中所表示的时间数相加后的值的对象。</returns>
        public TimeOnly AddHours(double value) => this.AsPrimitiveCore().AddHours(value);

        /// <summary>
        /// 将该实例的值加上指定分数的新<see cref="TimeOnly" / & gt;返还。
        /// </summary>
        /// <param name="value">由整数部分和小数部分组成的分数有正负两种情况。</param>
        /// <returns>在这个实例所表示的日期和时间<paramref name = "value" /></paramref>中所表示的分数相加后的值的对象。</returns>
        public TimeOnly AddMinutes(double value) => this.AsPrimitiveCore().AddMinutes(value);

        /// <summary>
        /// 将该实例<see cref="timespan" />返回转换后的值。
        /// </summary>
        /// <returns></returns>
        public TimeSpan ToTimeSpan() => this.AsPrimitiveCore().ToTimeSpan();

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <param name="format">書式文字列</param>
        /// <returns>表示当前对象的字符串</returns>
        public virtual string ToString(string format) => this.AsPrimitiveCore().ToString(format);

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <param name="format">回回表示当前对象的字符串。</param>
        /// <param name="provider">用于设定值格式的提供商</param>
        /// <returns>表示当前对象的字符串</returns>
        public virtual string ToString(string format, IFormatProvider provider) => this.AsPrimitiveCore().ToString(format, provider);
    }
}
