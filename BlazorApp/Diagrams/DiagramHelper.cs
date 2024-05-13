using Blazor.Diagrams;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Extension;

namespace BlazorApp.Diagrams;

public class DiagramHelper
{
    public static void LinkNodes(BlazorDiagram diagram, NodeModel source, NodeModel target)
    {
        var sourcePort = source.GetPort(PortAlignment.Right);
        var targetPort = target.GetPort(PortAlignment.Left);
        if (sourcePort != null && targetPort != null)
            diagram.Links.Add(new LinkModel(sourcePort, targetPort)
            {
                Router = new OrthogonalRouter(),
                PathGenerator = new StraightPathGenerator(5),
                // SourceMarker = LinkMarker.Square,
                TargetMarker = LinkMarker.NewArrow(6, 6),
                Color = GetSourceColor(source.Id),
                Width = 1.5,
            });
    }

    public static void LinkNodesTwoWay(BlazorDiagram diagram, NodeModel source, NodeModel target)
    {
        var sourcePort = source.GetPort(PortAlignment.Right);
        var targetPort = target.GetPort(PortAlignment.Left);
        if (sourcePort != null && targetPort != null)
            diagram.Links.Add(new LinkModel(sourcePort, targetPort)
            {
                Router = new OrthogonalRouter(),
                PathGenerator = new StraightPathGenerator(5),
                SourceMarker = LinkMarker.NewArrow(6, 6),
                TargetMarker = LinkMarker.NewArrow(6, 6),
                Color = GetSourceColor(source.Id),
                Width = 1.5
            });
    }

    private static string GetSourceColor(string key)
    {
        var colors = new[] { "#ff5722", "#ffb800", "#16baaa", "#1e9fff", "#a233c6", "#2f363c", "#16b777" };
        var index = key.ToNumeric() % colors.Length;
        var randomColor = colors[index];
        return randomColor;
    }
}
