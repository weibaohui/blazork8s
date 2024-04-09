using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.${Item};

public partial class ${Item}Index : TableBase<${ItemType}>
{
    [Inject]
    private I${Item}Service ${Item}Service { get; set; }


    private async Task OnResourceChanged(ResourceCache<${ItemType}> data)
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


    private async Task OnItemNameClick(${ItemType} item)
    {
        await PageDrawerHelper<${ItemType}>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<${Item}DetailView, ${ItemType}, bool>(item);

    }
}
