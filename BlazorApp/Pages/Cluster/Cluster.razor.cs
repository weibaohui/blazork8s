using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Pages.Workload;
using BlazorApp.Service;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using Entity.Analyze;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Cluster;

public partial class Cluster : ComponentBase
{
    [Inject]
    public IKubeService KubeService { get; set; }

    [Inject]
    public IPodService PodService { get; set; }

    [Inject]
    public INodeService NodeService { get; set; }
    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }


    [Inject]
    private IAiService Ai { get; set; }

    private IList<V1ComponentStatus> ComponentStatus { get; set; }
    private IList<V1Pod>             PodList         { get; set; }
    private IList<V1Node>            NodeList        { get; set; }
    private IList<V1APIService>      ApiServicesList { get; set; }
    private IList<Result>            AnalyzeResult  { get; set; }


    protected override async Task OnInitializedAsync()
    {
        PodList  = PodService.List();
        NodeList = NodeService.List();
        var statusList = await KubeService.Client().ListComponentStatusAsync();
        ComponentStatus = statusList.Items;

        var apiServiceList = await KubeService.Client().ListAPIServiceAsync();
        ApiServicesList = apiServiceList.Items.ToList();

        // await TranslateService.ProcessKubeExplains();

         AnalyzeResult = await PodService.Analyze();
        await base.OnInitializedAsync();
    }



    private async Task OnAnalyzeClick(object item)
    {
        var options = PageDrawerService.DefaultOptions($"智能分析:", width: 1000);
        await PageDrawerService.ShowDrawerAsync<AiAnalyzeView, IAiService.AiChatData, bool>(options,
            new IAiService.AiChatData
            {
                Data  =  item,
                Style = "error"
            });
    }
}
