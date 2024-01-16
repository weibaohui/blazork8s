using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class MetricsQueueHolder
{
    private static readonly ILogger<MetricsQueueHolder> Logger = LoggingHelper<MetricsQueueHolder>.Logger();
    private static readonly IDictionary<string, object>     Map    = new Dictionary<string, object>();

    public static MetricsQueueHolder Instance => Nested.Instance;

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly MetricsQueueHolder Instance = new MetricsQueueHolder();
    }

    public IDictionary<string, object> GetMap()
    {
        return Map;
    }
}
