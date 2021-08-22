using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s.Models;

namespace Blazor.Service.impl
{
    public class NamespaceService : INamespaceService
    {
        private readonly IBaseService _baseService;

        public NamespaceService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        private IList<V1Namespace> ns;
        public async Task<IList<V1Namespace>> GetNamespaces()
        {
            if (ns != null)
            {
                return ns;
            }
            var x= await List();
            return ns;
        }

        public async Task<V1NamespaceList> List()
        {
           var list=  await _baseService.GetFromJsonAsync<V1NamespaceList>("/KubeApi/api/v1/namespace");
            ns = list.Items.ToList();
            return list;
        }
    }
}
