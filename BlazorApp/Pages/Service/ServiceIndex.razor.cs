using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Service;

public partial class ServiceIndex : TableBase<V1Service>
{
    [Inject]
    private IServiceService ServiceService { get; set; }


    private async Task OnResourceChanged(ResourceCache<V1Service> data)
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


    private async Task OnItemNameClick(V1Service item)
    {
        await PageDrawerHelper<V1Service>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<ServiceDetailView, V1Service, bool>(item);

    }
}
