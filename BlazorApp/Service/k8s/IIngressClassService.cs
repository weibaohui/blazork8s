using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IIngressClassService : ICommonAction<V1IngressClass>
{
    Task SetDefault(V1IngressClass            item);
    Task ChangeGlobalDefaultTo(V1IngressClass item, bool status);
}
