using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace Entity
{
    public interface IChatClient
    {
        Task ReceiveMessage(string   user, string message);
        Task PodWatch(WatchEventType type, V1Pod  item);
    }
}
