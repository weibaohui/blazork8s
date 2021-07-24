using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Entity;
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

        public async Task<JsonNodeList> List()
        {
            return await _baseService.GetFromJsonAsync<JsonNodeList>("/KubeApi/api/v1/nodes/");
        }
    }
}
