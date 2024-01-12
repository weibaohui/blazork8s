using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class MutatingWebhookConfigurationService : CommonAction<V1MutatingWebhookConfiguration>, IMutatingWebhookConfigurationService
{
    private readonly IKubeService                _baseService;

    public MutatingWebhookConfigurationService(IKubeService baseService)
    {
        _baseService = baseService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _baseService.Client().DeleteMutatingWebhookConfigurationAsync(name);
    }
}
