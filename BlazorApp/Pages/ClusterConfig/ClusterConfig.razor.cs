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

    private IList<V1ComponentStatus> ComponentStatus { get; set; }
    private IList<PodMetrics>        PodMetricsList  { get; set; }


    protected override async Task OnInitializedAsync()
    {
        var podMetricsList = await KubeService.Client().GetKubernetesPodsMetricsAsync();
        PodMetricsList = podMetricsList.Items.ToList();
        var statusList = await KubeService.Client().ListComponentStatusAsync();
        ComponentStatus = statusList.Items;
        await base.OnInitializedAsync();
    }

}
