using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.ServiceAccount;
public partial class ServiceAccountIndex : TableBase<V1ServiceAccount>
{
    [Inject]
    private IServiceAccountService ServiceAccountService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1ServiceAccount> data)
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
    private async Task OnItemNameClick(V1ServiceAccount item)
    {
        await PageDrawerHelper<V1ServiceAccount>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<ServiceAccountDetailView, V1ServiceAccount, bool>(item);
    }
}
