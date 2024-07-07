using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;

namespace BlazorApp.Pages.Gateway.BackendLBPolicy;

public partial class BackendLBPolicyDetailView : DrawerPageBase<V1Alpha2BackendLBPolicy>
{
    private V1Alpha2BackendLBPolicy BackendLBPolicy { get; set; }

    protected override async Task OnInitializedAsync()
    {
        BackendLBPolicy = base.Options;
        await base.OnInitializedAsync();
    }
}