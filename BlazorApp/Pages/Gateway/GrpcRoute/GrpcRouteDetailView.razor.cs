using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using Entity.Crd.Gateway;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.GrpcRoute;

public partial class GrpcRouteDetailView : DrawerPageBase<V1GRPCRoute>
{
    private V1GRPCRoute GrpcRoute { get; set; }


    [Inject] private IGrpcRouteService GrpcRouteService { get; set; }

    private IList<V1Service> _svcList = new List<V1Service>();

    protected override async Task OnInitializedAsync()
    {
        GrpcRoute = base.Options;
        _svcList = GrpcRouteService.GetBackendServices(GrpcRoute);
        await base.OnInitializedAsync();
    }
}
