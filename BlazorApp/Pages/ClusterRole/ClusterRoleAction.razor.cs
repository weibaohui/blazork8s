using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ClusterRole;
public partial class ClusterRoleAction : ComponentBase
{
    [Parameter]
    public V1ClusterRole Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IClusterRoleService ClusterRoleService { get; set; }

    private async Task OnDeleteClick(V1ClusterRole item)
    {
        await ClusterRoleService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
