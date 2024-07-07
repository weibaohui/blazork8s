using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;

namespace BlazorApp.Pages.Gateway.Gateway;

public partial class GatewayDetailView : DrawerPageBase<V1Gateway>
{
    private V1Gateway Gateway { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Gateway = base.Options;
        await base.OnInitializedAsync();
    }
}