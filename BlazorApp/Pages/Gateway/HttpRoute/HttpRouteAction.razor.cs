using AntDesign;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.HttpRoute;

public partial class HttpRouteAction : PageBase
{
    [Parameter] public V1HTTPRoute Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;
}