using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;

namespace BlazorApp.Pages.Gateway.BackendTLSPolicy;

public partial class BackendTLSPolicyDetailView : DrawerPageBase<V1Alpha3BackendTLSPolicy>
{
    private V1Alpha3BackendTLSPolicy BackendTLSPolicy { get; set; }

    protected override async Task OnInitializedAsync()
    {
        BackendTLSPolicy = base.Options;
        await base.OnInitializedAsync();
    }
}