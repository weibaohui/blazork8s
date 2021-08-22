using System.Threading.Tasks;
using k8s;

namespace Blazor.Service
{
    public interface IK8s
    {
        public IKubernetes  k8s();
        public Task<string> GetResourceJson(string     url);
        Task<T>             GetFromJsonAsync<T>(string url);
    }
}