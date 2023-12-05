using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.MutatingWebhookConfiguration;

public partial class MutatingWebhookConfigurationIndex : TableBase<V1MutatingWebhookConfiguration>
{
    [Inject]
    private IMutatingWebhookConfigurationService MutatingWebhookConfigurationService { get; set; }


    private async Task OnResourceChanged(ResourceCache<V1MutatingWebhookConfiguration> data)
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


    private async Task OnItemNameClick(V1MutatingWebhookConfiguration item)
    {
        await PageDrawerHelper<V1MutatingWebhookConfiguration>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<MutatingWebhookConfigurationDetailView, V1MutatingWebhookConfiguration, bool>(item);

    }
}
