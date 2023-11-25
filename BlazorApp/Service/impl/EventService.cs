using k8s.Models;

namespace BlazorApp.Service.impl
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
