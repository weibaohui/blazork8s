using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface ISecretService : ICommonAction<V1Secret>
{
}
