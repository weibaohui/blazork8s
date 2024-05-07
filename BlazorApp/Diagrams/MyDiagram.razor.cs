using System.Threading.Tasks;
using Blazor.Diagrams;
using Blazor.Diagrams.Core.Anchors;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Diagrams;

public partial class MyDiagram : ComponentBase
{
    private BlazorDiagram Diagram { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var options = new BlazorDiagramOptions
        {
            Zoom =
            {
                Enabled = false
            }
        };

        Diagram = new BlazorDiagram(options);
        var firstNode = Diagram.Nodes.Add(new NodeModel(new Point(50, 50))
        {
            Title = "Node x"
        });
        var secondNode = Diagram.Nodes.Add(new NodeModel(new Point(200, 100))
        {
            Title = "Node y"
        });
        var firstNodeRightPort = firstNode.AddPort(PortAlignment.Right);

        var secondNodeLeftPort = secondNode.AddPort(PortAlignment.Left);
        var secondNodeRightPort = secondNode.AddPort(PortAlignment.Right);
        var sourceAnchor1 = new SinglePortAnchor(firstNodeRightPort);
        var targetAnchor1 = new SinglePortAnchor(secondNodeLeftPort);
        var link = Diagram.Links.Add(new LinkModel(sourceAnchor1, targetAnchor1));

        Diagram.RegisterComponent<AddTwoNumbersNode, AddTwoNumbersWidget>();
        var node = Diagram.Nodes.Add(new AddTwoNumbersNode(new Point(400, 150)));
        node.AddPort(PortAlignment.Left);
        node.AddPort(PortAlignment.Right);

        Diagram.Links.Add(new LinkModel(secondNode.GetPort(PortAlignment.Right), node.GetPort(PortAlignment.Left))
        {
            SourceMarker = LinkMarker.Arrow,
            TargetMarker = LinkMarker.Arrow,
            PathGenerator = new SmoothPathGenerator()
        });


        var node1 = NewNode(50, 50, "node1");
        var node2 = NewNode(300, 300, "node2");
        var node3 = NewNode(300, 50, "node3");
        Diagram.Nodes.Add(new[] { node1, node2, node3 });

        Diagram.Links.Add(new LinkModel(node1.GetPort(PortAlignment.Right), node2.GetPort(PortAlignment.Left))
        {
            SourceMarker = LinkMarker.Arrow,
            TargetMarker = LinkMarker.Arrow,
            PathGenerator = new SmoothPathGenerator()
        });
        Diagram.Links.Add(new LinkModel(node2.GetPort(PortAlignment.Right), node3.GetPort(PortAlignment.Right))
        {
            Router = new OrthogonalRouter(),
            PathGenerator = new StraightPathGenerator(),
            SourceMarker = LinkMarker.Arrow,
            TargetMarker = LinkMarker.Arrow
        });
        await base.OnInitializedAsync();
    }

    protected override void OnInitialized()
    {
    }

    private NodeModel NewNode(double x, double y, string title)
    {
        var node = new NodeModel(new Point(x, y))
        {
            Title = title
        };
        node.AddPort();
        node.AddPort(PortAlignment.Top);
        node.AddPort(PortAlignment.Left);
        node.AddPort(PortAlignment.Right);
        return node;
    }
}
