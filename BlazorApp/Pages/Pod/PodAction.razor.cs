using System;
using System.Threading.Tasks;
using BlazorApp.Pages.Workload;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod;

public partial class PodAction : ComponentBase
{
    [Parameter]
    public V1Pod PodItem { get; set; }

    [Inject]
    private IPodService PodService { get; set; }


    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    [Inject]
    private IOpenAiService OpenAi { get; set; }

    [Parameter]
    public EventCallback<V1Pod> OnPodDelete { get; set; }

    public bool Enable;

    protected override async Task OnInitializedAsync()
    {
        Enable = OpenAi.Enabled();
        await base.OnInitializedAsync();
    }

    private async Task DeletePod(V1Pod pod)
    {
        await PodService.DeletePod(pod.Namespace(), pod.Name());
        await OnPodDelete.InvokeAsync(pod);
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
}