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
    private IList<Result>            AnalyzeResult   { get; set; }

    private string _aiSummary = string.Empty;

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
        if (Ai.Enabled())
        {
            Ai.SetChatEventHandler(EventHandler);
        }

        await base.OnInitializedAsync();
    }

    private async void EventHandler(object sender, string resp)
    {
        _aiSummary += resp;
        await InvokeAsync(StateHasChanged);
    }

    private async Task OnSummaryClick()
    {
        if (AnalyzeResult is { Count: > 0 })
        {
            const string prompt = "请用中文归纳总结以下异常信息，并以一句话出统计k8s资源类型、错误类型、数量的概要汇总：";
            var          json   = KubernetesJson.Serialize(AnalyzeResult);
            _aiSummary = await Ai.AIChat(prompt + json);
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


    public class GroupKindCount
    {
        public string Kind  { get; set; }
        public int    Count { get; set; }
    }
}
