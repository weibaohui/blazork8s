using System.Collections.Generic;
using BlazorApp.Utils.Prometheus.Models.Interfaces;

namespace BlazorApp.Utils.Prometheus.Models;

public class Gauge : IMetric
{
    public string                 Name         { get; set; }
    public string                 Description  { get; set; }
    public MetricTypes            Type         => MetricTypes.Gauge;
    public List<GaugeMeasurement> Measurements { get; set; } = new List<GaugeMeasurement>();

    public override string ToString()
    {
        return $"{Name} - {Description}";
    }
}
