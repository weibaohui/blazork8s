using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;

namespace BlazorApp.Pages.Gateway.ReferenceGrant;

public partial class ReferenceGrantDetailView : DrawerPageBase<V1Alpha2ReferenceGrant>
{
    private V1Alpha2ReferenceGrant ReferenceGrant { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ReferenceGrant = base.Options;
        await base.OnInitializedAsync();
    }
}