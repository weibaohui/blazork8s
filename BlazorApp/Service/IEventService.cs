using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service
{
    public interface IEventService
    {
        Task<Corev1EventList> List();
    }
}
