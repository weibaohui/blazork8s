using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class EventService : CommonAction<Corev1Event>, IEventService
    {
        private readonly IBaseService _baseService;

        public EventService(IBaseService baseService)
        {
            _baseService = baseService;
        }
    }
}