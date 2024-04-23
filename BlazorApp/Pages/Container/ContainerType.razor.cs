using BlazorApp.Pages.Common;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Container;

public partial class ContainerType : PageBase
{
    [Parameter] public string Type { get; set; }
}