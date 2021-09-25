using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using Blazor.Pages.Pod;
using k8s.Models;

namespace Blazor.Service.impl
{
    public class PodService : IPodService
    {
        private readonly IBaseService  BaseService;
        private readonly DrawerService DrawerService;

        public PodService(IBaseService baseService, DrawerService drawerService)
        {
            BaseService   = baseService;
            DrawerService = drawerService;
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

        public async Task<V1PodList> List()
        {
            return await BaseService.GetFromJsonAsync<V1PodList>("/KubeApi/api/v1/pods");
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
