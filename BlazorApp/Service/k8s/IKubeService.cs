using System.Threading.Tasks;
using Entity;
using k8s;

namespace BlazorApp.Service.k8s;

public interface IKubeService
{
    public Kubernetes Client();
    public string     CurrentContext();

    public Task<ServerInfo> GetServerVersion();
    public Task<string> GetReadyz();
    public Task<string> GetLivez();

}
