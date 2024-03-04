using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Analyze;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IHorizontalPodAutoscalerService : ICommonAction<V2HorizontalPodAutoscaler>
{
      Task<object>       V1Delete(string ns, string name);
      Task<List<Result>> Analyze();

}
