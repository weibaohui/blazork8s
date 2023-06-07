using System.Threading.Tasks;
using BlazorApp.Service;
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
    private IConfigService ConfigService { get; set; }

    [Parameter]
    public EventCallback<V1Pod> OnPodDelete { get; set; }

    public bool Enable;

    protected override async Task OnInitializedAsync()
    {
        Enable = ConfigService.GetBool("OpenAI","Enable");
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

    private async Task OnPodAnalyzeClick(V1Pod pod)
    {
        var options = PageDrawerService.DefaultOptions($"分析:{pod.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<PodAnalyzeView, V1Pod, bool>(options, pod);
    }
}
