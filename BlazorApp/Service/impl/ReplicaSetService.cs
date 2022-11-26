using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Pod;
using BlazorApp.Pages.ReplicaSet;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.impl
{
    public class ReplicaSetService : IReplicaSetService
    {
        private readonly IBaseService  _baseService;
        private readonly DrawerService _drawerService;

        public ReplicaSetService(IBaseService baseService, DrawerService drawerService)
        {
            _baseService   = baseService;
            _drawerService = drawerService;
        }

        public async Task ShowReplicaSetDrawer(string rsName)
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
            await _drawerService.CreateAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(options, rs);
        }

        public async Task<V1ReplicaSet> FilterByName(string name)
        {
            var list = await ListAllReplicaSet();
            return list.Items.First(x => x.Name() == name);
        }

        public async Task<V1ReplicaSetList> ListAllReplicaSet()
        {
            return await _baseService.Client().ListReplicaSetForAllNamespacesAsync();
        }

        public async Task<V1ReplicaSetList> ListByNamespace(string ns)
        {
            if (string.IsNullOrEmpty(ns))
            {
                return await ListAllReplicaSet();
            }

            return await _baseService.Client().ListNamespacedReplicaSetAsync(ns);
        }

        public async Task<IList<V1ReplicaSet>> ListItemsByNamespaceAsync(string ns)
        {
            var ls = await ListByNamespace(ns);
            return ls.Items;
        }

        public async Task<IList<V1ReplicaSet>> ListByOwnerUid(string controllerByUid)
        {
            var list = await ListAllReplicaSet();
            return list.Items.Where(x => x.GetController() != null && x.GetController().Uid == controllerByUid)
                .ToList();
        }
    }
}
