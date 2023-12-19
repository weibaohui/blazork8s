using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
namespace BlazorApp.Pages.ClusterRole;
public partial class ClusterRoleIndex : TableBase<V1ClusterRole>
{
    [Inject]
    private IClusterRoleService ClusterRoleService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1ClusterRole> data)
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
    private async Task OnItemNameClick(V1ClusterRole item)
    {
        await PageDrawerHelper<V1ClusterRole>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<ClusterRoleDetailView, V1ClusterRole, bool>(item);
    }
}