using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FilmHouse.Data.Core.ValueObjects
{
    /// <summary>
    /// <see cref="DateTime"/>を内包する値オブジェクトの、デフォルト実装を含んだインターフェースです。
    /// </summary>
    /// <remarks>
    /// <see cref="DateTime"/>が保有するプロパティやメソッドはこのインターフェイスでデフォルト実装することで提供します。
    /// </remarks>
    public abstract class TimeOnlyValueObjectBase : IFormattable
    {
        /// <summary>
        /// <see cref="IValue{TValue}.AsPrimitive()"/>メソッドを内部的に呼び出すメソッド。
        /// <see cref="TimeOnlyValueObjectBase"/>クラス内の処理でプリミティブ型を使用する場合に呼び出される。
        /// </summary>
        /// <returns>プリミティブ型</returns>
        protected virtual TimeOnly AsPrimitiveCore() => ((IValue<TimeOnly>)this).AsPrimitive();

        /// <summary>
        /// このインスタンスで表される日付の時間の部分を取得します。
        /// </summary>
        public int Hour { get => this.AsPrimitiveCore().Hour; }

        /// <summary>
        /// このインスタンスで表される日付のミリ秒の部分を取得します。
        /// </summary>
        public int Millisecond { get => this.AsPrimitiveCore().Millisecond; }

        /// <summary>
        /// このインスタンスで表される日付の分の部分を取得します。
        /// </summary>
        public int Minute { get => this.AsPrimitiveCore().Minute; }

        /// <summary>
        /// このインスタンスで表される日付の秒の部分を取得します。
        /// </summary>
        public int Second { get => this.AsPrimitiveCore().Second; }

        /// <summary>
        /// このインスタンスの日付と時刻を表すタイマー刻み数を取得します。
        /// </summary>
        public long Ticks { get => this.AsPrimitiveCore().Ticks; }

        /// <summary>
        /// このインスタンスの値に、指定された時間数を加算した新しい <see cref="TimeOnly"/> を返します。
        /// </summary>
        /// <param name="value">整数部と小数部から成る時間数。正または負のどちらの場合もあります。</param>
        /// <returns>このインスタンスで表された日付と時刻に <paramref name="value"/> で表された時間数を加算した値を保持するオブジェクト。</returns>
        public TimeOnly AddHours(double value) => this.AsPrimitiveCore().AddHours(value);

        /// <summary>
        /// このインスタンスの値に、指定された分数を加算した新しい <see cref="TimeOnly"/> を返します。
        /// </summary>
        /// <param name="value">整数部と小数部から成る分数。正または負のどちらの場合もあります。</param>
        /// <returns>このインスタンスで表された日付と時刻に <paramref name="value"/> で表された分数を加算した値を保持するオブジェクト。</returns>
        public TimeOnly AddMinutes(double value) => this.AsPrimitiveCore().AddMinutes(value);

        /// <summary>
        /// このインスタンスを<see cref="TimeSpan"/>に変換した値を返します。
        /// </summary>
        /// <returns></returns>
        public TimeSpan ToTimeSpan() => this.AsPrimitiveCore().ToTimeSpan();

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
