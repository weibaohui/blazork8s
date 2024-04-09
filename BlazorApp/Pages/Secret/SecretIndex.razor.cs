using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.Secret;
public partial class SecretIndex : TableBase<V1Secret>
{
    [Inject]
    private ISecretService SecretService { get; set; }
    private async Task OnResourceChanged(ResourceCache<V1Secret> data)
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
    private async Task OnItemNameClick(V1Secret item)
    {
        await PageDrawerHelper<V1Secret>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<SecretDetailView, V1Secret, bool>(item);
    }
}
