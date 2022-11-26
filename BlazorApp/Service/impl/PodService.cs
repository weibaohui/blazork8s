using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Pod;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Caching.Memory;

namespace BlazorApp.Service.impl
{
    public class PodService : IPodService
    {
        private readonly IBaseService  BaseService;
        private readonly DrawerService DrawerService;
        private readonly IMemoryCache  MemoryCache;

        private const string CachePodList = "cache_pod_list";

        //TODO 作为一个能够独立更新的服务，获取POD的数据，都从这里获取，不再去转发请求
        private readonly List<V1Pod> _sharedPods = new();

        public PodService(IBaseService baseService, IMemoryCache memoryCache, DrawerService drawerService)
        {
            BaseService   = baseService;
            MemoryCache   = memoryCache;
            DrawerService = drawerService;
            Console.WriteLine("PodService 初始化");
        }

        public async Task ShowPodDrawer(V1Pod pod)
        {
            var options = new DrawerOptions
            {
                Title = "POD:" + pod.Name(),
                Width = 800
            };
            await DrawerService.CreateAsync<PodDetailView, V1Pod, bool>(options, pod);
        }

        public async Task<IList<V1Pod>> ListByOwnerUid(string controllerByUid)
        {
            var list = await List();
            return list.Items.Where(x => x.GetController() != null && x.GetController().Uid == controllerByUid)
                .ToList();
        }


        public void UpdateSharePods(WatchEventType type, V1Pod item)
        {
            switch (type)
            {
                case WatchEventType.Added:
                    _sharedPods.Insert(0, item);
                    break;
                case WatchEventType.Modified:
                    for (var i = 0; i < _sharedPods.Count; i++)
                    {
                        if (_sharedPods[i].Uid() == item.Uid())
                        {
                            _sharedPods[i] = item;
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
        }

        public async Task<V1PodList> List()
        {
            // return await MemoryCache.GetOrCreateAsync<V1PodList>(CachePodList, async r =>
            // {
            //     var pods = await BaseService.GetFromJsonAsync<V1PodList>("/KubeApi/api/v1/pods");
            //     foreach (var podsItem in pods.Items) SharedPods.Append<V1Pod>(podsItem);
            //     return pods;
            // });
            if (_sharedPods.Count == 0)
            {
                // Console.WriteLine($"Task<V1PodList> List()空，初始化获取,{_sharedPods.Count}");
                var pods = await BaseService.Client().ListPodForAllNamespacesAsync();
                foreach (var podsItem in pods.Items)
                {
                    // Console.WriteLine($"pod ={podsItem.Name()}");
                    _sharedPods.Add(podsItem);
                    // Console.WriteLine($"SharedPods.Add(podsItem),{_sharedPods.Count}");
                }
            }
            // Console.WriteLine($"Task<V1PodList> List(),{_sharedPods.Count}");

            var list = new V1PodList
            {
                Items = new List<V1Pod>(_sharedPods)
            };
            return list;
        }

        public async Task<IList<V1Pod>> ListPods()
        {
            // return await MemoryCache.GetOrCreateAsync<V1PodList>(CachePodList, async r =>
            // {
            //     var pods = await BaseService.GetFromJsonAsync<V1PodList>("/KubeApi/api/v1/pods");
            //     foreach (var podsItem in pods.Items) SharedPods.Append<V1Pod>(podsItem);
            //     return pods;
            // });
            if (_sharedPods.Count == 0)
            {
                var pods = await BaseService.Client().ListPodForAllNamespacesAsync();
                foreach (var item in pods.Items)
                {
                    _sharedPods.Add(item);
                }
            }

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

        public async Task<V1PodList> ListByNamespace(string ns)
        {
            if (string.IsNullOrEmpty(ns))
            {
                return await List();
            }

            return await BaseService.Client().ListNamespacedPodAsync(ns);
        }

        //
        public async Task<bool> DeletePod(string ns, string name)
        {
            return await BaseService.Client().DeleteNamespacedPodAsync(name, ns) != null;
        }

        public async Task<IList<V1Pod>> ListItemsByNamespaceAsync(string ns)
        {
            return await ListPodByNamespace(ns);
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
