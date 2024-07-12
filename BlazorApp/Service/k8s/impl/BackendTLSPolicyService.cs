using System.Threading.Tasks;
using Entity.Crd.Gateway;
using k8s;

namespace BlazorApp.Service.k8s.impl;

public class BackendTLSPolicyService(IKubeService kubeService)
    : CommonAction<V1Alpha3BackendTLSPolicy>, IBackendTLSPolicyService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().CustomObjects
            .DeleteNamespacedCustomObjectAsync("gateway.networking.k8s.io", "v1alpha3", ns, "backendtlspolicies", name);
    }
}
