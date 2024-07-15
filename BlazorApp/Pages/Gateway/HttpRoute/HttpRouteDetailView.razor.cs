using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using Entity.Crd.Gateway;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.HttpRoute;

public partial class HttpRouteDetailView : DrawerPageBase<V1HTTPRoute>
{
    private V1HTTPRoute HttpRoute { get; set; }

    [Inject] private IHttpRouteService HttpRouteService { get; set; }

    private IList<V1Service> _svcList = new List<V1Service>();

    protected override async Task OnInitializedAsync()
    {
        HttpRoute = base.Options;
        _svcList = HttpRouteService.GetBackendServices(HttpRoute);
        await base.OnInitializedAsync();
    }
}
