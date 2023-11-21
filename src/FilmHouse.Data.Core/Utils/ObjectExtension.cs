using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.Core.Utils
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Returns the result of conversion to the specified type.
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
        /// Returns the result of conversion to the specified type.
        /// </summary>
        /// <param name="self"></param>
        public static T CastTo<T>(this object self)
        {
            return (T)CastTo(self, typeof(T));
        }
    }
}
