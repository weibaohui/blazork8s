using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IDaemonSetService : ICommonAction<V1DaemonSet>
{
    Task Restart(V1DaemonSet item);

}
