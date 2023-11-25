using k8s.Models;

namespace BlazorApp.Service.k8s
{
    public interface IDeploymentService : ICommonAction<V1Deployment>
    {
    }
}