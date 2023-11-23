using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Data.Core.Utils
{
    /// <summary>
    /// 为了指定能在EFCore中使用的ValueCoverter的属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ValueConverterAttribute : Attribute
    {
        /// <summary>
        /// <see cref="ValueConverterAttribute"/>
        /// </summary>
        /// <param name="valueConverterType"></param>
        /// <param name="valueArrayConverterType"></param>
        public ValueConverterAttribute(Type valueConverterType, Type valueArrayConverterType)
        {
            this.ValueConverterType = valueConverterType;
            this.ValueArrayConverterType = valueArrayConverterType;
        }

        /// <summary>
        /// ValueConverter的类型。
        /// </summary>
        public Type ValueConverterType { get; }

        /// <summary>
        /// 获取用于排列的ValueConerter的类型。
        /// </summary>
        public Type ValueArrayConverterType { get; }
    }
}
