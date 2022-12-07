using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Caching.Memory;

namespace BlazorApp.Service.impl
{
    public class PodService : IPodService
    {
        private readonly IBaseService _baseService;
        private readonly IMemoryCache _memoryCache;
        private          bool         _podListChangedByWatch;

        private const string CachePodList = "cache_pod_list";

        //TODO 作为一个能够独立更新的服务，获取POD的数据，都从这里获取，不再去转发请求
        private readonly List<V1Pod> _sharedPods = new();

        public PodService(IBaseService baseService, IMemoryCache memoryCache)
        {
            _baseService = baseService;
            _memoryCache = memoryCache;
            // Console.WriteLine("PodService 初始化");
        }


        public async Task<IList<V1Pod>> ListByOwnerUid(string controllerByUid)
        {
            var list = await ListPods();
            return list.Where(x => x.GetController() != null && x.GetController().Uid == controllerByUid)
                .ToList();
        }


        public void UpdateSharePods(WatchEventType type, V1Pod item)
        {
            switch (type)
            {
                case WatchEventType.Added:

                    for (var i = 0; i < _sharedPods.Count; i++)
                    {
                        if (_sharedPods[i].Uid() == item.Uid()) continue;
                        _sharedPods.Insert(0, item);
                    }

                    break;
                case WatchEventType.Modified:
                    for (var i = 0; i < _sharedPods.Count; i++)
                    {
                        if (_sharedPods[i].Uid() == item.Uid())
                        {
                            _sharedPods[i] = item;
                            break;
                        }
                    }

                    break;
                case WatchEventType.Deleted:
                    _sharedPods.RemoveAll(w => w.Uid() == item.Uid());
                    break;
                case WatchEventType.Error:
                    break;
                case WatchEventType.Bookmark:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            _podListChangedByWatch = true;
        }


        public async Task<IList<V1Pod>> ListPods()
        {
            if (_sharedPods.Count == 0)
            {
                var pods = await _baseService.Client().ListPodForAllNamespacesAsync();
                foreach (var item in pods.Items)
                {
                    _sharedPods.Add(item);
                }
            }

            _podListChangedByWatch = false;
            return _sharedPods;
        }

        public async Task<IList<V1Pod>> ListPodByNamespace(string ns)
        {
            if (string.IsNullOrEmpty(ns))
            {
                return await ListPods();
            }

            return _sharedPods.Where(x => x.Namespace() == ns).ToList();
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

        public async Task WatchAllPod()
        {
            var podlistResp = _baseService.Client().CoreV1
                .ListPodForAllNamespacesWithHttpMessagesAsync(watch: true);
            await foreach (var (type, item) in podlistResp.WatchAsync<V1Pod, V1PodList>())
            {
                Console.WriteLine("==on watch event==");
                Console.WriteLine($"{type}:{item.Metadata.Name}");
                UpdateSharePods(type, item);
                Console.WriteLine("==on watch event==");
            }
        }

        public bool PodListChangedByWatch()
        {
            return _podListChangedByWatch;
        }
    }
}
