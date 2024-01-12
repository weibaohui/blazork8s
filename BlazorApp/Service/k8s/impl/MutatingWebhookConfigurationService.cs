using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class MutatingWebhookConfigurationService : CommonAction<V1MutatingWebhookConfiguration>, IMutatingWebhookConfigurationService
{
    private readonly IKubeService                _kubeService;

    public MutatingWebhookConfigurationService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteMutatingWebhookConfigurationAsync(name);
    }
}
