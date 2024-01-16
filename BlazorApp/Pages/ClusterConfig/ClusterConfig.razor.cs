using System.Collections.Generic;
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

    private IList<V1ComponentStatus> ComponentStatus { get; set; }
    private IEnumerable<PodMetrics>  PodMetricsList  { get; set; }
    public  IEnumerable<NodeMetrics> NodeMetricsList { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var podMetricsList = await KubeService.Client().GetKubernetesPodsMetricsAsync();
        PodMetricsList = podMetricsList.Items;

        var nodeMetricsList = await KubeService.Client().GetKubernetesNodesMetricsAsync();
        NodeMetricsList = nodeMetricsList.Items;


        var statusList = await KubeService.Client().ListComponentStatusAsync();
        ComponentStatus = statusList.Items;


        await base.OnInitializedAsync();
    }
}
