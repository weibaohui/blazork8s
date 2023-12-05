using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IEndpointSliceService : ICommonAction<V1EndpointSlice>
{
}
