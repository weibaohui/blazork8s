using System;
using System.Collections.Generic;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class TableDataHelper<T> where T : IKubernetesObject<V1ObjectMeta>
{
    private static readonly ILogger<TableDataHelper<T>>      Logger = LoggingHelper<TableDataHelper<T>>.Logger();
    private static          Dictionary<string, TableData<T>> map    = new();
    private static readonly string                           Key    = $"{typeof(T)}";

    public static TableDataHelper<T> Instance => Nested.Instance;

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly TableDataHelper<T> Instance = new TableDataHelper<T>();
    }

    public TableDataHelper()
    {
        Console.WriteLine($"TableData<T> Created,{typeof(T)}");
    }


    public TableData<T> Build()
    {
        if (map.TryGetValue(Key, out var data))
        {
            return data;
        }

        data = new TableData<T>();
        var result = map.TryAdd(Key, data);
        return data;
    }
}
