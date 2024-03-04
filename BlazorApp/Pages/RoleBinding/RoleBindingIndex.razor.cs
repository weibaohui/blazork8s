using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Mapster;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.RoleBinding;
public partial class RoleBindingIndex : TableBase<V1RoleBinding>
{
    [Inject]
    private IRoleService RoleService { get; set; }
    [Inject]
    private IRoleBindingService RoleBindingService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1RoleBinding> data)
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
    private async Task OnItemNameClick(V1RoleBinding item)
    {
        await PageDrawerHelper<V1RoleBinding>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<RoleBindingDetailView, V1RoleBinding, bool>(item);
    }

    private V1ObjectReference GetRef(V1RoleBinding item)
    {
        var reference = item.RoleRef.Adapt<V1ObjectReference>();
        reference.NamespaceProperty = item.Namespace();
        return reference;
    }
}
