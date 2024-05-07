using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using k8s;
using k8s.Models;

namespace BlazorApp.Diagrams;

public class KubeNode<T> : NodeModel where T : IKubernetesObject<V1ObjectMeta>
{
    public KubeNode(BlazorDiagram diagram, T item, Point position = null) : base(position)
    {
        Item = item;
        Name = Item.Name();
        Title = Name;
        Diagram = diagram;

        AddPort(PortAlignment.Left);
        AddPort(PortAlignment.Right);
        AddNode(item);
    }

    public T Item { get; set; }
    public string Name { get; set; }
    private BlazorDiagram Diagram { get; set; }

    private void AddNode(T item)
    {
        var key = $"{item.Namespace()}/{item.Name()}";
        KubeNodeContainer.Instance.AddNode(key, this);
        Diagram.Nodes.Add(this);
    }
}
