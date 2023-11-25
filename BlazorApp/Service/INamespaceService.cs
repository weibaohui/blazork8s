using k8s.Models;

namespace BlazorApp.Service
{
    public interface INamespaceService : ICommonAction<V1Namespace>
    {
    }
}