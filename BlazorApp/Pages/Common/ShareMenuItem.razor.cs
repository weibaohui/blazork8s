using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Pages.Workload;
using BlazorApp.Service;
using BlazorApp.Service.AI;
using Extension;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common;

public partial class ShareMenuItem<T> : ComponentBase where T : IKubernetesObject<V1ObjectMeta>
{
    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; } =MenuMode.Vertical;

    [Parameter]
    public T Item { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }


    [Inject]
    private IAiService Ai { get; set; }

    private bool _enable;
    protected override async Task OnInitializedAsync()
    {
        _enable = Ai.Enabled();
        await base.OnInitializedAsync();
    }

    private async Task OnYamlClick(T item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<T>, T, bool>(options, item);
    }

    private async Task OnDocClick(T item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<T>, T, bool>(options, item);
    }

    private async Task OnDescribeClick(T item)
    {
        var type     = typeof(T).Name.GetSubstringAfterFirstDigit().ToLower();
        var command = $"{type} {item.Name()} ";
        if (!item.Namespace().IsNullOrWhiteSpace())
        {
            command = $"{type} {item.Name()} -n {item.Namespace()}";
        }
        var options  = PageDrawerService.DefaultOptions($"Describe:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<KubectlDescribeView, string, bool>(options, command);
    }


    private async Task OnAnalyzeClick(T item)
    {
        var options = PageDrawerService.DefaultOptions($"智能分析:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<AiAnalyzeView, IAiService.AiChatData, bool>(options,
            new IAiService.AiChatData
            {
                Data  =  item,
                Style = "error"
            });
    }

    private async Task OnSecurityClick(T item)
    {
        var options = PageDrawerService.DefaultOptions($"安全分析:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<AiAnalyzeView, IAiService.AiChatData, bool>(options,
            new IAiService.AiChatData
            {
                Data  = item,
                Style = "security"
            });
    }
}
