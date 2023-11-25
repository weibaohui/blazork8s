using k8s.Models;

namespace BlazorApp.Service.k8s
{
    public interface IEventService : ICommonAction<Corev1Event>
    {
    }
}