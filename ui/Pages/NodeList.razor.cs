using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Entity;

namespace ui.Pages
{
    public partial class NodeList: ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; }

        private IEnumerable<NodeVO> _nodes;


        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine(_nodes);
             _nodes = await Http.GetFromJsonAsync<IEnumerable<NodeVO>>("https://localhost:4001/Node/GetNodes");
            foreach (var node in _nodes)
            {
                Console.WriteLine(node.Name);
            }
        }
    }
}
