using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Entity;
using Extensions;
using Newtonsoft.Json;

namespace ui.Pages
{
    public partial class NodeList : ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; }

        private JsonNodeList _nodes;

        protected override async Task OnInitializedAsync()
        {
            _nodes = await Http.GetFromJsonAsync<JsonNodeList>("https://localhost:4001/KubeApi/api/v1/nodes/");
        }
    }
}
