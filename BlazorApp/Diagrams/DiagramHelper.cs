using Blazor.Diagrams;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Extension;

namespace BlazorApp.Diagrams;

public class DiagramHelper
{
    public static void LinkNodesTwoWay(BlazorDiagram diagram, NodeModel source, NodeModel target, string title = "")
    {
        var sourcePort = source.GetPort(PortAlignment.Right);
        var targetPort = target.GetPort(PortAlignment.Left);
        if (sourcePort != null && targetPort != null)
        {
            var spaLink = diagram.Links.Add(new LinkModel(sourcePort, targetPort));
            spaLink.Router = new OrthogonalRouter();
            spaLink.PathGenerator = new StraightPathGenerator(5);
            spaLink.Color = GetSourceColor(source.Id);
            spaLink.Width = 1.5;
            spaLink.SourceMarker = LinkMarker.NewArrow(6, 6);
            spaLink.TargetMarker = LinkMarker.NewArrow(6, 6);
            if (!string.IsNullOrWhiteSpace(title))
            {
                spaLink.Labels.Add(new LinkLabelModel(spaLink, title));
            }
        }
    }

    /// <summary>
    /// Link nodes from right to left
    /// Target 《---- Source
    /// </summary>
    /// <param name="diagram"></param>
    /// <param name="target"></param>
    /// <param name="source"></param>
    /// <param name="title"></param>
    public static void LinkNodesRight2Left(BlazorDiagram diagram, NodeModel target, NodeModel source, string title = "")
    {
        var sourcePort = source.GetPort(PortAlignment.Left);
        var targetPort = target.GetPort(PortAlignment.Right);
        if (sourcePort != null && targetPort != null)
        {
            var spaLink = diagram.Links.Add(new LinkModel(sourcePort, targetPort));
            spaLink.Router = new OrthogonalRouter();
            spaLink.PathGenerator = new StraightPathGenerator(5);
            spaLink.Color = GetSourceColor(source.Id);
            spaLink.Width = 1.5;
            spaLink.TargetMarker = LinkMarker.NewArrow(6, 6);
            if (!string.IsNullOrWhiteSpace(title))
            {
                spaLink.Labels.Add(new LinkLabelModel(spaLink, title));
            }
        }
    }

    /// <summary>
    /// Link nodes from right to left
    ///  Source ----》Target
    /// </summary>
    /// <param name="diagram"></param>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="title"></param>
    public static void LinkNodesLeft2Right(BlazorDiagram diagram, NodeModel source, NodeModel target, string title = "")
    {
        var sourcePort = source.GetPort(PortAlignment.Right);
        var targetPort = target.GetPort(PortAlignment.Left);
        if (sourcePort != null && targetPort != null)
        {
            var spaLink = diagram.Links.Add(new LinkModel(sourcePort, targetPort));
            spaLink.Router = new OrthogonalRouter();
            spaLink.PathGenerator = new StraightPathGenerator(5);
            spaLink.Color = GetSourceColor(source.Id);
            spaLink.Width = 1.5;
            spaLink.TargetMarker = LinkMarker.NewArrow(6, 6);
            if (!string.IsNullOrWhiteSpace(title))
            {
                spaLink.Labels.Add(new LinkLabelModel(spaLink, title));
            }
        }
    }

    private static string GetSourceColor(string key)
    {
        var colors = new[] { "#ff5722", "#ffb800", "#16baaa", "#1e9fff", "#a233c6", "#2f363c", "#16b777" };
        var index = key.ToNumeric() % colors.Length;
        var randomColor = colors[index];
        return randomColor;
    }
}
