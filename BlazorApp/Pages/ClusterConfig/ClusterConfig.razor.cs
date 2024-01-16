using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.Charts;
using BlazorApp.Service.k8s;
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
    public  IList<NodeMetrics>       NodeMetricsList { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var podMetricsList = await KubeService.Client().GetKubernetesPodsMetricsAsync();
        PodMetricsList = podMetricsList.Items.ToList();

        var nodeMetricsList = await KubeService.Client().GetKubernetesNodesMetricsAsync();
        NodeMetricsList = nodeMetricsList.Items.ToList();


        var statusList = await KubeService.Client().ListComponentStatusAsync();
        ComponentStatus = statusList.Items;


        var r = new Random();
        datas.ForEach(x =>
        {
            for (var i = 0; i < 10; i++)
            {
                x.trend.Add(new DataValue { index = i.ToString(), value = r.Next(10, 1000) });
            }

            for (var i = 0; i < 20; i++)
            {
                x.times.Add(new DataValue { index = i.ToString(), value = r.Next(200, 400) });
            }

            x.load = r.NextDouble();
        });

        await base.OnInitializedAsync();
    }

    readonly List<DataItem> datas = new List<DataItem>
    {
        new DataItem { id = "local-001" },
        new DataItem { id = "local-002" },
        new DataItem { id = "local-003" },
        new DataItem { id = "local-004" },
        new DataItem { id = "local-005" }
    };

    public class DataItem
    {
        public string          id    { get; set; }
        public List<DataValue> times { get; set; } = new List<DataValue>();
        public List<DataValue> trend { get; set; } = new List<DataValue>();
        public double          load  { get; set; }
    }

    public class DataValue
    {
        public string index { get; set; }
        public int    value { get; set; }
    }

    readonly TinyColumnConfig trendConfig = new TinyColumnConfig
    {
        Width  = 200,
        Height = 50,
        XField = "index",
        YField = "value",
        GuideLine = new GuideLineConfig[]
        {
            new GuideLineConfig()
            {
                Type = "median",
                Text = new GuideLineConfigText()
                {
                    Position = "start",
                    Content  = "中位数",
                    Style = new TextStyle()
                    {
                        Stroke    = "white",
                        LineWidth = 2
                    }
                }
            }
        }
    };

    readonly TinyLineConfig timesConfig = new TinyLineConfig
    {
        Width  = 200,
        Height = 50,
        XField = "index",
        YField = "value",
        GuideLine = new GuideLineConfig[]
        {
            new GuideLineConfig()
            {
                Type = "mean",
                Text = new GuideLineConfigText()
                {
                    Position = "start",
                    Content  = "平均值",
                    Style = new TextStyle()
                    {
                        Stroke    = "white",
                        LineWidth = 2
                    }
                }
            }
        }
    };

    readonly RingProgressConfig loadConfig = new RingProgressConfig
    {
        Width  = 50,
        Height = 50
    };

    object[] data = new object[] {
        new  { year = "1991", value = 3 },
        new  { year = "1992", value = 4 },
        new  { year = "1993", value = 3.5 },
        new  { year = "1994", value = 5 },
        new  { year = "1995", value = 4.9 },
        new  { year = "1996", value = 6 },
        new  { year = "1997", value = 7 },
        new  { year = "1998", value = 9 },
        new  { year = "1999", value = 13 },
    };
    LineConfig config = new LineConfig()
    {
        Title = new Title()
        {
            Visible = true,
            Text    = "曲线折线图",
        },
        Description = new Description()
        {
            Visible = true,
            Text    = "用平滑的曲线代替折线。",
        },
        Padding = "auto",
        AutoFit = true,
        XField  = "year",
        YField  = "value",
        Smooth  = true,
    };
}
