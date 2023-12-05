using System.Threading.Tasks;
using AntDesign;
using k8s.Models;

namespace BlazorApp.Pages.ValidatingWebhookConfiguration
{
    public partial class ValidatingWebhookConfigurationDetailView : FeedbackComponent<V1ValidatingWebhookConfiguration, bool>
    {
        private V1ValidatingWebhookConfiguration Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
