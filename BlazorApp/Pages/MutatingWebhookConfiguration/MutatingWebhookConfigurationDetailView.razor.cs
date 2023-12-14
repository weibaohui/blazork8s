using System.Threading.Tasks;
using k8s.Models;
using BlazorApp.Pages.Common;
namespace BlazorApp.Pages.MutatingWebhookConfiguration
{
    public partial class MutatingWebhookConfigurationDetailView :  DrawerPageBase<V1MutatingWebhookConfiguration>
    {
        private V1MutatingWebhookConfiguration Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
