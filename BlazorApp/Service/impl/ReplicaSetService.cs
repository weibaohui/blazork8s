using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.impl
{
    public class ReplicaSetService : IReplicaSetService
    {
        private readonly IBaseService  _baseService;

        public ReplicaSetService(IBaseService baseService)
        {
            _baseService   = baseService;
        }


        public async Task<V1ReplicaSet> FindByName(string name)
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
