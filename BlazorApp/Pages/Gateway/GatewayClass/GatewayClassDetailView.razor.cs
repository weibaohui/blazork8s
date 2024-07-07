using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;

namespace BlazorApp.Pages.Gateway.GatewayClass;

public partial class GatewayClassDetailView : DrawerPageBase<V1GatewayClass>
{
    private V1GatewayClass GatewayClass { get; set; }

    protected override async Task OnInitializedAsync()
    {
        GatewayClass = base.Options;
        await base.OnInitializedAsync();
    }
}