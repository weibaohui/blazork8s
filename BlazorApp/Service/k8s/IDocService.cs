using System.Threading.Tasks;
using Entity;

namespace BlazorApp.Service.k8s;

public interface IDocService
{
     Task<KubeExplainEntity> GetExplainByField(string fieldPath);

}
