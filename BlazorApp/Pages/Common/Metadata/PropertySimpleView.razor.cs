#nullable enable
using System.Threading.Tasks;
using Extension;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class PropertySimpleView : PageBase
{
    [Parameter] public RenderFragment ChildContent { get; set; } = null!;

    [Parameter] public object? Item { get; set; } = new();

    [Parameter] public string Title { get; set; } = "";

    [Parameter] public string ExplainField { get; set; } = "";

    [Parameter] public bool ShowInJson { get; set; } = false;


    [Parameter] public EventCallback OnClick { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private Task OnTagClick()
    {
        return OnClick.InvokeAsync();
    }

    private string PrettyJson()
    {
        return Item?.ToPrettyJson().ToHtmlDisplay();
    }
}
