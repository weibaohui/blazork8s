using System;
using System.Collections.Generic;

namespace BlazorApp.Utils.Prometheus.Models;

public class Measurement
{
    public Dictionary<string, string> Labels    { get; set; } = new Dictionary<string, string>();
    public double                     Value     { get; set; }
    public DateTimeOffset?            Timestamp { get; set; }
}
