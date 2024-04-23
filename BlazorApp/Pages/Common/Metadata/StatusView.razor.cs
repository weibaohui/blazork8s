using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common.Metadata;

public partial class StatusView : PageBase
{
    [Parameter] public string Status { get; set; }
}