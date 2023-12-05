using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IPodDisruptionBudgetService : ICommonAction<V1PodDisruptionBudget>
{
}
