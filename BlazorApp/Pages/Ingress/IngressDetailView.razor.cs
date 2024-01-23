using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.Ingress;

public partial class IngressDetailView :  DrawerPageBase<V1Ingress>
{
    private V1Ingress Ingress { get; set; }
    protected override async Task OnInitializedAsync()
    {
        Ingress = base.Options;
        await base.OnInitializedAsync();
    }
}
