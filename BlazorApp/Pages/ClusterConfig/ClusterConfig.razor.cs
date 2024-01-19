using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Service.k8s;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ClusterConfig;

public partial class ClusterConfig : ComponentBase
{
    [Inject]
    public IKubeService KubeService { get; set; }

    [Inject]
    public IPodService PodService { get; set; }

    [Inject]
    public INodeService NodeService { get; set; }

    private IList<V1ComponentStatus> ComponentStatus { get; set; }
    private IList<V1Pod>             PodList         { get; set; }
    private IList<V1Node>            NodeList        { get; set; }
    private IList<V1APIService>      ApiServicesList { get; set; }


    protected override async Task OnInitializedAsync()
    {
        PodList  = PodService.List();
        NodeList = NodeService.List();
        var statusList = await KubeService.Client().ListComponentStatusAsync();
        ComponentStatus = statusList.Items;

        var apiServiceList = await KubeService.Client().ListAPIServiceAsync();
        ApiServicesList = apiServiceList.Items.ToList();
        await base.OnInitializedAsync();
    }
}
