using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service.k8s
{
    public interface IDeploymentService : ICommonAction<V1Deployment>
    {
        Task UpdateReplicas(V1Deployment item, int? replicas);
        Task Restart(V1Deployment        item);
    }
}
