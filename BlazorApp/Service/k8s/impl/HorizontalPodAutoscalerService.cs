using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class HorizontalPodAutoscalerService : CommonAction<V1HorizontalPodAutoscaler>, IHorizontalPodAutoscalerService
{
    private readonly IBaseService                _baseService;

    public HorizontalPodAutoscalerService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
