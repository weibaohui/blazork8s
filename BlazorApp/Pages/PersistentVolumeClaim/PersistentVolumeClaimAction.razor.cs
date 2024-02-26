using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.PersistentVolumeClaim;
public partial class PersistentVolumeClaimAction : ComponentBase
{
    [Parameter]
    public V1PersistentVolumeClaim Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IPersistentVolumeClaimService PersistentVolumeClaimService { get; set; }

    private async Task OnDeleteClick(V1PersistentVolumeClaim item)
    {
        await PersistentVolumeClaimService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
