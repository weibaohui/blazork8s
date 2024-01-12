using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class ResourceCacheContainer
{
    private static readonly ILogger<ResourceCacheContainer> Logger = LoggingHelper<ResourceCacheContainer>.Logger();
    private static readonly IDictionary<string, object>      Map    = new Dictionary<string, object>();

    public static ResourceCacheContainer Instance => Nested.Instance;

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly ResourceCacheContainer Instance = new ResourceCacheContainer();
    }

    public IDictionary<string, object> GetMap()
    {
        return Map;
    }
}
