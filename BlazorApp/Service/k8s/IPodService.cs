using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.Analyze;
using k8s.Models;

namespace BlazorApp.Service.k8s
{
    public interface IPodService : ICommonAction<V1Pod>
    {
        IEnumerable<Tuple<string, int>> NodePodsNum();

        IList<V1Pod> ListByNodeName(string nodeName);

        Task<IList<V1Pod>> FilterPodByLabels(string ns, string labels);
        Task<IList<V1Pod>> FilterPodByLabels(string ns, IDictionary<string, string> labels);

        Task<IList<V1Pod>> FilterPodByLabelsForAllNamespace(string labels);
        Task<IList<V1Pod>> FilterPodByLabelsForAllNamespace(IDictionary<string, string> labels);

        public Task<List<Result>> Analyze();
    }
}
