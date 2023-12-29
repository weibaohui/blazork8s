using System.Collections.Generic;
using BlazorApp.Utils;
using Extension;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class PropertyMapView<T>:ComponentBase
{

    [Parameter]
    public IDictionary<string,T> Items { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public string Key { get; set; }

    [Parameter]
    public string ExplainField { get; set; }

    private bool IsBasicType()
    {
        return typeof(T).IsNullableType() ? typeof(T).GetUnderlyingType().IsBasicType() : typeof(T).IsBasicType();
    }

    private string GetValue(T item)
    {
        if (Key==null)
        {
            return item.ToString();
        }

        return ReflectHelper<T>.GetValue(item, Key);
    }

    private string GetSumValue(T item)
    {
        return IsBasicType() ? item.ToString() : GetValue(item);
    }
}
