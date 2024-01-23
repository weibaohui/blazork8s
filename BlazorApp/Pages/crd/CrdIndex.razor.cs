using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.crd;

public partial class CrdIndex: TableBase<V1CustomResourceDefinition>
{
[Inject]
private ICustomResourceDefinitionService CustomResourceDefinitionService { get; set; }
private async Task OnResourceChanged(ResourceCache<V1CustomResourceDefinition> data)
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
private async Task OnItemNameClick(V1CustomResourceDefinition item)
{
    await PageDrawerHelper<V1CustomResourceDefinition>.Instance
        .SetDrawerService(PageDrawerService.DrawerService)
        .ShowDrawerAsync<CrdDetailView, V1CustomResourceDefinition, bool>(item);
}
}
