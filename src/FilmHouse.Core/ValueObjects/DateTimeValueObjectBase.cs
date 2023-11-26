#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Core.ValueObjects
{
    /// <summary>
    /// <see cref="DateTime"/>包含的值对象的，包含默认实现的接口。
    /// </summary>
    /// <remarks>
    /// <see cref="DateTime"/>在这个界面默认实现拥有的属性和方法提供。
    /// </remarks>
    public abstract class DateTimeValueObjectBase : IFormattable, IConvertible
    {
        /// <summary>
        /// <see cref="IValue{TValue}.AsPrimitive()"/>内部调用方法的方法。
        /// <see cref="DateTimeValueObjectBase"/>在类中的处理中使用原始类型时被调用。
        /// </summary>
        /// <returns>原始型</returns>
        protected virtual DateTime AsPrimitiveCore() => ((IValue<System.DateTime>)this).AsPrimitive();

        /// <summary>
        /// 获取这个实例的日期部分。
        /// </summary>
        public DateTime Date { get => this.AsPrimitiveCore().Date; }

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
        /// 获得这个实例所表示的日期的时间部分。
        /// </summary>
        public int Hour { get => this.AsPrimitiveCore().Hour; }

        /// <summary>
        /// 取得表示这个实例表示的时间的种类(当地时间，世界协定时间(UTC)，或者，两者都不是)的值。
        /// </summary>
        public DateTimeKind Kind { get => this.AsPrimitiveCore().Kind; }

        /// <summary>
        /// 获取这个实例所表示的日期的毫秒部分。
        /// </summary>
        public int Millisecond { get => this.AsPrimitiveCore().Millisecond; }

        /// <summary>
        /// 获得这个实例中所表示的日期的部分。
        /// </summary>
        public int Minute { get => this.AsPrimitiveCore().Minute; }

        /// <summary>
        /// 获得这个实例所表示的日期的月份部分。
        /// </summary>
        public int Month { get => this.AsPrimitiveCore().Month; }

        /// <summary>
        /// 获得这个实例所表示的日期的秒的部分。
        /// </summary>
        public int Second { get => this.AsPrimitiveCore().Second; }

        /// <summary>
        /// 获取这个实例的日期和时间的计时器刻度。
        /// </summary>
        public long Ticks { get => this.AsPrimitiveCore().Ticks; }

        /// <summary>
        /// 获取这个实例的时间。
        /// </summary>
        public TimeSpan TimeOfDay { get => this.AsPrimitiveCore().TimeOfDay; }

        /// <summary>
        /// 获得这个实例所表示的日期的年份部分。
        /// </summary>
        public int Year { get => this.AsPrimitiveCore().Year; }

        /// <summary>
        /// 对于该实例的值，指定<see cref="timespan" />的值加起来的新<see cref="DateTime" />返还。
        /// </summary>
        /// <param name="value">正负时间间隔</param>
        /// <returns>在这个实例所表示的日期和时间<paramref name="value" />中表示的加上时间间隔的值的对象。</returns>
        public DateTime Add(TimeSpan value) => this.AsPrimitiveCore().Add(value);

        /// <summary>
        /// 该实例的值加上指定天数的新的<see cref="DateTime" />返还。
        /// </summary>
        /// <param name="value">整数部分和小数部分所组成的天数。有正负两种情况。</param>
        /// <returns>在这个实例所表示的日期和时间<paramref name="value" />中表示的天数相加后的值的对象。</returns>
        public DateTime AddDays(double value) => this.AsPrimitiveCore().AddDays(value);

        /// <summary>
        /// 该实例的值加上指定小时数的新的<see cref="DateTime" />返还。
        /// </summary>
        /// <param name="value">由整数部分和小数部分组成的小时数。有正负两种情况。</param>
        /// <returns>在这个实例所表示的日期和时间<paramref name="value"/>中所表示的时间数相加后的值的对象。</returns>
        public DateTime AddHours(double value) => this.AsPrimitiveCore().AddHours(value);

        /// <summary>
        /// 该实例的值加上指定分钟数的新的<see cref="DateTime" />返还。
        /// </summary>
        /// <param name="value">由整数部分和小数部分组成的毫秒数。有正负两种情况。这个值被舍入近似的整数。</param>
        /// <returns>在这个实例所表示的日期和时间<paramref name="value"/>中表示的加上毫秒数的值的对象。</returns>
        public DateTime AddMilliseconds(double value) => this.AsPrimitiveCore().AddMilliseconds(value);

        /// <summary>
        /// 该实例的值加上指定秒数的新的<see cref="DateTime" />返还。
        /// </summary>
        /// <param name="value">由整数部分和小数部分组成的分数有正负两种情况。</param>
        /// <returns>在这个实例所表示的日期和时间<paramref name="value"/>中所表示的分数相加后的值的对象。</returns>
        public DateTime AddMinutes(double value) => this.AsPrimitiveCore().AddMinutes(value);

        /// <summary>
        /// 该实例的值加上指定月数的新的<see cref="DateTime" />返还。
        /// </summary>
        /// <param name="months">月数有正负两种情况。</param>
        /// <returns>这个实例所表示的日期、时间和<paramref name="months"/>的总和为值的对象。</returns>
        public DateTime AddMonths(int months) => this.AsPrimitiveCore().AddMonths(months);

        /// <summary>
        /// 该实例的值加上指定秒数的新的<see cref="DateTime" />返还。
        /// </summary>
        /// <param name="value">由整数部分和小数部分组成的秒数。有正负两种情况。</param>
        /// <returns>在这个实例所表示的日期和时间<paramref name="value"/>中表示的秒数相加后的值的对象。</returns>
        public DateTime AddSeconds(double value) => this.AsPrimitiveCore().AddSeconds(value);

        /// <summary>
        /// 在该实例的值上加上指定的计时器刻度的数目的新的<see cref="DateTime" />返还。
        /// </summary>
        /// <param name="value">100纳秒计时器刻度。有正负两种情况。</param>
        /// <returns>在这个实例所表示的日期和时间<paramref name="value"/>中所表示的时间数相加后的值的对象。</returns>
        public DateTime AddTicks(long value) => this.AsPrimitiveCore().AddTicks(value);

        /// <summary>
        /// 该实例的值加上指定年数的新的<see cref="DateTime" />返还。
        /// </summary>
        /// <param name="value">年数有正负两种情况。</param>
        /// <returns>在这个实例所表示的日期和时间<paramref name="value"/>中表示的年数相加后的值的对象。</returns>
        public DateTime AddYears(int value) => this.AsPrimitiveCore().AddYears(value);

        /// <summary>
        /// 表示这个实例是否在现在的时区的夏令时的期间内。
        /// </summary>
        /// <returns></returns>
        public bool IsDaylightSavingTime() => this.AsPrimitiveCore().IsDaylightSavingTime();

        /// <summary>
        /// 从该实例的值减去指定的日期和时间后，新的<see cref="DateTime" />返还。
        /// </summary>
        /// <param name="value">减去的日期和时刻的值。</param>
        /// <returns>从这个实例所表示的日期和时间<paramref name="value"/>中表示的日期和时间相减后的值和时间间隔相等。</returns>
        public TimeSpan Subtract(DateTime value) => this.AsPrimitiveCore().Subtract(value);

        /// <summary>
        /// 从该实例的值减去指定的日期和时间后，新的<see cref="DateTime" />返还。
        /// </summary>
        /// <param name="value">减去的时间间隔。</param>
        /// <returns>从这个实例所表示的日期和时间<paramref name="value"/>中所表示的与减去时间间隔后的值相等的对象。</returns>
        public DateTime Subtract(TimeSpan value) => this.AsPrimitiveCore().Subtract(value);

        /// <summary>
        /// 将当前实例串行化为64位二进制值。之后，使用这个值，<see cref="DateTime" />可以重新构建对象。
        /// </summary>
        /// <returns></returns>
        public long ToBinary() => this.AsPrimitiveCore().ToBinary();

        /// <summary>
        /// 将当前实例的值转换为当地时间。
        /// </summary>
        /// <returns></returns>
        public DateTime ToLocalTime() => this.AsPrimitiveCore().ToLocalTime();

        /// <summary>
        /// 现在的实例的值转换为世界协定时间(UTC)。
        /// </summary>
        /// <returns></returns>
        public DateTime ToUniversalTime() => this.AsPrimitiveCore().ToUniversalTime();

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <param name="format">書式文字列</param>
        /// <returns>表示当前对象的字符串</returns>
        public virtual string ToString(string? format) => this.AsPrimitiveCore().ToString(format);

        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <param name="format">書式文字列</param>
        /// <param name="provider">値の書式設定に使用するプロバイダー</param>
        /// <returns>表示当前对象的字符串</returns>
        public virtual string ToString(string? format, IFormatProvider? provider) => this.AsPrimitiveCore().ToString(format, provider);

        #region IConvertible
        /// <summary>
        /// 返回表示当前对象的字符串。
        /// </summary>
        /// <param name="provider">用于设定值格式的提供商</param>
        /// <returns>表示当前对象的字符串</returns>
        public virtual string ToString(IFormatProvider? provider) => this.AsPrimitiveCore().ToString(provider);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TypeCode GetTypeCode() => this.AsPrimitiveCore().GetTypeCode();
        bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToBoolean(provider);
        byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToByte(provider);
        char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToChar(provider);
        DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToDateTime(provider);
        decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToDecimal(provider);
        double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToDouble(provider);
        short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToInt16(provider);
        int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToInt32(provider);
        long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToInt64(provider);
        sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToSByte(provider);
        float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToSingle(provider);
        /// <summary>
        /// 在继承元类的情况下，需要转换并返回到所求的类型。
        /// </summary>
        /// <param name="conversionType"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
        {
            if (this.GetType().IsAssignableFrom(conversionType))
            {
                return conversionType.CreateValueObjectInstance(this.AsPrimitiveCore());
            }
            return ((IConvertible)this.AsPrimitiveCore()).ToType(conversionType, provider);
        }
        ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToUInt16(provider);
        uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToUInt32(provider);
        ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)this.AsPrimitiveCore()).ToUInt64(provider);
        #endregion
    }
}
