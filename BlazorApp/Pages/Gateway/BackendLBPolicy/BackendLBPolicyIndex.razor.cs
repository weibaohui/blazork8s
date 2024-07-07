using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.BackendLBPolicy;

public partial class BackendLBPolicyIndex : TableBase<V1Alpha2BackendLBPolicy>
{
    [Inject] private IBackendLBPolicyService BackendLBPolicyService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1Alpha2BackendLBPolicy> data)
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

    private async Task OnItemNameClick(V1Alpha2BackendLBPolicy item)
    {
        await PageDrawerHelper<V1Alpha2BackendLBPolicy>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<BackendLBPolicyDetailView, V1Alpha2BackendLBPolicy, bool>(item);
    }
}