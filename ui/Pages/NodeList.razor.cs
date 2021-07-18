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
using ui.Service;

namespace ui.Pages
{
    public partial class NodeList : ComponentBase
    {

        [Inject]
        private INodeService NodeService { get; set; }
        private JsonNodeList _nodes;

        protected override async Task OnInitializedAsync()
        {
            _nodes = await NodeService.List();
        }
    }
}
