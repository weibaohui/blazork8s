using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ClusterRoleBinding;
public partial class ClusterRoleBindingAction : ComponentBase
{
    [Parameter]
    public V1ClusterRoleBinding Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IClusterRoleBindingService ClusterRoleBindingService { get; set; }

    private async Task OnDeleteClick(V1ClusterRoleBinding item)
    {
        await ClusterRoleBindingService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
