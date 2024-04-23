using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.LimitRange;

public partial class LimitRangeDetailView : DrawerPageBase<V1LimitRange>
{
    private V1LimitRange LimitRange { get; set; }

    protected override async Task OnInitializedAsync()
    {
        LimitRange = base.Options;
        await base.OnInitializedAsync();
    }
}