using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ValidatingWebhookConfiguration;
public partial class ValidatingWebhookConfigurationAction : ComponentBase
{
    [Parameter]
    public V1ValidatingWebhookConfiguration Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;
    [Inject]
    private IValidatingWebhookConfigurationService ValidatingWebhookConfigurationService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
    private async Task OnDeleteClick(V1ValidatingWebhookConfiguration item)
    {
        await ValidatingWebhookConfigurationService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }

}
