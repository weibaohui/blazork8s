using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Entity;
using Extensions;
using k8s.Models;
using Newtonsoft.Json;
using ui.Service;

namespace ui.Pages
{
    public partial class NodeList : ComponentBase
    {


        [Inject]
        private INodeService NodeService { get; set; }

        [Inject]
        private IPodService PodService { get; set; }

        private V1NodeList _nodes;
        private V1PodList  _pods;

        protected override async Task OnInitializedAsync()
        {
            _nodes = await NodeService.List();
            _pods  = await PodService.List();
        }
    }
}
