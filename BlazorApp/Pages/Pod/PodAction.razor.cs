using System;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Pages.Workload;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.Pod;

public partial class PodAction : ComponentBase
{
    [Parameter]
    public V1Pod PodItem { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

    [Inject]
    private IPodService PodService { get; set; }


    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    [Inject]
    private IOpenAiService OpenAi { get; set; }


    [Inject]
    private ILogger<PodAction> Logger { get; set; }
    public bool Enable;


    protected override async Task OnInitializedAsync()
    {
        Enable = OpenAi.Enabled();
        await base.OnInitializedAsync();
    }

    private async Task OnPodDeleteClick(V1Pod pod)
    {
        await PodService.Delete(pod.Namespace(), pod.Name());
        StateHasChanged();
    }


    private async Task OnPodLogClick(V1Pod pod)
    {
        var options = PageDrawerService.DefaultOptions($"Logs:{pod.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<PodLogsView, V1Pod, bool>(options, pod);
    }

    private async Task OnPodExecClick(V1Pod pod)
    {
        var options = PageDrawerService.DefaultOptions($"Logs:{pod.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<PodExecView, V1Pod, bool>(options, pod);
    }

    private async Task OnAnalyzeClick(V1Pod pod)
    {
        var options = PageDrawerService.DefaultOptions($"AI智能分析:{pod.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<AIAnalyzeView, Object, bool>(options, new IOpenAiService.AIChatData()
        {
            data  = pod,
            style = "error"
        });
    }

    private async Task OnSecurityClick(V1Pod pod)
    {
        var options = PageDrawerService.DefaultOptions($"AI安全检测:{pod.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<AIAnalyzeView, Object, bool>(options, new IOpenAiService.AIChatData()
        {
            data  = pod,
            style = "security"
        });
    }

    private async Task OnYamlClick(V1Pod item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1Pod>, V1Pod, bool>(options, item);
    }
}
