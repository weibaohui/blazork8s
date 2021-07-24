using System.Threading.Tasks;
using Entity;
using k8s.Models;

namespace ui.Service
{
    public interface IPodService
    {
        Task<V1PodList>    List();
        Task<int> NodePodsNum();
    }
}
