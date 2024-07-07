using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;

namespace BlazorApp.Pages.Gateway.UdpRoute;

public partial class UdpRouteDetailView : DrawerPageBase<V1Alpha2UDPRoute>
{
    private V1Alpha2UDPRoute UdpRoute { get; set; }

    protected override async Task OnInitializedAsync()
    {
        UdpRoute = base.Options;
        await base.OnInitializedAsync();
    }
}