using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;

namespace BlazorApp.Pages.Gateway.TlsRoute;

public partial class TlsRouteDetailView : DrawerPageBase<V1Alpha2TLSRoute>
{
    private V1Alpha2TLSRoute TlsRoute { get; set; }

    protected override async Task OnInitializedAsync()
    {
        TlsRoute = base.Options;
        await base.OnInitializedAsync();
    }
}