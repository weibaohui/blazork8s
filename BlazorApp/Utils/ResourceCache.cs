using System;
using System.Collections.Generic;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class ResourceCache<T> where T : IKubernetesObject<V1ObjectMeta>
{
    private readonly        List<T>                         _cache = new();
    private                 bool                            _changed;
    private static readonly ILogger<ResourceCacheHelper<T>> Logger = LoggingHelper<ResourceCacheHelper<T>>.Logger();

    public Int32 Count => _cache.Count;

    public List<T> Get()
    {
        return _cache;
    }


    public void Update(WatchEventType type, T item)
    {
        var index = _cache.FindIndex(0, r => r.Uid() == item.Uid());
        // Console.WriteLine($"{index}:{item.Name()}");
        switch (type)
        {
            case WatchEventType.Added:
                if (index == -1)
                {
                    //不存在
                    _cache.Insert(0, item);
                    _changed = true;
                }

                break;
            case WatchEventType.Modified:
                if (index > -1)
                {
                    //已存在
                    _cache[index] = item;
                    _changed      = true;
                }

                break;
            case WatchEventType.Deleted:
                if (index > -1)
                {
                    //已存在
                    _cache.RemoveAt(index);
                    _changed = true;
                }

                break;
            case WatchEventType.Error:
                break;
            case WatchEventType.Bookmark:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public bool Changed()
    {
        return _changed;
    }
}
