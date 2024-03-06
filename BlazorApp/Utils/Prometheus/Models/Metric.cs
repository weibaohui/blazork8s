using System.Collections.Generic;
using BlazorApp.Utils.Prometheus.Models.Interfaces;

namespace BlazorApp.Utils.Prometheus.Models;

public class Metric : IMetric
{
    public string                 Name         { get; set; }
    public string                 Description  { get; set; }
    public MetricTypes            Type         { get; set; }
    public List<Measurement> Measurements { get; set; } = new List<Measurement>();

    public override string ToString()
    {
        return $"{Name} - {Description}";
    }
}
