using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ValidatingWebhookConfigurationService : CommonAction<V1ValidatingWebhookConfiguration>,
    IValidatingWebhookConfigurationService
{
    private readonly IKubeService _kubeService;

    public ValidatingWebhookConfigurationService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }

    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteValidatingWebhookConfigurationAsync(name);
    }
}
