using System.Threading.Tasks;
using k8s.Models;

namespace Blazor.Service
{
    public interface IEventService
    {
        Task<Corev1EventList> List();
    }
}