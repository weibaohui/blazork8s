using System;
using System.Threading.Tasks;
using BlazorApp.Pages.Workload;
using BlazorApp.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Deployment;

public partial class DeploymentAction : ComponentBase
{
    [Parameter]
    public V1Deployment Item { get; set; }


    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    [Inject]
    private IOpenAiService OpenAi { get; set; }

    [Parameter]
    public EventCallback<V1Deployment> OnDeploymentDelete { get; set; }

    public bool Enable;

    protected override async Task OnInitializedAsync()
    {
        Enable = OpenAi.Enabled();
        await base.OnInitializedAsync();
    }

    private async Task DeletePod(V1Deployment pod)
    {
        await OnDeploymentDelete.InvokeAsync(pod);
    }


    private async Task OnAnalyzeClick(V1Deployment item)
    {
        var options = PageDrawerService.DefaultOptions($"AI智能分析:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<AIAnalyzeView, Object, bool>(options, new IOpenAiService.AIChatData()
        {
            data  = item,
            style = "error"
        });
    }

    private async Task OnSecurityClick(V1Deployment item)
    {
        var options = PageDrawerService.DefaultOptions($"AI安全检测:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<AIAnalyzeView, Object, bool>(options, new IOpenAiService.AIChatData()
        {
            data  = item,
            style = "security"
        });
    }
}
