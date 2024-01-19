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

namespace BlazorApp.Pages.ClusterConfig;

public partial class MetricView : ComponentBase, IDisposable
{
    [Inject]
    public IKubeService KubeService { get; set; }

    [Parameter]
    public string ItemName { get; set; }

    [Parameter]
    public string MetricName { get; set; }

    private Timer _timer;

    private List<DataValue> _showData = new();

    private string           _lastMetricValue;
    private ResourceQuantity _lastValue;

    protected override async Task OnInitializedAsync()
    {
        _timer         =  new Timer(1000);
        _timer.Elapsed += (sender, eventArgs) => OnTimerCallback();
        _timer.Start();


        _showData = GetTrendData(ItemName, MetricName);
        await base.OnInitializedAsync();
    }

    private void OnTimerCallback()
    {
        _showData = GetTrendData(ItemName, MetricName);
        InvokeAsync(StateHasChanged);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="podName"></param>
    /// <param name="type">cpu/memory</param>
    /// <returns></returns>
    private List<DataValue> GetTrendData(string podName, string type)
    {
        var queue = MetricsQueueHelper<PodMetrics>.Instance.Build(podName);
        var trend = new List<DataValue>();

        var i = 0;
        foreach (var metrics in queue.GetList())
        {
            var index = metrics.Timestamp?.ToLocalTime();
            var cpuList = metrics.Containers.SelectMany(x => x.Usage)
                .ToList()
                .Where(x => x.Key == type);

            foreach (var (key, value) in cpuList)
            {
                var item = new DataValue
                {
                    Index = i++,
                    Value = Convert.ToInt64(value.Value.RemoveStringOfNonDigits())
                };
                _lastValue = value;
                trend.Add(item);
            }
        }

        return trend;
    }

    private string HumanizeValue()
    {
        if (_lastValue.Value.EndsWith("n"))
        {
            return Math.Round(_lastValue.ToDecimal() * 1000, 2) + "m";
        }

        if (_lastValue.Value.EndsWith("Ki"))
        {
            return Math.Round(_lastValue.ToDecimal() / 1024 / 1024, 2) + "Mi";
        }

        return _lastValue.CanonicalizeString(ResourceQuantity.SuffixFormat.BinarySI);
    }

    public class DataValue
    {
        public int  Index { get; set; }
        public long Value { get; set; }
    }

    private TinyAreaConfig _tinyAreaConfig = new TinyAreaConfig
    {
        Width   = 100,
        Height  = 20,
        Smooth  = true,
        AutoFit = true,
        AreaStyle = new TextStyle()
        {
            Fill = "#d6e3fd",
        },
        XField = "index",
        YField = "Value",
    };

    public void Dispose()
    {
        _timer.Dispose();
    }
}
