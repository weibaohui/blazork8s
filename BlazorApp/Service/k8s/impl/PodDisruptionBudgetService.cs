using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class PodDisruptionBudgetService : CommonAction<V1PodDisruptionBudget>, IPodDisruptionBudgetService
{
    private readonly IKubeService                _kubeService;

    public PodDisruptionBudgetService(IKubeService kubeService)
    {
        _kubeService = kubeService;
    }
    public new async Task<object> Delete(string ns, string name)
    {
        return await _kubeService.Client().DeleteNamespacedPodDisruptionBudgetAsync(name, ns);
    }
}
