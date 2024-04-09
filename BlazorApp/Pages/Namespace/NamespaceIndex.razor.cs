using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Namespace;
public partial class NamespaceIndex : TableBase<V1Namespace>
{
    [Inject]
    private INamespaceService NamespaceService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1Namespace> data)
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
    private async Task OnItemNameClick(V1Namespace item)
    {
        await PageDrawerHelper<V1Namespace>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<NamespaceDetailView, V1Namespace, bool>(item);
    }
}
