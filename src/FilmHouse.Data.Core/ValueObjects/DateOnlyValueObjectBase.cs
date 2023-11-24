using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FilmHouse.Data.Core.ValueObjects
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
        /// このインスタンスの値に、指定された月数を加算した新しい <see cref="DateOnly"/> を返します。
        /// </summary>
        /// <param name="months">月数。正または負のどちらの場合もあります。</param>
        /// <returns>このインスタンスで表された日付と時刻と <paramref name="months"/> の合計を値とするオブジェクト。</returns>
        public DateOnly AddMonths(int months) => this.AsPrimitiveCore().AddMonths(months);

        /// <summary>
        /// このインスタンスの値に、指定された年数を加算した新しい <see cref="DateOnly"/> を返します。
        /// </summary>
        /// <param name="value">年数。正または負のどちらの場合もあります。</param>
        /// <returns>このインスタンスで表された日付と時刻に <paramref name="value"/> で表された年数を加算した値を保持するオブジェクト。</returns>
        public DateOnly AddYears(int value) => this.AsPrimitiveCore().AddYears(value);

        /// <summary>
        /// このインスタンスの日付と指定した入力時刻に設定された値を返します。
        /// </summary>
        /// <param name="time">時刻</param>
        /// <returns>構成された日時</returns>
        public virtual DateTime ToDateTime(TimeOnly? time) => this.AsPrimitiveCore().ToDateTime(time ?? TimeOnly.MinValue);

        /// <summary>
        /// 現在のオブジェクトを表す文字列を返します。
        /// </summary>
        /// <param name="format">書式文字列</param>
        /// <returns>現在のオブジェクトを表す文字列</returns>
        public virtual string ToString(string format) => this.AsPrimitiveCore().ToString(format);

        /// <summary>
        /// 現在のオブジェクトを表す文字列を返します。
        /// </summary>
        /// <param name="format">書式文字列</param>
        /// <param name="provider">値の書式設定に使用するプロバイダー</param>
        /// <returns>現在のオブジェクトを表す文字列</returns>
        public virtual string ToString(string format, IFormatProvider provider) => this.AsPrimitiveCore().ToString(format, provider);
    }
}
