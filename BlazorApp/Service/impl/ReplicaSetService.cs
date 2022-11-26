using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Pod;
using BlazorApp.Pages.ReplicaSet;
using k8s.Models;

namespace BlazorApp.Service.impl
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

        public async Task ShowReplicaSetDrawer(string  rsName)
        {
            var rs = await FilterByName(rsName);
            await ShowReplicaSetDrawer(rs);
        }
        public async Task ShowReplicaSetDrawer(V1ReplicaSet rs)
        {
            var options = new DrawerOptions
            {
                Title = "ReplicaSet:" + rs.Name(),
                Width = 800
            };
            await DrawerService.CreateAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(options, rs);
        }

        public async Task<V1ReplicaSet> FilterByName(string name)
        {
            var list = await List();
            return list.Items.First(x => x.Name() == name);
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

        public async Task<IList<V1ReplicaSet>> ListByOwnerUid(string controllerByUid)
        {
            var list = await List();
            return list.Items.Where(x => x.GetController() != null && x.GetController().Uid == controllerByUid)
                .ToList();
        }
    }
}
