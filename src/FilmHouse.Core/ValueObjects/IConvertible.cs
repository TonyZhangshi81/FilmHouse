#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Core.Utils;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Core.ValueObjects
{
    /// <summary>
    /// 它是一个包含返回表达式的接口，用于比较继承类与继承类。
    /// </summary>
    public interface IConvertible<TPrimitive> : IConvertible
        where TPrimitive : IConvertible
    {
        /// <summary>
        /// 检索要包含的基本类型。
        /// </summary>
        /// <returns></returns>
        TPrimitive AsPrimitive();

        /// <summary>
        /// 检索已转换为字符串的实例的值。
        /// </summary>
        /// <returns></returns>
        string ToString();

        TypeCode IConvertible.GetTypeCode() => this.AsPrimitive().GetTypeCode();
        string IConvertible.ToString(IFormatProvider? provider) => this.AsPrimitive().ToString(provider);
        bool IConvertible.ToBoolean(IFormatProvider? provider) => this.AsPrimitive().ToBoolean(provider);
        byte IConvertible.ToByte(IFormatProvider? provider) => this.AsPrimitive().ToByte(provider);
        char IConvertible.ToChar(IFormatProvider? provider) => this.AsPrimitive().ToChar(provider);
        DateTime IConvertible.ToDateTime(IFormatProvider? provider) => this.AsPrimitive().ToDateTime(provider);
        decimal IConvertible.ToDecimal(IFormatProvider? provider) => this.AsPrimitive().ToDecimal(provider);
        double IConvertible.ToDouble(IFormatProvider? provider) => this.AsPrimitive().ToDouble(provider);
        short IConvertible.ToInt16(IFormatProvider? provider) => this.AsPrimitive().ToInt16(provider);
        int IConvertible.ToInt32(IFormatProvider? provider) => this.AsPrimitive().ToInt32(provider);
        long IConvertible.ToInt64(IFormatProvider? provider) => this.AsPrimitive().ToInt64(provider);
        sbyte IConvertible.ToSByte(IFormatProvider? provider) => this.AsPrimitive().ToSByte(provider);
        float IConvertible.ToSingle(IFormatProvider? provider) => this.AsPrimitive().ToSingle(provider);
        /// <summary>
        /// 在继承类的情况下，必须将其转换为所需的类型并返回。
        /// </summary>
        /// <param name="conversionType"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
        {
            if (this.GetType().IsAssignableFrom(conversionType))
            {
                return conversionType.CreateValueObjectInstance(this.AsPrimitive());
            }
            return this.AsPrimitive().ToType(conversionType, provider);
        }
        ushort IConvertible.ToUInt16(IFormatProvider? provider) => this.AsPrimitive().ToUInt16(provider);
        uint IConvertible.ToUInt32(IFormatProvider? provider) => this.AsPrimitive().ToUInt32(provider);
        ulong IConvertible.ToUInt64(IFormatProvider? provider) => this.AsPrimitive().ToUInt64(provider);
    }
}
