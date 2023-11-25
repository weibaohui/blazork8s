using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.metadata;

public partial class StatusView : ComponentBase
{
    [Parameter]
    public string Status { get; set; }
}