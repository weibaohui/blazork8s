using System.Threading.Tasks;
using BlazorApp.Utils;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class PropertySimpleGenericsView<T> : PageBase
{
    [Parameter] public T Item { get; set; }

    [Parameter] public string Title { get; set; }

    [Parameter] public string ExplainField { get; set; }

    [Parameter] public string Key { get; set; }

    [Parameter] public bool IsString { get; set; }

    [Parameter] public EventCallback OnClick { get; set; }

    private string GetValue(T item)
    {
        return ReflectHelper<T>.GetValue(item, Key);
    }

    private Task OnTagClick()
    {
        return OnClick.InvokeAsync();
    }
}