using System;
using System.Reflection;

namespace BlazorApp.Utils;

public class TypeHelper
{
    public static T ChangeType<T>(object value)
    {
        return TypeHelper.ChangeType<T>(value, (IFormatProvider) null);
    }

    public static T ChangeType<T>(object value, IFormatProvider provider)
    {
        Type type = typeof (T);
        if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof (Nullable<>)))
        {
            if (value == null)
                return default (T);
            type = Nullable.GetUnderlyingType(type);
        }
        return provider == null ? (T) Convert.ChangeType(value, type) : (T) Convert.ChangeType(value, type, provider);
    }

    public static bool IsTypeNullable<T>() => TypeHelper.IsTypeNullable(typeof (T));

    public static bool IsTypeNullable(Type type) => Nullable.GetUnderlyingType(type) != (Type) null;

    public static Type GetNullableType<T>()
    {
        Type nullableType1 = typeof (T);
        Type type          = Nullable.GetUnderlyingType(nullableType1);
        if ((object) type == null)
            type = nullableType1;
        Type nullableType2 = type;
        if (!nullableType2.IsValueType)
            return nullableType2;
        return typeof (Nullable<>).MakeGenericType(nullableType2);
    }

    public static Type GetUnderlyingType<T>()
    {
        Type type = typeof (T);
        return !type.GetTypeInfo().IsGenericType || !(type.GetGenericTypeDefinition() == typeof (Nullable<>)) ? type : Nullable.GetUnderlyingType(type);
    }

}
