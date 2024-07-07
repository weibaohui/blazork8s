using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Entity.Crd.Gateway;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Gateway.BackendTLSPolicy;

public partial class BackendTLSPolicyIndex : TableBase<V1Alpha3BackendTLSPolicy>
{
    [Inject] private IBackendTLSPolicyService BackendTLSPolicyService { get; set; }

    private async Task OnResourceChanged(ResourceCache<V1Alpha3BackendTLSPolicy> data)
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

    private async Task OnItemNameClick(V1Alpha3BackendTLSPolicy item)
    {
        await PageDrawerHelper<V1Alpha3BackendTLSPolicy>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<BackendTLSPolicyDetailView, V1Alpha3BackendTLSPolicy, bool>(item);
    }
}