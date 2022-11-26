using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.impl
{
    public class EventService : IEventService
    {
        private readonly IBaseService _baseService;

        public EventService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<Corev1EventList> List()
        {
            return await _baseService.Client().CoreV1.ListEventForAllNamespacesAsync();
        }
    }
}
