using AntDesign;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.BackendLBPolicy;

public partial class BackendLBPolicyAction : PageBase
{
    [Parameter] public V1Alpha2BackendLBPolicy Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;
}