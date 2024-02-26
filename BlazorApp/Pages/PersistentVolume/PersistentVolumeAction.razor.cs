using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.PersistentVolume;
public partial class PersistentVolumeAction : ComponentBase
{
    [Parameter]
    public V1PersistentVolume Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IPersistentVolumeService PersistentVolumeService { get; set; }

    private async Task OnDeleteClick(V1PersistentVolume item)
    {
        await PersistentVolumeService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
