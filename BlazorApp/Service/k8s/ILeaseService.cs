using k8s.Models;

namespace BlazorApp.Service.k8s;
public interface ILeaseService : ICommonAction<V1Lease>
{
}