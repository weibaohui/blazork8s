using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace Entity
{
    public interface IChatClient<in T> where T : IKubernetesObject<V1ObjectMeta>
    {
        Task ResourceWatch(WatchEventType type, T item);
    }
}
