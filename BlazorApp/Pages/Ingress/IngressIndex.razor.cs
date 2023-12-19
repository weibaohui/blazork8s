using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
namespace BlazorApp.Pages.Ingress;
public partial class IngressIndex : TableBase<V1Ingress>
{
    [Inject]
    private IIngressService IngressService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1Ingress> data)
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
    private async Task OnItemNameClick(V1Ingress item)
    {
        await PageDrawerHelper<V1Ingress>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<IngressDetailView, V1Ingress, bool>(item);
    }
}