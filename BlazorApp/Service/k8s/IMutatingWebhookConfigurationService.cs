using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IMutatingWebhookConfigurationService : ICommonAction<V1MutatingWebhookConfiguration>
{
}
