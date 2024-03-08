using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using BlazorApp.Pages.Workload;
using BlazorApp.Service;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using BlazorApp.Utils.Prometheus.Models.Interfaces;
using Entity;
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

    private List<V1ComponentStatus> ComponentStatus   { get; set; }
    private IList<V1Pod>            PodList           { get; set; }
    private IList<V1Node>           NodeList          { get; set; }
    private List<V1APIService>      ApiServicesList   { get; set; }
    private IList<Result>           AnalyzeResult     { get; set; }
    private DateTime                LastInspection    { get; set; }
    private IList<string>           PassResources     { get; set; }
    private Dictionary<string, int> AllResourcesCount { get; set; }
    private IList<IMetric>          AllMetrics        { get; set; }

    bool apiserver_request_detail_visible = false;
    bool etcd_storage_detail_visible      = false;

    public  string LivezResult  { get; set; }
    public  string ReadyzResult { get; set; }
    private string _aiSummary = string.Empty;

    private Timer _timer;

    private ServerInfo ServerInfo { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Ai.Enabled())
        {
            Ai.SetChatEventHandler(EventHandler);
        }

        _timer         =  new Timer(10000);
        _timer.Elapsed += async (sender, eventArgs) => await OnTimerCallback();
        _timer.Start();
        await OnTimerCallback(); //先执行一次
        ServerInfo = await KubeService.GetServerVersion();
        await base.OnInitializedAsync();
    }

    private async Task OnTimerCallback()
    {
        PodList  = PodService.List();
        NodeList = NodeService.List();
        var statusList = await KubeService.Client().ListComponentStatusAsync();
        ComponentStatus = statusList.Items.ToList();
        ComponentStatus.Sort((x, y) => string.Compare(x.Metadata.Name, y.Metadata.Name, StringComparison.Ordinal));

        var apiServiceList = await KubeService.Client().ListAPIServiceAsync();
        ApiServicesList = apiServiceList.Items.ToList();


        //巡检信息
        PassResources     = ClusterInspectionResultContainer.Instance.GetPassResources();
        AnalyzeResult     = ClusterInspectionResultContainer.Instance.GetResults();
        LastInspection    = ClusterInspectionResultContainer.Instance.LastInspection;
        AllResourcesCount = ClusterInspectionResultContainer.Instance.GetAllResourcesCount();
        LivezResult       = ClusterInspectionResultContainer.Instance.LivezResult;
        ReadyzResult      = ClusterInspectionResultContainer.Instance.ReadyzResult;
        AnalyzeResult     = AnalyzeResult.OrderBy(x => x.Kind).ThenBy(x => x.Name()).ToList();
        AllResourcesCount = AllResourcesCount.OrderBy(x => x.Key).ToList().ToDictionary(x => x.Key, x => x.Value);

        AllMetrics = await KubeService.GetMetrics();


        await InvokeAsync(StateHasChanged);
    }



    private async void EventHandler(object sender, string resp)
    {
        _aiSummary += resp;
        await InvokeAsync(StateHasChanged);
    }

    private async Task OnSummaryClick()
    {
        _aiSummary = string.Empty;
        if (Ai.Enabled())
        {
            if (AnalyzeResult is { Count: > 0 })
            {
                const string prompt = "请用中文归纳总结以下异常信息，并以300字以内给出概要统计信息。";
                var          json   = KubernetesJson.Serialize(AnalyzeResult);
                _aiSummary = await Ai.AIChat(prompt + json);
            }
        }
        else
        {
            _aiSummary = "AI服务未启用";
        }
    }

    private async Task OnAnalyzeClick(object item)
    {
        var options = PageDrawerService.DefaultOptions($"智能分析:", width: 1000);
        await PageDrawerService.ShowDrawerAsync<AiAnalyzeView, IAiService.AiChatData, bool>(options,
            new IAiService.AiChatData
            {
                Data  = item,
                Style = "error"
            });
    }

    private V1ObjectReference GetRef(Result item)
    {
        return new V1ObjectReference()
        {
            Kind              = item.Kind,
            Name              = item.Name(),
            NamespaceProperty = item.Namespace(),
        };
    }
}
