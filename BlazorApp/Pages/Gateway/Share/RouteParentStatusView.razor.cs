using System.Collections.Generic;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.Share;

public partial class RouteParentStatusView : PageBase
{
    [Parameter] public IList<RouteParentStatus> Parents { get; set; }
}
