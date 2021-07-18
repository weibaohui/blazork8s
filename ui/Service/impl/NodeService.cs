using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Components;

namespace ui.Service.impl
{
    public class NodeService : INodeService
    {

        [Inject]
        private IBaseService BaseService { get; set; }

        public NodeService(IBaseService baseService)
        {
            BaseService = baseService;
        }

        public async Task<JsonNodeList> List()
        {
            return await BaseService.GetFromJsonAsync<JsonNodeList>("/KubeApi/api/v1/nodes/");
         }
    }
}
