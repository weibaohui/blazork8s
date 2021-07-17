using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Entity;
using Extension;
using Extension.k8s;

namespace ui.Pages
{
    public partial class NodeList : ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; }

        private IEnumerable<NodeVO> _nodes;
        private JsonNode            _jsonNode;
         
        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine(_nodes);
            _nodes = await Http.GetFromJsonAsync<IEnumerable<NodeVO>>("https://localhost:4001/Node/GetNodes");
            foreach (var node in _nodes)
            {
                Console.WriteLine(node);
            }

            _jsonNode = await Http.GetFromJsonAsync<JsonNode>(
                "https://localhost:4001/KubeApi/api/v1/nodes/docker-desktop");
            var capacity = _jsonNode.Status.Capacity;
            foreach (var kv in capacity)
            {
                Console.WriteLine($"{kv.Key}-{kv.Value}");
            }



        }

    }
}
