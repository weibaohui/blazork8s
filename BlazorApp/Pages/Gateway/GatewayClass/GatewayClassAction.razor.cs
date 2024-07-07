using AntDesign;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.GatewayClass;

public partial class GatewayClassAction : PageBase
{
    [Parameter] public V1GatewayClass Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;
}