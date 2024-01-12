using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class PodService : CommonAction<V1Pod>, IPodService
    {
        private readonly IKubeService  _baseService;

        public PodService(IKubeService baseService)
        {
            _baseService   = baseService;
        }


        public IList<V1Pod> ListByNodeName(string nodeName)
        {
            var list = List();
            return list.Where(x => x.Spec.NodeName == nodeName)
                .ToList();
        }

        public IEnumerable<Tuple<string, int>> NodePodsNum()
        {
            var pods = List();
            var tuples = pods.GroupBy(s => s.Spec.NodeName)
                .OrderBy(g => g.Key)
                .Select(g => Tuple.Create(g.Key, g.Count()));
            foreach (var tuple in tuples)
            {
                Console.WriteLine($"{tuple.Item1}={tuple.Item2}");
            }

            return tuples;
        }

        public new async Task<object> Delete(string ns, string name)
        {
            return await _baseService.Client().DeleteNamespacedPodAsync(name, ns);
        }
    }
}
