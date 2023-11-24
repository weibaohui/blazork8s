using System.Collections.Generic;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class ResourceCacheHelper<T> where T : IKubernetesObject<V1ObjectMeta>
{
    private static readonly ILogger<ResourceCacheHelper<T>> Logger = LoggingHelper<ResourceCacheHelper<T>>.Logger();
    private static          Dictionary<string, ResourceCache<T>> map = new();
    private static readonly string key = $"{typeof(T)}";

    public static ResourceCacheHelper<T> Instance => Nested.Instance;

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly ResourceCacheHelper<T> Instance = new ResourceCacheHelper<T>();
    }


    public ResourceCache<T> Build()
    {
        if (map.TryGetValue(key, out var cache))
        {
            return cache;
        }

        cache = new ResourceCache<T>();
        var result = map.TryAdd(key, cache);
        return cache;
    }
}
