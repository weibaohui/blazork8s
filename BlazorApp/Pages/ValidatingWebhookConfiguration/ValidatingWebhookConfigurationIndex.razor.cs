using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ValidatingWebhookConfiguration;

public partial class ValidatingWebhookConfigurationIndex : TableBase<V1ValidatingWebhookConfiguration>
{
    [Inject]
    private IValidatingWebhookConfigurationService ValidatingWebhookConfigurationService { get; set; }


    private async Task OnResourceChanged(ResourceCache<V1ValidatingWebhookConfiguration> data)
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


    private async Task OnItemNameClick(V1ValidatingWebhookConfiguration item)
    {
        await PageDrawerHelper<V1ValidatingWebhookConfiguration>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<ValidatingWebhookConfigurationDetailView, V1ValidatingWebhookConfiguration, bool>(item);

    }
}
