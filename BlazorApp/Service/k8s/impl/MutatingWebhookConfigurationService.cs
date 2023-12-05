using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class MutatingWebhookConfigurationService : CommonAction<V1MutatingWebhookConfiguration>, IMutatingWebhookConfigurationService
{
    private readonly IBaseService                _baseService;

    public MutatingWebhookConfigurationService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
