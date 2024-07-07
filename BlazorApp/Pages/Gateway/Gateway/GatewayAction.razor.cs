using AntDesign;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.Gateway;

public partial class GatewayAction : PageBase
{
    [Parameter] public V1Gateway Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;
}