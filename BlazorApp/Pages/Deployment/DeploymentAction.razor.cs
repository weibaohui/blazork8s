using System;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Pages.Workload;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Deployment;

public partial class DeploymentAction : ComponentBase
{
    [Inject]
    public IDeploymentService DeploymentService { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; } = MenuMode.Vertical;

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

    private async Task OnDeleteClick(V1Deployment item)
    {
        DeploymentService.Delete(item.Namespace(), item.Name());
        await OnDeploymentDelete.InvokeAsync(item);
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


    private async Task OnYamlClick(V1Deployment item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1Deployment>, V1Deployment, bool>(options, item);
    }

    private async Task OnDocClick(V1Deployment item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<V1Deployment>, V1Deployment, bool>(options, item);
    }
}
