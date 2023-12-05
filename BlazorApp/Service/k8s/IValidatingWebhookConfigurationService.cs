using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IValidatingWebhookConfigurationService : ICommonAction<V1ValidatingWebhookConfiguration>
{
}
