using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl;
public class LeaseService(IKubeService kubeService) : CommonAction<V1Lease>, ILeaseService
{
    public new async Task<object> Delete(string ns, string name)
    {
        return await kubeService.Client().DeleteNamespacedLeaseAsync(name, ns);
    }
}