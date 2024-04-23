using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.Lease;

public partial class LeaseDetailView : DrawerPageBase<V1Lease>
{
    private V1Lease Lease { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Lease = base.Options;
        await base.OnInitializedAsync();
    }
}