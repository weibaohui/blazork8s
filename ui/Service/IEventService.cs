using System;
using System.Threading.Tasks;
using k8s.Models;

namespace ui.Service
{
    public interface IEventService
    {
        Task<Corev1EventList> List();
    }
}
