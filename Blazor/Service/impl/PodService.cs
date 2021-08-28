using System;
using System.Linq;
using System.Threading.Tasks;
using k8s.Models;

namespace Blazor.Service.impl
{
    public class PodService : IPodService
    {
        private readonly IBaseService _baseService;

        public PodService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<V1PodList> List()
        {
            return await _baseService.GetFromJsonAsync<V1PodList>("/KubeApi/api/v1/pods");
        }

        public async Task<V1PodList> ListByNamespace(string ns)
        {
            if (string.IsNullOrEmpty(ns))
            {
                return await List();
            }

            return await _baseService.GetFromJsonAsync<V1PodList>(@$"/KubeApi/api/v1/namespaces/{ns}/pods");
        }


        public async Task<int> NodePodsNum()
        {
            var pods = await List();
            var tuples = pods.Items.GroupBy(s => s.Spec.NodeName)
                .OrderBy(g => g.Key)
                .Select(g => Tuple.Create(g.Key, g.Count()));
            foreach (var tuple in tuples)
            {
                Console.WriteLine($"{tuple.Item1}={tuple.Item2}");
            }

            return await Task.FromResult(0);
        }
    }
}
