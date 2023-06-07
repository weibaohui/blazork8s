using System.Threading.Tasks;
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
        var options = PageDrawerService.DefaultOptions($"分析Deployment:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DeploymentAnalyzeView, V1Deployment, bool>(options, item);
    }
}
