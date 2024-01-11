using System;
using System.Collections.Generic;
using BlazorApp.Utils;
using Extension;
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
        Console.WriteLine("item" + item);
        Console.WriteLine("key" + Key);
        return ReflectHelper<T>.GetValue(item, Key);
    }

    private bool IsBasicType()
    {
        return typeof(T).IsNullableType() ? typeof(T).GetUnderlyingType().IsBasicType() : typeof(T).IsBasicType();
    }


}
