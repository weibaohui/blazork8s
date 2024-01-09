using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ClusterRoleBinding;
public partial class ClusterRoleBindingIndex : TableBase<V1ClusterRoleBinding>
{
    [Inject]
    private IClusterRoleService ClusterRoleService { get; set; }
    [Inject]
    private IClusterRoleBindingService ClusterRoleBindingService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1ClusterRoleBinding> data)
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
    private async Task OnItemNameClick(V1ClusterRoleBinding item)
    {
        await PageDrawerHelper<V1ClusterRoleBinding>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<ClusterRoleBindingDetailView, V1ClusterRoleBinding, bool>(item);
    }
}
