using System.Threading.Tasks;
using k8s.Models;

namespace Blazor.Service
{
    public interface IDeploymentService : INamespaceAction<V1Deployment>
    {
        Task<V1DeploymentList> List();
        Task<V1DeploymentList> ListByNamespace(string            ns);
        Task                   ShowDeploymentDrawer(V1Deployment deploy);
        Task                   ShowDeploymentDrawer(string       deployName);
        Task<V1Deployment>     FilterByName(string               name);

    }
}
