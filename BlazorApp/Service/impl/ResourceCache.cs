using System;
using System.Collections.Generic;
using k8s;
using k8s.Models;

namespace BlazorApp.Service.impl;

public class ResourceCache<T> where T : IKubernetesObject<V1ObjectMeta>
{
    private static readonly ResourceCache<T> Rc     = new ResourceCache<T>();
    private readonly        List<T>          _cache = new();
    private                 bool             _listChangedByWatch;

    public static ResourceCache<T> Instance()
    {
        return Rc;
    }

    public Int32 Count()
    {
        return _cache.Count;
    }

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
                    _listChangedByWatch = true;
                }

                break;
            case WatchEventType.Modified:
                if (index > -1)
                {
                    //已存在
                    _cache[index]       = item;
                    _listChangedByWatch = true;
                }

                break;
            case WatchEventType.Deleted:
                if (index > -1)
                {
                    //已存在
                    _cache.RemoveAt(index);
                    _listChangedByWatch = true;
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
        return _listChangedByWatch;
    }
}
