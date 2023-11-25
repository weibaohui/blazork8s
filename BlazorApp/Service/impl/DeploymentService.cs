using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Utils;
using k8s.Models;

namespace BlazorApp.Service.impl
{
    public class DeploymentService : IDeploymentService
    {
        private readonly IBaseService                _baseService;
        private readonly ResourceCache<V1Deployment> _cache = ResourceCacheHelper<V1Deployment>.Instance.Build();

        public DeploymentService(IBaseService baseService)
        {
            _baseService = baseService;
        }


        public V1Deployment FindByName(string name)
        {
            var list = List();
            return list.First(x => x.Name() == name);
        }

        public List<V1Deployment> List()
        {
            return _cache.Get();
        }

        public List<V1Deployment> ListByNamespace(string ns)
        {
            if (string.IsNullOrEmpty(ns))
            {
                return List();
            }

            return List().Where(x => x.Namespace() == ns).ToList();
        }


        public Task<IList<V1Deployment>> ListItemsByNamespaceAsync(string ns)
        {
            throw new System.NotImplementedException();
        }
    }
}