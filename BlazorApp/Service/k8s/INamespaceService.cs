using k8s.Models;

namespace BlazorApp.Service.k8s
{
    public interface INamespaceService : ICommonAction<V1Namespace>
    {
    }
}