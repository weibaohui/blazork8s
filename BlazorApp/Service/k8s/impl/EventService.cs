using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class EventService : CommonAction<Corev1Event>, IEventService
    {
        private readonly IKubeService _kubeService;

        public EventService(IKubeService kubeService)
        {
            _kubeService = kubeService;
        }
        public new async Task<object> Delete(string ns, string name)
        {
            return await _kubeService.Client().CoreV1.DeleteNamespacedEventAsync(name, ns);
        }
    }
}
