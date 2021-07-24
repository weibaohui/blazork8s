using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace ui.Service.impl
{
    public class NodeService : INodeService
    {

        private readonly IBaseService _baseService;

        public NodeService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<V1NodeList> List()
        {
            return await _baseService.GetFromJsonAsync<V1NodeList>("/KubeApi/api/v1/nodes/");
        }
    }
}
