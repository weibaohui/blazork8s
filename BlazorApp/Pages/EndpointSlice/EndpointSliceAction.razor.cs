using AntDesign;
using BlazorApp.Pages.Common;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.EndpointSlice;

public partial class EndpointSliceAction : PageBase
{
    [Parameter] public V1EndpointSlice Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;
}