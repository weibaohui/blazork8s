using System.Collections.Generic;

namespace BlazorApp.Utils.Prometheus.Models.Interfaces;

public interface IMetric
{
    public string                 Name         { get; }
    public string                 Description  { get; }
    public MetricTypes            Type         { get; }
    public List<Measurement> Measurements { get; set; }

}
