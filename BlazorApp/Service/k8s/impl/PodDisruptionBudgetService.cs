using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class PodDisruptionBudgetService : CommonAction<V1PodDisruptionBudget>, IPodDisruptionBudgetService
{
    private readonly IBaseService                _baseService;

    public PodDisruptionBudgetService(IBaseService baseService)
    {
        _baseService = baseService;
    }
}
