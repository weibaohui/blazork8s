using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<Corev1Event>> GetInvolvingObject(string ns, string name)
        {
            var list = await _kubeService.Client().CoreV1
                .ListNamespacedEventAsync(ns, fieldSelector: $"involvedObject.name={name}");
            return list.Items;
        }

        public Corev1Event GetLatestEvent(IList<Corev1Event> list)
        {
            return list.MaxBy(x => x.LastTimestamp);
        }

        public async Task<Corev1Event> GetInvolvingObjectLatestEvent(string ns, string name)
        {
            var list = await GetInvolvingObject(ns, name);
            if (list == null || list.Count == 0)
            {
                return null;
            }

            return GetLatestEvent(list);
        }
    }
}
