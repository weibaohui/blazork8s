using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s;

public interface IBackendTLSPolicyService : ICommonAction<V1Alpha3BackendTLSPolicy>
{
}