using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.Core.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// この型が、値オブジェクトの型であるかどうかを取得します。
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        /// <remarks>
        /// <see cref="IValueObject"/>インターフェースを実装しているものを値型と判定します。
        /// </remarks>
        public static bool IsValueObject(this Type valueType)
        {
            return typeof(IValueObject).IsAssignableFrom(valueType);
        }

        /// <summary>
        /// <paramref name="primitiveValue"/>をプリミティブ型とした<paramref name="valueObjectType"/>型のインスタンスを生成します。
        /// </summary>
        /// <param name="valueObjectType"></param>
        /// <param name="primitiveValue"></param>
        /// <returns></returns>
        public static object CreateValueObjectInstance(this Type valueObjectType, object primitiveValue)
        {
            if (!IsValueObject(valueObjectType))
            {
                // プリミティブ型の場合はそのまま返す
                return Converter.ChangeType(primitiveValue, valueObjectType)!;
            }
            var typedValue = valueObjectType.ConvertToPrimitive(primitiveValue);
            var instance = Activator.CreateInstance(valueObjectType, typedValue);
            return instance!;
        }

        /// <summary>
        /// ValueObjectのプリミティブ型の値に変換します。
        /// </summary>
        /// <param name="valueObjectType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ConvertToPrimitive(this Type valueObjectType, object value)
        {
            var parameterType = valueObjectType.GetValueObjectPrimitiveType(value);

            try
            {
                if (parameterType == typeof(bool))
                {
                    var booleanConverter = new Utils.BooleanConverter();
                    var typedValue = booleanConverter.ConvertFrom(value);
                    return typedValue!;
                }
                else if (parameterType == typeof(Guid))
                {
                    return new Guid($"{value}");
                }
                else
                {
                    if (value.GetType().IsValueObject())
                    {
                        var typedValue = value.CastTo(parameterType);
                        return typedValue;
                    }
                    else
                    {
                        var typedValue = Converter.ChangeType(value, parameterType);
                        return typedValue!;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new ArgumentException($"ValueOjectのコンストラクタに渡す値 [{value}] を {parameterType.FullName} に変換することができませんでした。", nameof(value), exception);
            }
        }

        /// <summary>
        /// Nullableではない元の型を取得します。Nullable型でない場合はそのままの型を返します。
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Type UnwrapNullableType(this Type self) => Nullable.GetUnderlyingType(self) ?? self;

        /// <summary>
        /// ValueObjectが内包するプリミティブ型を取得します。
        /// </summary>
        /// <param name="valueObjectType"></param>
        /// <param name="value">コンストラクタを特定するための引数がある場合に指定</param>
        /// <returns></returns>
        public static Type GetValueObjectPrimitiveType(this Type valueObjectType, object value = null)
        {
            if (!valueObjectType.IsValueObject())
            {
                return valueObjectType;
            }

            var ctors = valueObjectType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (ctors.Length == 0)
            {
                throw new ArgumentException("ValueObjectにはプリミティブ型をひとつだけ設定可能なコンストラクタが、ひとつだけ存在する必要があります。", nameof(valueObjectType));
            }
            var ctor = ctors.FirstOrDefault(_ => _.GetParameters().Length == 1);
            if (value != null)
            {
                var targetCtor = ctors.Where(_ => _.GetParameters().Length == 1).FirstOrDefault(_ => _.GetParameters()[0].ParameterType == value.GetType());
                if (targetCtor != null)
                {
                    ctor = targetCtor;
                }
            }
            if (ctor == null)
            {
                throw new ArgumentException("ValueObjectにはプリミティブ型をひとつだけ設定可能なコンストラクタが、ひとつだけ存在する必要があります。", nameof(valueObjectType));
            }
            var parameterType = ctor.GetParameters()[0].ParameterType;

            return parameterType;
        }
    }
}
