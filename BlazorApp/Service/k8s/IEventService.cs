using System.Collections.Generic;
using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service.k8s
{
    public interface IEventService : ICommonAction<Corev1Event>
    {
        public Task<IList<Corev1Event>> GetInvolvingObject(string            ns, string name);
        public Corev1Event              GetLatestEvent(IList<Corev1Event>    list);
        public Task<Corev1Event>        GetInvolvingObjectLatestEvent(string ns, string name);

    }
}
