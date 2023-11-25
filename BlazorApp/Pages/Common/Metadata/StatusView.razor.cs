using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class StatusView : ComponentBase
{
    [Parameter]
    public string Status { get; set; }
}
