using System.Threading.Tasks;
using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s.impl;

public class ReferenceGrantService(IKubeService kubeService)
    : CommonAction<V1Alpha2ReferenceGrant>, IReferenceGrantService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return Task.CompletedTask;

        // return await kubeService.Client().DeleteNamespacedReferenceGrantAsync(name, ns);
    }
}