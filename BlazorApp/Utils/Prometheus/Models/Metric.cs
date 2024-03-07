using System.Collections.Generic;
using BlazorApp.Utils.Prometheus.Models.Interfaces;
using SqlSugar;

namespace BlazorApp.Utils.Prometheus.Models;

public class Metric : IMetric
{
    [SugarColumn(IsPrimaryKey = true)] //设置主键
    public string            Name         { get; set; }
    public string            Description  { get; set; }
    public string            Cn           { get; set; }
    public MetricTypes       Type         { get; set; }
    public List<Measurement> Measurements { get; set; } = new List<Measurement>();

    public override string ToString()
    {
        return $"{Name} - {Description}";
    }
}
