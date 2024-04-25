using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using AntDesign;
using BlazorApp.Pages.Ai;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Node;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using BlazorApp.Utils.Prometheus.Models.Interfaces;
using Entity;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Result = Entity.Analyze.Result;

namespace BlazorApp.Pages.Cluster;

public partial class Inspection : PageBase
{
    private string _aiSummary = string.Empty;

    private Timer _timer;

    [Inject] IPromptService PromptService { get; set; }
    [Inject] public IKubeService KubeService { get; set; }

    [Inject] public IPodService PodService { get; set; }

    [Inject] public INodeService NodeService { get; set; }

    [Inject] private IMessageService MessageService { get; set; }
    [Inject] private IAiService Ai { get; set; }

    private IList<V1Pod> PodList { get; set; }
    private IList<V1Node> NodeList { get; set; }
    private IList<Result> AnalyzeResult { get; set; }
    private DateTime LastInspection { get; set; }
    private IList<string> PassResources { get; set; }
    private Dictionary<string, int> AllResourcesCount { get; set; }
    private IList<IMetric> AllMetrics { get; set; }


    private string LivezResult { get; set; }
    private string ReadyzResult { get; set; }

    private ServerInfo ServerInfo { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Ai.Enabled())
        {
            Ai.SetChatEventHandler(EventHandler);
        }

        _timer = new Timer(10000);
        _timer.Elapsed += async (sender, eventArgs) => await OnTimerCallback();
        _timer.Start();
        await OnTimerCallback(); //先执行一次
        ServerInfo = await KubeService.GetServerVersion();

        await base.OnInitializedAsync();
    }

    private async Task OnTimerCallback()
    {
        PodList = PodService.List();
        NodeList = NodeService.List();


        //巡检信息
        PassResources = ClusterInspectionResultContainer.Instance.GetPassResources();
        AnalyzeResult = ClusterInspectionResultContainer.Instance.GetResults();
        LastInspection = ClusterInspectionResultContainer.Instance.LastInspection;
        AllResourcesCount = ClusterInspectionResultContainer.Instance.GetAllResourcesCount();
        LivezResult = ClusterInspectionResultContainer.Instance.LivezResult;
        ReadyzResult = ClusterInspectionResultContainer.Instance.ReadyzResult;
        AnalyzeResult = AnalyzeResult.OrderBy(x => x.Kind).ThenBy(x => x.Name()).ToList();
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
                var prompt = PromptService.GetPrompt("ErrorSummarize");
                var json = KubernetesJson.Serialize(AnalyzeResult);
                _aiSummary = await Ai.AIChat(prompt + json);
            }
        }
        else
        {
            _aiSummary = L["AI service not enabled"];
        }
    }

    private async Task OnAnalyzeClick(object item)
    {
        if (Ai.Enabled())
        {
            var options = PageDrawerService.DefaultOptions($"{L["AI Analysis"]}:", 1000);
            await PageDrawerService.ShowDrawerAsync<AiAnalyzeView, IAiService.AiChatData, bool>(options,
                new IAiService.AiChatData
                {
                    Data = item,
                    Style = "error"
                });
        }
        else
        {
            await MessageService.Error($"{L["AI service not enabled"]}");
        }
    }

    private V1ObjectReference GetRef(Result item)
    {
        return KubeHelper.GetObjectRef(item.Kind, item.Namespace(), item.Name());
    }

    private async Task OnItemNameClick(V1Node item)
    {
        await PageDrawerHelper<V1Node>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<NodeDetailView, V1Node, bool>(item);
    }
}
