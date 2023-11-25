using k8s.Models;

namespace BlazorApp.Service
{
    public interface IEventService : ICommonAction<Corev1Event>
    {
    }
}