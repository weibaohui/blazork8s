using System.Threading.Tasks;
using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s.impl;

public class BackendTLSPolicyService(IKubeService kubeService)
    : CommonAction<V1Alpha3BackendTLSPolicy>, IBackendTLSPolicyService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return Task.CompletedTask;

        // return await kubeService.Client().DeleteNamespacedBackendTLSPolicyAsync(name, ns);
    }
}