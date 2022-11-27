using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.impl
{
    public class DeploymentService : IDeploymentService
    {
        private readonly IBaseService  _baseService;

        public DeploymentService(IBaseService baseService)
        {
            _baseService   = baseService;
        }



        public async Task<V1Deployment> FindByName(string name)
        {
            var list = await List();
            return list.Items.First(x => x.Name() == name);
        }

        public async Task<V1DeploymentList> List()
        {
            return await _baseService.Client().ListDeploymentForAllNamespacesAsync();
        }

        public async Task<V1DeploymentList> ListByNamespace(string ns)
        {
            if (string.IsNullOrEmpty(ns))
            {
                return await List();
            }

            return await _baseService.Client().ListNamespacedDeploymentAsync(ns);
        }

        public async Task<IList<V1Deployment>> ListItemsByNamespaceAsync(string ns)
        {
            var ls = await ListByNamespace(ns);
            return ls.Items;
        }
    }
}
