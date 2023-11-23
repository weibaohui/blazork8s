using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Chat;

public interface IChatHub<in T> where T : IKubernetesObject<V1ObjectMeta>
{
    public Task SendMessage(string    message);
    public Task SendWatchEvent(string message);
}