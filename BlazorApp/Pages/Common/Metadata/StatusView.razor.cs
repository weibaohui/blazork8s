using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Common.Metadata;

public partial class StatusView : PageBase
{
    [Parameter]
    public string Status { get; set; }
}
