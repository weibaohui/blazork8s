using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ui.Pages
{
    public partial class NodeList: ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; }

        private IEnumerable<Node> _nodes;


        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine(_nodes);
             _nodes = await Http.GetFromJsonAsync<IEnumerable<Node>>("https://localhost:4001/Node/GetNodes");
            foreach (var node in _nodes)
            {
                Console.WriteLine(node.Name);
            }
        }
    }
}
