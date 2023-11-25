using System.Collections.Generic;
using k8s.Models;

namespace BlazorApp.Service
{
    public interface IDeploymentService : INamespaceAction<V1Deployment>
    {
        List<V1Deployment> List();
        List<V1Deployment> ListByNamespace(string ns);
        V1Deployment       FindByName(string      name);
    }
}