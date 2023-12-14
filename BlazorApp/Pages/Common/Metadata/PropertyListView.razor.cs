using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class PropertyListView<T> : ComponentBase
{
    [Parameter]
    public IList<T> Items { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public string Key { get; set; }

    [Parameter]
    public string ExplainField { get; set; }

    private string GetValue(T item)
    {
        Type          type     = typeof(T);
        var           property = type.GetProperty(Key);
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
}
