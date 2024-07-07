using AntDesign;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.TlsRoute;

public partial class TlsRouteAction : PageBase
{
    [Parameter] public V1Alpha2TLSRoute Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;
}