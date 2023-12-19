using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
namespace BlazorApp.Pages.ReplicationController;
public partial class ReplicationControllerIndex : TableBase<V1ReplicationController>
{
    [Inject]
    private IReplicationControllerService ReplicationControllerService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1ReplicationController> data)
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
    private async Task OnItemNameClick(V1ReplicationController item)
    {
        await PageDrawerHelper<V1ReplicationController>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<ReplicationControllerDetailView, V1ReplicationController, bool>(item);
    }
}