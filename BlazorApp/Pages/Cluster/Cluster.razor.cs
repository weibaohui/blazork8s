using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using BlazorApp.Pages.Ai;
using BlazorApp.Pages.Common;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using BlazorApp.Utils.Prometheus.Models.Interfaces;
using Entity;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Cluster;

public partial class Cluster : PageBase
{
    private Timer _timer;

    [Inject] public IKubeService KubeService { get; set; }

    [Inject] public IPodService PodService { get; set; }

    [Inject] public INodeService NodeService { get; set; }

    [Inject] private IPromptService PromptService { get; set; }
    private List<V1ComponentStatus> ComponentStatus { get; set; }
    private List<V1APIService> ApiServicesList { get; set; }
    private IList<IMetric> AllMetrics { get; set; }

    private ServerInfo ServerInfo { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _timer = new Timer(10000);
        _timer.Elapsed += async (sender, eventArgs) => await OnTimerCallback();
        _timer.Start();
        await OnTimerCallback(); //先执行一次
        ServerInfo = await KubeService.GetServerVersion();

        await base.OnInitializedAsync();
    }

    private async Task OnTimerCallback()
    {
        var statusList = await KubeService.Client().ListComponentStatusAsync();
        ComponentStatus = statusList.Items.ToList();
        ComponentStatus.Sort((x, y) => string.Compare(x.Metadata.Name, y.Metadata.Name, StringComparison.Ordinal));

        var apiServiceList = await KubeService.Client().ListAPIServiceAsync();
        ApiServicesList = apiServiceList.Items.ToList();
        AllMetrics = await KubeService.GetMetrics();
        await InvokeAsync(StateHasChanged);
    }

    private async Task OnAnyChatClick(string item)
    {
        var prompt = PromptService.GetPrompt("FeatureExplain");
        prompt = prompt.Replace("{FeatureName}", item);
        var options = PageDrawerService.DefaultOptions($"{L["AI Chat"]}", 1000);
        await PageDrawerService.ShowDrawerAsync<AnyChat, string, bool>(options, prompt);
    }
}
