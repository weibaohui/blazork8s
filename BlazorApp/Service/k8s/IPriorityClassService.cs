using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IPriorityClassService : ICommonAction<V1PriorityClass>
{
    Task SetDefault(V1PriorityClass            item);
    Task ChangeGlobalDefaultTo(V1PriorityClass item, bool status);

}
