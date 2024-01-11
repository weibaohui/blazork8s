using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.PriorityClass;
public partial class PriorityClassIndex : TableBase<V1PriorityClass>
{
    [Inject]
    IMessageService MessageService  { get; set; }

    [Inject]
    private IPriorityClassService PriorityClassService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1PriorityClass> data)
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
    private async Task OnItemNameClick(V1PriorityClass item)
    {
        await PageDrawerHelper<V1PriorityClass>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<PriorityClassDetailView, V1PriorityClass, bool>(item);
    }

    private async Task OnDefaultChange(V1PriorityClass context)
    {
       await  PriorityClassService.SetDefault(context);
       await MessageService.Success($"{context.Name()} is Global Default now ");
    }
}
