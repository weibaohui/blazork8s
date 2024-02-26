using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ResourceQuota;
public partial class ResourceQuotaAction : ComponentBase
{
    [Parameter]
    public V1ResourceQuota Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IResourceQuotaService ResourceQuotaService { get; set; }

    private async Task OnDeleteClick(V1ResourceQuota item)
    {
        await ResourceQuotaService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
