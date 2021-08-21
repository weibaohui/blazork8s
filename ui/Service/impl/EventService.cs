using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace ui.Service.impl
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
            return await _baseService.GetFromJsonAsync<Corev1EventList>("/KubeApi/api/v1/events?limit=1000");
        }
    }
}
