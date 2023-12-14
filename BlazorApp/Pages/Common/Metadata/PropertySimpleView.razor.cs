#nullable enable
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class PropertySimpleView : ComponentBase
{
    [Parameter]
    public object? Item { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public string ExplainField { get; set; }

    [Parameter]
    public bool ShowInJson { get; set; } = false;
}
