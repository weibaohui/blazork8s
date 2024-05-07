using System.Collections.Generic;
using Blazor.Diagrams.Core.Models;
using BlazorApp.Utils;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Diagrams;

public class KubeNodeContainer
{
    private static readonly ILogger<KubeNodeContainer> Logger = LoggingHelper<KubeNodeContainer>.Logger();
    private static readonly IDictionary<string, NodeModel> Map = new Dictionary<string, NodeModel>();

    public static KubeNodeContainer Instance => KubeNodeContainer.Nested.Instance;

    public void Clear()
    {
        Map.Clear();
    }

    public IDictionary<string, NodeModel> AddNode(string key, NodeModel node)
    {
        if (!Map.ContainsKey(key))
        {
            Map.Add(key, node);
        }

        return Map;
    }

    public NodeModel Get(string key)
    {
        return Map.TryGetValue(key, out var nodeModel) ? nodeModel : null;
    }

    private class Nested
    {
        internal static readonly KubeNodeContainer Instance = new KubeNodeContainer();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }
    }
}
