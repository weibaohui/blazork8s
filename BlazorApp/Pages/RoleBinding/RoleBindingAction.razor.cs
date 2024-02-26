using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.RoleBinding;
public partial class RoleBindingAction : ComponentBase
{
    [Parameter]
    public V1RoleBinding Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IRoleBindingService RoleBindingService { get; set; }


    private async Task OnDeleteClick(V1RoleBinding item)
    {
        await RoleBindingService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
