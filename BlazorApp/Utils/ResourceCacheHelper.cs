using System.Collections.Generic;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class ResourceCacheHelper<T> where T : IKubernetesObject<V1ObjectMeta>
{
    private static readonly ILogger<ResourceCacheHelper<T>> Logger = LoggingHelper<ResourceCacheHelper<T>>.Logger();

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
        if (ResourceCacheContainer.Instance.GetMap().TryGetValue(key, out var cache))
        {
            return (ResourceCache<T>)cache;
        }

        cache = new ResourceCache<T>();
        var result = ResourceCacheContainer.Instance.GetMap().TryAdd(key, cache);
        return (ResourceCache<T>)cache;
    }
}
