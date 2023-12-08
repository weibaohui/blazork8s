using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.ValidatingWebhookConfiguration;

public partial class ValidatingWebhookConfigurationAction : ComponentBase
{
    [Parameter]
    public V1ValidatingWebhookConfiguration Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

    [Inject]
    private IValidatingWebhookConfigurationService ValidatingWebhookConfigurationService { get; set; }


    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    [Inject]
    private ILogger<ValidatingWebhookConfigurationAction> Logger { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task OnValidatingWebhookConfigurationDeleteClick(V1ValidatingWebhookConfiguration item)
    {
        await ValidatingWebhookConfigurationService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

 

    private async Task OnYamlClick(V1ValidatingWebhookConfiguration item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1ValidatingWebhookConfiguration>, V1ValidatingWebhookConfiguration, bool>(options, item);
    }

    private async Task OnDocClick(V1ValidatingWebhookConfiguration item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<V1ValidatingWebhookConfiguration>, V1ValidatingWebhookConfiguration, bool>(options, item);
    }
}
