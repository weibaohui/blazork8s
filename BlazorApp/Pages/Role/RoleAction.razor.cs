using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Role;
public partial class RoleAction : ComponentBase
{
    [Parameter]
    public V1Role Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IRoleService RoleService { get; set; }

    private async Task OnDeleteClick(V1Role item)
    {
        await RoleService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
