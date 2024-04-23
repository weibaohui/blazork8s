using AntDesign;
using BlazorApp.Pages.Common;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Endpoints;

public partial class EndpointsAction : PageBase
{
    [Parameter] public V1Endpoints Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;
}