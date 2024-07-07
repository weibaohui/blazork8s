using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s;

public interface IBackendLBPolicyService : ICommonAction<V1Alpha2BackendLBPolicy>
{
}