using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmHouse.Data.Core.Utils;

namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// <see cref="DateTime"/>を内包する値オブジェクトの、デフォルト実装を含んだインターフェースです。
    /// </summary>
    /// <remarks>
    /// <see cref="DateTime"/>が保有するプロパティやメソッドはこのインターフェイスでデフォルト実装することで提供します。
    /// </remarks>
    public abstract class DateTimeValueObjectBase : IFormattable, IConvertible
    {
        /// <summary>
        /// <see cref="IValue{TValue}.AsPrimitive()"/>メソッドを内部的に呼び出すメソッド。
        /// <see cref="DateTimeValueObjectBase"/>クラス内の処理でプリミティブ型を使用する場合に呼び出される。
        /// </summary>
        /// <returns>プリミティブ型</returns>
        protected virtual DateTime AsPrimitiveCore() => ((IValue<System.DateTime>)this).AsPrimitive();

        /// <summary>
        /// このインスタンスの日付の部分を取得します。
        /// </summary>
        public DateTime Date { get => this.AsPrimitiveCore().Date; }

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
        /// このインスタンスで表される日付の時間の部分を取得します。
        /// </summary>
        public int Hour { get => this.AsPrimitiveCore().Hour; }

        /// <summary>
        /// このインスタンスが表す時刻の種類 (現地時刻、世界協定時刻 (UTC)、または、そのどちらでもない) を示す値を取得します。
        /// </summary>
        public DateTimeKind Kind { get => this.AsPrimitiveCore().Kind; }

        /// <summary>
        /// このインスタンスで表される日付のミリ秒の部分を取得します。
        /// </summary>
        public int Millisecond { get => this.AsPrimitiveCore().Millisecond; }

        /// <summary>
        /// このインスタンスで表される日付の分の部分を取得します。
        /// </summary>
        public int Minute { get => this.AsPrimitiveCore().Minute; }

        /// <summary>
        /// このインスタンスで表される日付の月の部分を取得します。
        /// </summary>
        public int Month { get => this.AsPrimitiveCore().Month; }

        /// <summary>
        /// このインスタンスで表される日付の秒の部分を取得します。
        /// </summary>
        public int Second { get => this.AsPrimitiveCore().Second; }

        /// <summary>
        /// このインスタンスの日付と時刻を表すタイマー刻み数を取得します。
        /// </summary>
        public long Ticks { get => this.AsPrimitiveCore().Ticks; }

        /// <summary>
        /// このインスタンスの時刻を取得します。
        /// </summary>
        public TimeSpan TimeOfDay { get => this.AsPrimitiveCore().TimeOfDay; }

        /// <summary>
        /// このインスタンスで表される日付の年の部分を取得します。
        /// </summary>
        public int Year { get => this.AsPrimitiveCore().Year; }

        /// <summary>
        /// このインスタンスの値に、指定された <see cref="TimeSpan"/> の値を加算した新しい <see cref="DateTime"/> を返します。
        /// </summary>
        /// <param name="value">正または負の時間間隔。</param>
        /// <returns>このインスタンスで表された日付と時刻に <paramref name="value"/> で表された時間間隔を加算した値を保持するオブジェクト。</returns>
        public DateTime Add(TimeSpan value) => this.AsPrimitiveCore().Add(value);

        /// <summary>
        /// このインスタンスの値に、指定された日数を加算した新しい <see cref="DateTime"/> を返します。
        /// </summary>
        /// <param name="value">整数部と小数部から成る日数。正または負のどちらの場合もあります。</param>
        /// <returns>このインスタンスで表された日付と時刻に <paramref name="value"/> で表された日数を加算した値を保持するオブジェクト。</returns>
        public DateTime AddDays(double value) => this.AsPrimitiveCore().AddDays(value);

        /// <summary>
        /// このインスタンスの値に、指定された時間数を加算した新しい <see cref="DateTime"/> を返します。
        /// </summary>
        /// <param name="value">整数部と小数部から成る時間数。正または負のどちらの場合もあります。</param>
        /// <returns>このインスタンスで表された日付と時刻に <paramref name="value"/> で表された時間数を加算した値を保持するオブジェクト。</returns>
        public DateTime AddHours(double value) => this.AsPrimitiveCore().AddHours(value);

        /// <summary>
        /// このインスタンスの値に、指定されたミリ秒数を加算した新しい <see cref="DateTime"/> を返します。
        /// </summary>
        /// <param name="value">整数部と小数部から成るミリ秒数。正または負のどちらの場合もあります。 この値は、近似値の整数に丸められます。</param>
        /// <returns>このインスタンスで表された日付と時刻に <paramref name="value"/> で表されたミリ秒数を加算した値を保持するオブジェクト。</returns>
        public DateTime AddMilliseconds(double value) => this.AsPrimitiveCore().AddMilliseconds(value);

        /// <summary>
        /// このインスタンスの値に、指定された分数を加算した新しい <see cref="DateTime"/> を返します。
        /// </summary>
        /// <param name="value">整数部と小数部から成る分数。正または負のどちらの場合もあります。</param>
        /// <returns>このインスタンスで表された日付と時刻に <paramref name="value"/> で表された分数を加算した値を保持するオブジェクト。</returns>
        public DateTime AddMinutes(double value) => this.AsPrimitiveCore().AddMinutes(value);

        /// <summary>
        /// このインスタンスの値に、指定された月数を加算した新しい <see cref="DateTime"/> を返します。
        /// </summary>
        /// <param name="months">月数。正または負のどちらの場合もあります。</param>
        /// <returns>このインスタンスで表された日付と時刻と <paramref name="months"/> の合計を値とするオブジェクト。</returns>
        public DateTime AddMonths(int months) => this.AsPrimitiveCore().AddMonths(months);

        /// <summary>
        /// このインスタンスの値に、指定された秒数を加算した新しい <see cref="DateTime"/> を返します。
        /// </summary>
        /// <param name="value">整数部と小数部から成る秒数。正または負のどちらの場合もあります。</param>
        /// <returns>このインスタンスで表された日付と時刻に <paramref name="value"/> で表された秒数を加算した値を保持するオブジェクト。</returns>
        public DateTime AddSeconds(double value) => this.AsPrimitiveCore().AddSeconds(value);

        /// <summary>
        /// このインスタンスの値に、指定されたタイマー刻みの数を加算した新しい <see cref="DateTime"/> を返します。
        /// </summary>
        /// <param name="value">100 ナノ秒タイマー刻み数。正または負のどちらの場合もあります。</param>
        /// <returns>このインスタンスで表された日付と時刻に <paramref name="value"/> で表された時間数を加算した値を保持するオブジェクト。</returns>
        public DateTime AddTicks(long value) => this.AsPrimitiveCore().AddTicks(value);

        /// <summary>
        /// このインスタンスの値に、指定された年数を加算した新しい <see cref="DateTime"/> を返します。
        /// </summary>
        /// <param name="value">年数。正または負のどちらの場合もあります。</param>
        /// <returns>このインスタンスで表された日付と時刻に <paramref name="value"/> で表された年数を加算した値を保持するオブジェクト。</returns>
        public DateTime AddYears(int value) => this.AsPrimitiveCore().AddYears(value);

        /// <summary>
        /// このインスタンスが、現在のタイム ゾーンの夏時間の期間内であるかどうかを示します。
        /// </summary>
        /// <returns></returns>
        public bool IsDaylightSavingTime() => this.AsPrimitiveCore().IsDaylightSavingTime();

        /// <summary>
        /// このインスタンスの値から指定した日時を減算した、新しい <see cref="TimeSpan"/> を返します。
        /// </summary>
        /// <param name="value">減算する日付と時刻の値。</param>
        /// <returns>このインスタンスで表された日付と時刻から <paramref name="value"/> で表された日付と時刻を減算した値と等しい時間間隔。</returns>
        public TimeSpan Subtract(DateTime value) => this.AsPrimitiveCore().Subtract(value);

        /// <summary>
        /// このインスタンスの値から指定した期間を減算した、新しい <see cref="DateTime"/> を返します。
        /// </summary>
        /// <param name="value">減算する時間間隔。</param>
        /// <returns>このインスタンスで表された日付と時刻から <paramref name="value"/> で表された時間間隔を減算した値と等しいオブジェクト。</returns>
        public DateTime Subtract(TimeSpan value) => this.AsPrimitiveCore().Subtract(value);

        /// <summary>
        /// 現在のインスタンスを 64 ビットのバイナリ値にシリアル化します。後で、この値を使って、<see cref="DateTime"/> オブジェクトを再構築できます。
        /// </summary>
        /// <returns></returns>
        public long ToBinary() => this.AsPrimitiveCore().ToBinary();

        /// <summary>
        /// 現在のインスタンスの値を現地時刻に変換します。
        /// </summary>
        /// <returns></returns>
        public DateTime ToLocalTime() => this.AsPrimitiveCore().ToLocalTime();

        /// <summary>
        /// 現在のインスタンスの値を世界協定時刻 (UTC) に変換します。
        /// </summary>
        /// <returns></returns>
        public DateTime ToUniversalTime() => this.AsPrimitiveCore().ToUniversalTime();

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

        #region IConvertible
        /// <summary>
        /// 現在のオブジェクトを表す文字列を返します。
        /// </summary>
        /// <param name="provider">値の書式設定に使用するプロバイダー</param>
        /// <returns>現在のオブジェクトを表す文字列</returns>
        public virtual string ToString(IFormatProvider provider) => this.AsPrimitiveCore().ToString(provider);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TypeCode GetTypeCode() => this.AsPrimitiveCore().GetTypeCode();
        bool IConvertible.ToBoolean(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToBoolean(provider);
        byte IConvertible.ToByte(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToByte(provider);
        char IConvertible.ToChar(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToChar(provider);
        DateTime IConvertible.ToDateTime(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToDateTime(provider);
        decimal IConvertible.ToDecimal(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToDecimal(provider);
        double IConvertible.ToDouble(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToDouble(provider);
        short IConvertible.ToInt16(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToInt16(provider);
        int IConvertible.ToInt32(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToInt32(provider);
        long IConvertible.ToInt64(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToInt64(provider);
        sbyte IConvertible.ToSByte(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToSByte(provider);
        float IConvertible.ToSingle(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToSingle(provider);
        /// <summary>
        /// 継承元クラスの場合、求められた型に変換して返す必要がある。
        /// </summary>
        /// <param name="conversionType"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            if (this.GetType().IsAssignableFrom(conversionType))
            {
                return conversionType.CreateValueObjectInstance(this.AsPrimitiveCore());
            }
            return ((IConvertible)this.AsPrimitiveCore()).ToType(conversionType, provider);
        }
        ushort IConvertible.ToUInt16(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToUInt16(provider);
        uint IConvertible.ToUInt32(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToUInt32(provider);
        ulong IConvertible.ToUInt64(IFormatProvider provider) => ((IConvertible)this.AsPrimitiveCore()).ToUInt64(provider);
        #endregion
    }
}
