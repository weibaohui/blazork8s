using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using Entity.Crd.Gateway;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.TcpRoute;

public partial class TcpRouteDetailView : DrawerPageBase<V1Alpha2TCPRoute>
{
    private V1Alpha2TCPRoute TcpRoute { get; set; }


    [Inject] private ITcpRouteService TcpRouteService { get; set; }

    private IList<V1Service> _svcList = new List<V1Service>();

    protected override async Task OnInitializedAsync()
    {
        TcpRoute = base.Options;
        _svcList = TcpRouteService.GetBackendServices(TcpRoute);
        await base.OnInitializedAsync();
    }
}
