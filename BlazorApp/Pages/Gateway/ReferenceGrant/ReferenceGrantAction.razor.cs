using AntDesign;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.ReferenceGrant;

public partial class ReferenceGrantAction : PageBase
{
    [Parameter] public V1Alpha2ReferenceGrant Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;
}