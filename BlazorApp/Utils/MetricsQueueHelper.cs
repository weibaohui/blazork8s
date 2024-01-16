using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class MetricsQueueHelper<T>
{
    private static readonly ILogger<MetricsQueueHelper<T>> Logger = LoggingHelper<MetricsQueueHelper<T>>.Logger();


    public static MetricsQueueHelper<T> Instance => Nested.Instance;

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly MetricsQueueHelper<T> Instance = new MetricsQueueHelper<T>();
    }


    public MetricsQueue<T> Build(string key)
    {
        if (MetricsQueueHolder.Instance.GetMap().TryGetValue(key, out var cache))
        {
            return (MetricsQueue<T>)cache;
        }

        cache = new MetricsQueue<T>();
        var result = MetricsQueueHolder.Instance.GetMap().TryAdd(key, cache);
        return (MetricsQueue<T>)cache;
    }
}
