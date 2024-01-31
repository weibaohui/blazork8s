using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using AntDesign.Charts;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Node;

public partial class NodeStatusChart : ComponentBase, IDisposable
{
    [Parameter]
    public V1Node Node { get; set; }

    [Inject]
    public INodeService NodeService { get; set; }

    [Inject]
    public IPodService PodService { get; set; }

    private BulletConfig _uiConfig;
    private Timer        _timer;
    private IList<V1Pod> podsOnNode;

    protected override async Task OnInitializedAsync()
    {
        _timer         =  new Timer(3000);
        _timer.Elapsed += async (sender, eventArgs) => await OnTimerCallback();
        _timer.Start();
        _uiConfig  = GetConfig();
        podsOnNode = PodService.ListByNodeName(Node.Name());
        await base.OnInitializedAsync();
    }

    private async Task OnTimerCallback()
    {
        _uiConfig  = GetConfig();
        podsOnNode = PodService.ListByNodeName(Node.Name());
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        _timer.Dispose();
    }

    private int CalcMeasure(string fullValue)
    {
        var strings = fullValue.Split("/");
        var s1      = strings[0];
        var s2      = strings[1];
        if (string.IsNullOrWhiteSpace(s1))
        {
            s1 = "0";
        }

        if (string.IsNullOrWhiteSpace(s2))
        {
            s1 = "1";
        }

        var avg = Math.Round((double.Parse(s1) / double.Parse(s2)), 2);
        return (int)(avg * 100);
    }

    BulletConfig GetConfig()
    {
        var nodeName       = Node.Name();
        var podCapacity    = NodeService.GetPodCapacity(nodeName);
        var cpuCapacity    = NodeService.GetCpuCapacity(nodeName);
        var memoryCapacity = NodeService.GetMemoryCapacity(nodeName);

        return new BulletConfig
        {
            Width       = 300,
            Height      = 150,
            RangeMax    = 100,
            RangeColors = new string[] { "#9ebc19", "#e18a3b", "#c12c1f" },
            Data = new[]
            {
                new BulletViewConfigData
                {
                    Title    = "Pods (%)",
                    Measures = new int[] { CalcMeasure(podCapacity) },
                    Ranges   = new double[] { 0, 0.5, 0.8, 1 },
                },
                new BulletViewConfigData
                {
                    Title    = "Memory (%)",
                    Measures = new int[] { CalcMeasure(memoryCapacity) },
                    Ranges   = new double[] { 0, 0.5, 0.8, 1 },
                },
                new BulletViewConfigData
                {
                    Title    = "CPU (%)",
                    Measures = new int[] { CalcMeasure(cpuCapacity) },
                    Ranges   = new double[] { 0, 0.5, 0.8, 1 },
                }
            }
        };
    }
}
