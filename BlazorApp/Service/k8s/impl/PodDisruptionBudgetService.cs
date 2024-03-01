using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class PodDisruptionBudgetService(IKubeService kubeService)
    : CommonAction<V1PodDisruptionBudget>, IPodDisruptionBudgetService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedPodDisruptionBudgetAsync(name, ns);
    }
}
