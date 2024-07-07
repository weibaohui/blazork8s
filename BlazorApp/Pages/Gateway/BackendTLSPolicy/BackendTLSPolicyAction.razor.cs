using AntDesign;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.BackendTLSPolicy;

public partial class BackendTLSPolicyAction : PageBase
{
    [Parameter] public V1Alpha3BackendTLSPolicy Item { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;
}