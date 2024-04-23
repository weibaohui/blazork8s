using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using AntDesign.Charts;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Extension;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Common;

public partial class MetricView : PageBase, IDisposable
{
    readonly AreaConfig _areaConfig = new AreaConfig
    {
        Height = 250,
        Width = 600,
        Smooth = true,
        AutoFit = true,
        AreaStyle = new GraphicStyle()
        {
            Fill = "l(270) 0:#ffffff 0.5:#7ec2f3 1:#1890ff",
        },
        Color = RandomColorHelper.RandomColorHexRgb(),
        XField = "index",
        YField = "value",
        XAxis = new ValueCatTimeAxis
        {
            TickCount = 1,
            Range = [0, 1]
        },
        Slider = new Slider
        {
            Start = 0,
            End = 1,
            TrendCfg = new TrendCfg
            {
                IsArea = true
            }
        }
    };

    private ResourceQuantity _lastValue;

    private List<DataValue> _showData = new();

    private Timer _timer;

    private TinyAreaConfig _tinyAreaConfig = new TinyAreaConfig
    {
        Width = 100,
        Height = 20,
        Smooth = true,
        AutoFit = true,
        AreaStyle = new TextStyle()
        {
            Fill = RandomColorHelper.RandomColorHexRgb(),
        },
        Color = RandomColorHelper.RandomColorHexRgb(),
        XField = "index",
        YField = "value",
        XAxis = new Axis
        {
            TickCount = 2,
        },
    };

    [Inject] public IKubeService KubeService { get; set; }

    [Inject] public IMetricsService MetricsService { get; set; }


    /// <summary>
    /// podName、nodeName
    /// </summary>
    [Parameter]
    public string ItemName { get; set; }


    /// <summary>
    /// 需要跟podName、ContainerName一起使用
    /// </summary>
    [Parameter]
    public string ContainerName { get; set; }

    /// <summary>
    /// cpu、memory
    /// </summary>
    [Parameter]
    public string MetricType { get; set; }

    /// <summary>
    /// Pod Node
    /// </summary>
    [Parameter]
    public string ResourceType { get; set; }

    /// <summary>
    /// 是否完整视图，还是只显示mini视图,默认mini视图
    /// </summary>
    [Parameter]
    public bool FullView { get; set; } = false;

    public void Dispose()
    {
        _timer.Dispose();
    }


    protected override async Task OnInitializedAsync()
    {
        _timer = new Timer(1000);
        _timer.Elapsed += (sender, eventArgs) => OnTimerCallback();
        _timer.Start();


        _showData = GetTrendData(ItemName, MetricType, ContainerName);
        await base.OnInitializedAsync();
    }

    private void OnTimerCallback()
    {
        _showData = GetTrendData(ItemName, MetricType, ContainerName);
        InvokeAsync(StateHasChanged);
    }

    private List<DataValue> GetContainerTrendData(string podName, string containerName, string type)
    {
        var queue = MetricsService.PodMetricsQueue(podName);
        var trend = new List<DataValue>();

        var i = 0;
        foreach (var metrics in queue.GetList())
        {
            var metricsList = metrics.Containers.Where(x => x.Name == containerName).SelectMany(x => x.Usage)
                .ToList()
                .Where(x => x.Key == type);

            foreach (var (key, value) in metricsList)
            {
                var item = new DataValue
                {
                    Index = i++,
                    Value = value.HumanizeValue()
                };
                _lastValue = value;
                trend.Add(item);
            }
        }

        return trend;
    }

    private List<DataValue> GetPodTrendData(string podName, string type)
    {
        var queue = MetricsService.PodMetricsQueue(podName);
        var trend = new List<DataValue>();

        var i = 0;
        foreach (var metrics in queue.GetList())
        {
            var metricsList = metrics.Containers.SelectMany(x => x.Usage)
                .ToList()
                .Where(x => x.Key == type);

            foreach (var (key, value) in metricsList)
            {
                var item = new DataValue
                {
                    Index = i++,
                    Value = value.HumanizeValue()
                };
                _lastValue = value;
                trend.Add(item);
            }
        }

        return trend;
    }

    private List<DataValue> GetNodeTrendData(string nodeName, string type)
    {
        var queue = MetricsService.NodeMetricsQueue(nodeName);
        var trend = new List<DataValue>();

        var i = 0;
        foreach (var metrics in queue.GetList())
        {
            var metricsList = metrics.Usage
                .ToList()
                .Where(x => x.Key == type);

            foreach (var (key, value) in metricsList)
            {
                var item = new DataValue
                {
                    Index = i++,
                    Value = value.HumanizeValue()
                };
                _lastValue = value;
                trend.Add(item);
            }
        }

        return trend;
    }

    private List<DataValue> GetTrendData(string itemName, string type, string containerName = null)
    {
        return ResourceType switch
        {
            "Pod" => GetPodTrendData(itemName, type),
            "Node" => GetNodeTrendData(itemName, type),
            "Container" => GetContainerTrendData(itemName, containerName, type),
            _ => null
        };
    }


    public class DataValue
    {
        public int Index { get; set; }
        public decimal Value { get; set; }
    }
}