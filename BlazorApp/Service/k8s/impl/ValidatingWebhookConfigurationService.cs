using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class ValidatingWebhookConfigurationService : CommonAction<V1ValidatingWebhookConfiguration>, IValidatingWebhookConfigurationService
{
    private readonly IBaseService                _baseService;

    public ValidatingWebhookConfigurationService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
