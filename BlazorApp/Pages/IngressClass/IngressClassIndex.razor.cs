using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.IngressClass;

public partial class IngressClassIndex : TableBase<V1IngressClass>
{
    [Inject]
    private IIngressClassService IngressClassService { get; set; }


    private async Task OnResourceChanged(ResourceCache<V1IngressClass> data)
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


    private async Task OnItemNameClick(V1IngressClass item)
    {
        await PageDrawerHelper<V1IngressClass>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<IngressClassDetailView, V1IngressClass, bool>(item);

    }
}
