using System.Threading.Tasks;
using k8s.Models;

namespace Blazor.Service
{
    public interface IPodService
    {
        Task<V1PodList> List();
        Task<int>       NodePodsNum();
    }
}