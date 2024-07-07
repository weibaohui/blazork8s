using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;

namespace BlazorApp.Pages.Gateway.HttpRoute;

public partial class HttpRouteDetailView : DrawerPageBase<V1HTTPRoute>
{
    private V1HTTPRoute HttpRoute { get; set; }

    protected override async Task OnInitializedAsync()
    {
        HttpRoute = base.Options;
        await base.OnInitializedAsync();
    }
}