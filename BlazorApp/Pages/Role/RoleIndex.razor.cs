using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.Role;
public partial class RoleIndex : TableBase<V1Role>
{
    [Inject]
    private IRoleService RoleService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1Role> data)
    {
        ItemList = data;
        TableData.CopyData(ItemList);
        await InvokeAsync(StateHasChanged);
    }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        TableData.CopyData(ItemList);
        await InvokeAsync(StateHasChanged);
    }
    private async Task OnItemNameClick(V1Role item)
    {
        await PageDrawerHelper<V1Role>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<RoleDetailView, V1Role, bool>(item);
    }
}
