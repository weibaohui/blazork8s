using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Diagrams;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod;

public partial class PodAction : PageBase
{
    [Parameter] public V1Pod PodItem { get; set; }

    [Parameter] public MenuMode MenuMode { get; set; } = MenuMode.Vertical;

    [Inject] private IPodService PodService { get; set; }


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

    private async Task OnDiagramClick(V1Pod item)
    {
        var options = PageDrawerService.DefaultOptions($"{L["Diagram"]}:{item.Name()}", 1300);
        var x = await PageDrawerService.ShowDrawerAsync<PodDiagram, V1Pod, bool>(options, item);
    }
}
