using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.Charts;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Extension;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ClusterConfig;

public partial class ClusterConfig : ComponentBase
{
    [Inject]
    public IKubeService KubeService { get; set; }

    private IList<V1ComponentStatus> ComponentStatus { get; set; }
    private IList<PodMetrics>        PodMetricsList  { get; set; }


    protected override async Task OnInitializedAsync()
    {
        var podMetricsList = await KubeService.Client().GetKubernetesPodsMetricsAsync();
        PodMetricsList = podMetricsList.Items.ToList();
        var statusList = await KubeService.Client().ListComponentStatusAsync();
        ComponentStatus = statusList.Items;
        await base.OnInitializedAsync();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="podName"></param>
    /// <param name="type">cpu/memory</param>
    /// <returns></returns>
    private List<DataValue> GetTrendData(string podName,string type)
    {
        var             queue = MetricsQueueHelper<PodMetrics>.Instance.Build(podName);
        List<DataValue> trend = new List<DataValue>();

        var             i     = 0;
        foreach (var metrics in queue.GetList())
        {
            var index = metrics.Timestamp?.ToLocalTime();
            var cpuList = metrics.Containers.SelectMany(x => x.Usage)
                .ToList()
                .Where(x => x.Key == type);

            foreach (var (key, value) in cpuList)
            {
                Console.WriteLine($"[{podName} {type}] {key}= {value.Value} {value.Format}");
                if (type == "memory")
                {
                    Console.WriteLine($"[{podName} {type}] {key}= {value.ToDecimal()/8/1024}MB");
                }
                var item = new DataValue
                {
                    index = i++,
                    value = Convert.ToInt64(value.Value.RemoveStringOfNonDigits())
                };
                trend.Add(item);

            }
        }
        return trend;
    }

    public class DataValue
    {
        public int index { get; set; }
        public Int64      value { get; set; }
    }

    TinyColumnConfig trendConfig = new TinyColumnConfig
    {
        Width  = 100,
        Height = 20,
        XField = "index",
        YField = "value",
    };
    readonly TinyLineConfig timesConfig = new TinyLineConfig
    {
        Width  = 100,
        Height = 20,
        XField = "index",
        YField = "value",
    };
    readonly AreaConfig config1 = new AreaConfig
    {

        XField = "index",
        YField = "value",
        Height = 200,

    };
}
