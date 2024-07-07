using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;

namespace BlazorApp.Pages.Gateway.TcpRoute;

public partial class TcpRouteDetailView : DrawerPageBase<V1Alpha2TCPRoute>
{
    private V1Alpha2TCPRoute TcpRoute { get; set; }

    protected override async Task OnInitializedAsync()
    {
        TcpRoute = base.Options;
        await base.OnInitializedAsync();
    }
}