using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
namespace BlazorApp.Pages.NetworkPolicy;
public partial class NetworkPolicyIndex : TableBase<V1NetworkPolicy>
{
    [Inject]
    private INetworkPolicyService NetworkPolicyService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1NetworkPolicy> data)
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
    private async Task OnItemNameClick(V1NetworkPolicy item)
    {
        await PageDrawerHelper<V1NetworkPolicy>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<NetworkPolicyDetailView, V1NetworkPolicy, bool>(item);
    }
}