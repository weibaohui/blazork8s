using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;

public class CustomResourceDefinitionService(IKubeService kubeService)
    : CommonAction<V1CustomResourceDefinition>, ICustomResourceDefinitionService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteCustomResourceDefinitionAsync(name);
    }
}
