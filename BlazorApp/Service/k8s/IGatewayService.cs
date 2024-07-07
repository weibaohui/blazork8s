using Entity.Crd.Gateway;

namespace BlazorApp.Service.k8s;

public interface IGatewayService : ICommonAction<V1Gateway>
{
}