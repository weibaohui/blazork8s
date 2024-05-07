using System.Collections.Generic;
using Blazor.Diagrams.Core.Models;
using BlazorApp.Utils;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Diagrams;

public class KubeNodeContainer<T> : NodeModel where T : IKubernetesObject<V1ObjectMeta>
{
    private static readonly ILogger<KubeNodeContainer<T>> Logger = LoggingHelper<KubeNodeContainer<T>>.Logger();
    private static readonly IDictionary<string, KubeNode<T>> Map = new Dictionary<string, KubeNode<T>>();

    public static KubeNodeContainer<T> Instance => KubeNodeContainer<T>.Nested.Instance;

    public void Clear()
    {
        Map.Clear();
    }

    public IDictionary<string, KubeNode<T>> AddNode(string key, KubeNode<T> node)
    {
        if (!Map.ContainsKey(key))
        {
            Map.Add(key, node);
        }

        return Map;
    }

    public KubeNode<T> Get(string key)
    {
        return Map.TryGetValue(key, out var nodeModel) ? nodeModel : null;
    }

    private class Nested
    {
        internal static readonly KubeNodeContainer<T> Instance = new KubeNodeContainer<T>();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }
    }
}
