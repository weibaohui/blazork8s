#nullable enable
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class PodLogHelper
{
    private static readonly ILogger<PodLogHelper>              Logger = LoggingHelper<PodLogHelper>.Logger();
    private static          Dictionary<string, PodLogExecutor> map    = new();

    public static PodLogHelper Instance => Nested.Instance;

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly PodLogHelper Instance = new PodLogHelper();
    }

    public PodLogHelper()
    {

    }


    public PodLogExecutor Create(string ns, string name, string containerName)
    {
        var key = $"{ns}/{name}/{containerName}";
        if (map.TryGetValue(key, out var executor))
        {
            return executor;
        }

        executor = new PodLogExecutor(ns, name, containerName);
        var result = map.TryAdd(key, executor);
        return executor;
    }


}
