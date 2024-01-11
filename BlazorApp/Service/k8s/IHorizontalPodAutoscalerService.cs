using System.Threading.Tasks;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IHorizontalPodAutoscalerService : ICommonAction<V1HorizontalPodAutoscaler>
{
      Task<object> V1Delete(string ns, string name);

}
