using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
namespace BlazorApp.Pages.Endpoints;
public partial class EndpointsIndex : TableBase<V1Endpoints>
{
    [Inject]
    private IEndpointsService EndpointsService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1Endpoints> data)
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
    private async Task OnItemNameClick(V1Endpoints item)
    {
        await PageDrawerHelper<V1Endpoints>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<EndpointsDetailView, V1Endpoints, bool>(item);
    }
}