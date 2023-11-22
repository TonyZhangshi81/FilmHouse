using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// <see cref="DateOnly"/>を内包する値オブジェクトの、デフォルト実装を含んだインターフェースです。
    /// </summary>
    /// <remarks>
    /// <see cref="DateOnly"/>が保有するプロパティやメソッドはこのインターフェイスでデフォルト実装することで提供します。
    /// </remarks>
    public abstract class DateOnlyValueObjectBase : IFormattable
    {
        /// <summary>
        /// <see cref="IValue{TValue}.AsPrimitive()"/>メソッドを内部的に呼び出すメソッド。
        /// <see cref="DateOnlyValueObjectBase"/>クラス内の処理でプリミティブ型を使用する場合に呼び出される。
        /// </summary>
        /// <returns>プリミティブ型</returns>
        protected virtual DateOnly AsPrimitiveCore() => ((IValue<System.DateOnly>)this).AsPrimitive();

        /// <summary>
        /// このインスタンスで表される月の日付を取得します。
        /// </summary>
        public int Day { get => this.AsPrimitiveCore().Day; }

        /// <summary>
        /// このインスタンスで表される曜日を取得します。
        /// </summary>
        public DayOfWeek DayOfWeek { get => this.AsPrimitiveCore().DayOfWeek; }

        /// <summary>
        /// このインスタンスで表される年間積算日を取得します。
        /// </summary>
        public int DayOfYear { get => this.AsPrimitiveCore().DayOfYear; }

        /// <summary>
        /// このインスタンスで表される日付の月の部分を取得します。
        /// </summary>
        public int Month { get => this.AsPrimitiveCore().Month; }

        /// <summary>
        /// このインスタンスで表される日付の年の部分を取得します。
        /// </summary>
        public int Year { get => this.AsPrimitiveCore().Year; }

        /// <summary>
        /// このインスタンスの値に、指定された日数を加算した新しい <see cref="DateOnly"/> を返します。
        /// </summary>
        /// <param name="value">整数部と小数部から成る日数。正または負のどちらの場合もあります。</param>
        /// <returns>このインスタンスで表された日付と時刻に <paramref name="value"/> で表された日数を加算した値を保持するオブジェクト。</returns>
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
