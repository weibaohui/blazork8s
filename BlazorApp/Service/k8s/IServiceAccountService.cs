using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IServiceAccountService : ICommonAction<V1ServiceAccount>
{
}
