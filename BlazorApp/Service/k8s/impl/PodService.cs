using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Pod;
using BlazorApp.Utils;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.k8s.impl
{
    public class PodService : CommonAction<V1Pod>, IPodService
    {
        private readonly IBaseService  _baseService;
        private          DrawerService _drawerService;

        public PodService(IBaseService baseService,DrawerService drawerService)
        {
            _baseService   = baseService;
            _drawerService = drawerService;
        }


        public IList<V1Pod> ListByNodeName(string nodeName)
        {
            var list = List();
            return list.Where(x => x.Spec.NodeName == nodeName)
                .ToList();
        }


        public async Task<bool> DeletePod(string ns, string name)
        {
            // Console.WriteLine($"DeletePod,{ns},{name}");
            return await _baseService.Client().DeleteNamespacedPodAsync(name, ns) != null;
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


       public async Task ShowPodDetailViewByPod(V1Pod pod)
        {
            await PageDrawerHelper<V1Pod>.Instance
                .SetDrawerService(_drawerService)
                .ShowDrawerAsync<PodDetailView, V1Pod, bool>(pod);
        }
    }
}
