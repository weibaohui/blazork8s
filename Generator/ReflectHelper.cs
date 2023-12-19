using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Extension;

namespace Generator;

public class ReflectHelper<T>
{
    /// <summary>
    /// 获取T的某个属性的值
    /// </summary>
    /// <param name="item"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetValue(T item, string key)
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


    public static void ListProperty(Type type, string parentName, IList<KubeType> container)
    {
        var properties = type.GetProperties();
        foreach (var property in properties)
        {
            var method = property.GetGetMethod();

            KubeType kt = new KubeType();
            kt.Type = method?.ReturnType.ToString()
                    .Replace("System.Collections.Generic.", "")
                    .Replace("[", "<")
                    .Replace("]", ">")
                    .Replace("`1", "")
                    .Replace("`2", "")
                ;
            kt.CoreType = method?.ReturnType.ToString()?.Replace("System.Collections.Generic.IList", "")
                .Replace("`1", "")
                .Replace("[", "")
                .Replace("]", "");
            kt.Name         = method?.Name.Replace("get_", "");
            kt.FullName     = parentName + "." + kt.Name;
            kt.ExplainFiled = kt.FullName.ToCamelCase(true);
            kt.FieldLevel   = kt.ExplainFiled.CountBy(".") + 1;
            kt.IsList       = IsList(kt.Type);
            kt.IsStatus     = kt.FullName.Contains(".Status");
            kt.ShowInJson   = ShowInJson(kt.Type);
            if (kt.Type != null && kt.Type.Contains("k8s"))
            {
                if (kt.IsList)
                {
                    ListItemsProperty(method?.ReturnType, kt.FullName, kt.Child);
                }
                else
                {
                    ListProperty(method?.ReturnType, kt.FullName, kt.Child);
                }
            }


            container.Add(kt);
        }
    }

    private static bool ShowInJson(string type)
    {
        return type.Contains("k8s") || type.Contains("IDictionary") || type.Contains("IList");
    }

    private static void ListItemsProperty(Type type, string parentName, IList<KubeType> container)
    {
        var properties = type.GetProperties();

        if (properties.Length == 1 && properties[0].Name == "Item")
        {
            var transType = properties[0].GetGetMethod()?.ReturnType;
            ListProperty(transType, parentName, container);
        }
    }

    private static bool IsList(string type)
    {
        return type.StartsWith("IList");
    }


    /// <summary>
    /// 处理child只有一个属性的情况，抽取到上层实例中。
    /// </summary>
    /// <param name="list"></param>
    public static void ProcessOnlyOneChildItem(IList<KubeType> list)
    {
        foreach (var type in list)
        {
            if (type.OnlyOneChildItemName != null)
            {
                continue;
            }

            if (type.IsList && type.Child != null && type.Child.Count == 1 && type.Child[0].IsList == false)
            {
                type.OnlyOneChildItemName = type.Child[0].Name;
            }

            if (type.Child != null && type.Child.Count > 1)
            {
                ProcessOnlyOneChildItem(type.Child);
            }
        }
    }

    public static void ProcessMultipleChildItem(IList<KubeType> list)
    {
        foreach (var type in list)
        {
            if (type.OnlyOneChildItemName != null)
            {
                continue;
            }

            if (type.IsList && type.Child != null && type.Child.Count > 1)
            {
                type.MultipleChildItem = true;
                ProcessMultipleChildItem(type.Child);
            }
        }
    }
}
