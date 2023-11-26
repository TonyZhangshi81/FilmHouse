using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Core.Utils.Data
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 返回到指定类型的转换结果。
        /// </summary>
        /// <param name="self"></param>
        /// <param name="castToType"></param>
        public static object CastTo(this object self, Type castToType)
        {
            if (!self.GetType().IsValueObject())
            {
                return Converter.ChangeType(self, castToType)!;
            }

            var valueObject = (IValueObject)self;
            if (!castToType.IsValueObject())
            {
                return Converter.ChangeType(valueObject.AsPrimitiveObject(), castToType)!;
            }
            return castToType.CreateValueObjectInstance(valueObject.AsPrimitiveObject());
        }

        /// <summary>
        /// 返回到指定类型的转换结果。
        /// </summary>
        /// <param name="self"></param>
        public static T CastTo<T>(this object self)
        {
            return (T)CastTo(self, typeof(T));
        }
    }
}
