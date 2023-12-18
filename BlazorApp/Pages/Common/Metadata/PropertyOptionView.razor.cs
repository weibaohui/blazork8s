using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class PropertyOptionView: ComponentBase
{
    [Parameter]
    public object Item { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public string ExplainField { get; set; }

    [Parameter]
    public string[] Options { get; set; }


}
