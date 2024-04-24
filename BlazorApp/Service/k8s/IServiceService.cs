using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Analyze;
using k8s.Models;

namespace BlazorApp.Service.k8s;

public interface IServiceService : ICommonAction<V1Service>
{
    Task<List<Result>> Analyze();

    IList<V1Service> ListByLabels(IDictionary<string, string> labels);
}
