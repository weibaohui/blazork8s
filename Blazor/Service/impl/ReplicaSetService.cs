using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using Blazor.Pages.Pod;
using k8s.Models;

namespace Blazor.Service.impl
{
    public class ReplicaSetService : IReplicaSetService
    {
        private readonly IBaseService  BaseService;
        private readonly DrawerService DrawerService;

        public ReplicaSetService(IBaseService baseService, DrawerService drawerService)
        {
            BaseService   = baseService;
            DrawerService = drawerService;
        }

        public async Task ShowReplicaSetDrawer(V1ReplicaSet rs)
        {
            var options = new DrawerOptions
            {
                Title = "ReplicaSet:" + rs.Name(),
                Width = 800
            };
            //await DrawerService.CreateAsync<ReplicaSetDetailView, V1Pod, bool>(options, rs);
        }

        public async Task<V1ReplicaSetList> List()
        {
            return await BaseService.GetFromJsonAsync<V1ReplicaSetList>("/KubeApi/apis/apps/v1/replicasets");
        }

        public async Task<V1ReplicaSetList> ListByNamespace(string ns)
        {
            if (string.IsNullOrEmpty(ns))
            {
                return await List();
            }

            return await BaseService.GetFromJsonAsync<V1ReplicaSetList>(
                @$"/KubeApi/apis/apps/v1/namespaces/{ns}/replicasets");
        }

        public async Task<IList<V1ReplicaSet>> ListItemsByNamespaceAsync(string ns)
        {
            var ls = await ListByNamespace(ns);
            return ls.Items;
        }
    }
}
