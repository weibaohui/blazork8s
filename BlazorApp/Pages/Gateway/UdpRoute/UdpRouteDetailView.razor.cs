using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using Entity.Crd.Gateway;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.UdpRoute;

public partial class UdpRouteDetailView : DrawerPageBase<V1Alpha2UDPRoute>
{
    private V1Alpha2UDPRoute UdpRoute { get; set; }


    [Inject] private IUdpRouteService UdpRouteService { get; set; }

    private IList<V1Service> _svcList = new List<V1Service>();

    protected override async Task OnInitializedAsync()
    {
        UdpRoute = base.Options;
        _svcList = UdpRouteService.GetBackendServices(UdpRoute);
        await base.OnInitializedAsync();
    }
}
