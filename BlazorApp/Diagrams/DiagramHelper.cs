using Blazor.Diagrams;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Extension;

namespace BlazorApp.Diagrams;

public class DiagramHelper
{
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

    /// <summary>
    /// Link nodes from right to left
    /// Target 《---- Source
    /// </summary>
    /// <param name="diagram"></param>
    /// <param name="target"></param>
    /// <param name="source"></param>
    public static void LinkNodesRight2Left(BlazorDiagram diagram, NodeModel target, NodeModel source)
    {
        var sourcePort = source.GetPort(PortAlignment.Left);
        var targetPort = target.GetPort(PortAlignment.Right);
        if (sourcePort != null && targetPort != null)
            diagram.Links.Add(new LinkModel(sourcePort, targetPort)
            {
                Router = new OrthogonalRouter(),
                PathGenerator = new StraightPathGenerator(5),
                // SourceMarker = LinkMarker.NewArrow(6, 6),
                TargetMarker = LinkMarker.NewArrow(6, 6),
                Color = GetSourceColor(source.Id),
                Width = 1.5
            });
    }

    /// <summary>
    /// Link nodes from right to left
    ///  Source ----》Target
    /// </summary>
    /// <param name="diagram"></param>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public static void LinkNodesLeft2Right(BlazorDiagram diagram, NodeModel source, NodeModel target)
    {
        var sourcePort = source.GetPort(PortAlignment.Right);
        var targetPort = target.GetPort(PortAlignment.Left);
        if (sourcePort != null && targetPort != null)
            diagram.Links.Add(new LinkModel(sourcePort, targetPort)
            {
                Router = new OrthogonalRouter(),
                PathGenerator = new StraightPathGenerator(5),
                // SourceMarker = LinkMarker.NewArrow(6, 6),
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
