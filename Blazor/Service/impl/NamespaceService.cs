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

        /// <summary>
        /// 缓存当前NS列表
        /// </summary>
        private IList<V1Namespace> ns;
        public async Task<IList<V1Namespace>> GetNamespaces()
        {
            if (ns != null)
            {
                return ns;
            }

            _ = await List();
            return ns;
        }

        public async Task<V1NamespaceList> List()
        {
           var list=  await _baseService.GetFromJsonAsync<V1NamespaceList>("/KubeApi/api/v1/namespaces");
            //缓存当前获取到的NS，每次获取都做强制更新
            ns = list.Items.ToList();
            return list;
        }
    }
}
