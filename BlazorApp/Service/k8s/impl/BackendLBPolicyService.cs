using System.Threading.Tasks;
using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s.impl;

public class BackendLBPolicyService(IKubeService kubeService)
    : CommonAction<V1Alpha2BackendLBPolicy>, IBackendLBPolicyService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return Task.CompletedTask;
        // return await kubeService.Client().DeleteNamespacedBackendLBPolicyAsync(name, ns);
    }
}