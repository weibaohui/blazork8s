using System;
using System.Reflection.Emit;

namespace BlazorApp.Utils;

public  class ReflectHelper<T>
{


    /// <summary>
    /// 获取T的某个属性的值
    /// </summary>
    /// <param name="item"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetValue(T item ,string key)
    {
        Type          type     = typeof(T);
        var           property = type.GetProperty(key);
        DynamicMethod method   = new DynamicMethod("GetPropertyValue", typeof(object), new Type[] { type }, true);
        ILGenerator   il       = method.GetILGenerator();
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Callvirt, property.GetGetMethod());

        if (property.PropertyType.IsValueType)
        {
            il.Emit(OpCodes.Box, property.PropertyType); //值类型需要装箱，因为返回类型是object
        }

        il.Emit(OpCodes.Ret);
        Func<T, object> fun   = method.CreateDelegate(typeof(Func<T, object>)) as Func<T, object>;
        object          value = fun.Invoke(item);
        return value.ToString();
    }


    private void ListProperty(T item)
    {
        Type type     = typeof(T);
        var  propertyList = type.GetProperties();
        foreach (var p in propertyList)
        {
            var method = p.GetGetMethod();
           Console.WriteLine( method.Name.ToString());
           Console.WriteLine( method.ToString());
           Console.WriteLine( method.GetType().ToString());
        }
    }
}
