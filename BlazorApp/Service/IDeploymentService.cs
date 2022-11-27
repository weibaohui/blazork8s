using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service
{
    public interface IDeploymentService : INamespaceAction<V1Deployment>
    {
        Task<V1DeploymentList> List();
        Task<V1DeploymentList> ListByNamespace(string            ns);
        Task<V1Deployment>     FindByName(string               name);
    }
}
