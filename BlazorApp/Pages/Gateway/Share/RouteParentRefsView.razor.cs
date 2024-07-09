using System.Collections.Generic;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.Share;

public partial class RouteParentRefsView : PageBase
{
    [Parameter] public IList<ParentReference> ParentRefs { get; set; }
    [Parameter] public string ExplainFieldPrefix { get; set; }
}
