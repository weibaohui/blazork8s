using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Common.Metadata;

public partial class PropertyOptionView : PageBase
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
