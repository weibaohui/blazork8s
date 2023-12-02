using k8s.Models;

namespace BlazorApp.Service.k8s
{
    public interface IDaemonSetService : ICommonAction<V1DaemonSet>
    {
    }
}
