using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;

namespace BlazorApp.Pages.Gateway.GrpcRoute;

public partial class GrpcRouteDetailView : DrawerPageBase<V1GRPCRoute>
{
    private V1GRPCRoute GrpcRoute { get; set; }

    protected override async Task OnInitializedAsync()
    {
        GrpcRoute = base.Options;
        await base.OnInitializedAsync();
    }
}