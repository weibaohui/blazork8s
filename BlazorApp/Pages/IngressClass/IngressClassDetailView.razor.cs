using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.IngressClass;

public partial class IngressClassDetailView : DrawerPageBase<V1IngressClass>
{
    private V1IngressClass IngressClass { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IngressClass = base.Options;
        await base.OnInitializedAsync();
    }
}