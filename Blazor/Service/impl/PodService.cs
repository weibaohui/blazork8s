using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using Blazor.Pages.Pod;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Blazor.Service.impl
{
    public class PodService : IPodService
    {
        private readonly IBaseService  BaseService;
        private readonly DrawerService DrawerService;
        private readonly IMemoryCache  MemoryCache;

        private const string       CachePodList = "cache_pod_list";
        private       List<V1Pod> SharedPods   = new List<V1Pod>();

        public PodService(IBaseService baseService, DrawerService drawerService, IMemoryCache memoryCache)
        {
            BaseService   = baseService;
            DrawerService = drawerService;
            MemoryCache   = memoryCache;
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
                    SharedPods.Add(item);
                    break;
                case WatchEventType.Modified:
                     for (var i = 0; i < SharedPods.Count; i++)
                     {
                         if (SharedPods[i].Uid()==item.Uid())
                         {
                             SharedPods[i] = item;
                         }
                     }
                     break;
                case WatchEventType.Deleted:
                    SharedPods.RemoveAll(w=>w.Uid() == item.Uid());
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

            return  await MemoryCache.GetOrCreateAsync<V1PodList>(CachePodList, async r =>
            {
                var pods = await BaseService.GetFromJsonAsync<V1PodList>("/KubeApi/api/v1/pods");
                foreach (var podsItem in pods.Items) SharedPods.Append<V1Pod>(podsItem);
                return pods;
            });
        }

        public async Task<V1PodList> ListByNamespace(string ns)
        {
            if (string.IsNullOrEmpty(ns))
            {
                return await List();
            }

            return await BaseService.GetFromJsonAsync<V1PodList>(@$"/KubeApi/api/v1/namespaces/{ns}/pods");
        }

        public async Task<IList<V1Pod>> ListItemsByNamespaceAsync(string ns)
        {
            var ls = await ListByNamespace(ns);
            return ls.Items;
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
