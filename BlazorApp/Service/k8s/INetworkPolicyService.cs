using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface INetworkPolicyService : ICommonAction<V1NetworkPolicy>
{
}
