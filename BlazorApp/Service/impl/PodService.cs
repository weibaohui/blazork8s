using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.impl
{
    public class PodService : IPodService
    {
        private readonly IBaseService  _baseService;
        private readonly IWatchService _watchService;

        public PodService(IBaseService baseService, IWatchService watchService)
        {
            _baseService  = baseService;
            _watchService = watchService;
            // Console.WriteLine("PodService 初始化");
        }


        public async Task<IList<V1Pod>> ListByOwnerUid(string controllerByUid)
        {
            var list = await ListPods();
            return list.Where(x => x.GetController() != null && x.GetController().Uid == controllerByUid)
                .ToList();
        }


        public async Task<IList<V1Pod>> ListPods()
        {
            return await _watchService.ListPods();
        }

        private async Task<IList<V1Pod>> ListPodByNamespace(string ns)
        {
            var pods = await _watchService.ListPods();
            if (string.IsNullOrEmpty(ns))
            {
                return pods;
            }

            return pods.Where(x => x.Namespace() == ns).ToList();
        }


        //
        public async Task<bool> DeletePod(string ns, string name)
        {
            // Console.WriteLine($"DeletePod,{ns},{name}");
            return await _baseService.Client().DeleteNamespacedPodAsync(name, ns) != null;
        }

        public async Task Logs(V1Pod pod, bool follow = false, bool previous = false)
        {
            var response = await _baseService.Client().CoreV1.ReadNamespacedPodLogWithHttpMessagesAsync(
                pod.Metadata.Name,
                pod.Metadata.NamespaceProperty,
                container: pod.Spec.Containers[0].Name,
                tailLines: 1,
                previous: previous,
                follow: follow).ConfigureAwait(false);
            var stream = response.Body;
            await stream.CopyToAsync(Console.OpenStandardOutput());
        }

        public async Task Logs(string podNs, string podName, string containerName, bool follow = false,
            bool                      previous = false)
        {
            var response = await _baseService.Client().CoreV1.ReadNamespacedPodLogWithHttpMessagesAsync(
                podName,
                podNs,
                container: containerName,
                tailLines: 10,
                previous: previous,
                follow: follow).ConfigureAwait(false);
            var stream = response.Body;
            await stream.CopyToAsync(Console.OpenStandardOutput());
        }

        public async Task<IList<V1Pod>> ListItemsByNamespaceAsync(string ns)
        {
            return await ListPodByNamespace(ns);
        }

        public async Task<int> NodePodsNum()
        {
            var pods = await ListPods();
            var tuples = pods.GroupBy(s => s.Spec.NodeName)
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
