using k8s.Models;

namespace BlazorApp.Service
{
    public interface IDeploymentService : ICommonAction<V1Deployment>
    {
    }
}
