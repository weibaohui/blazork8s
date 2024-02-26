using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ReplicationController;
public partial class ReplicationControllerAction : ComponentBase
{
    [Parameter]
    public V1ReplicationController Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IReplicationControllerService ReplicationControllerService { get; set; }

    private async Task OnDeleteClick(V1ReplicationController item)
    {
        await ReplicationControllerService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
