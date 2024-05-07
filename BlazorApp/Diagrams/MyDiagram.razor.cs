using System.Threading.Tasks;
using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Diagrams;

public partial class MyDiagram : ComponentBase
{
    [Inject] private IDeploymentService DeploymentService { get; set; }
    [Inject] private IReplicaSetService ReplicaSetService { get; set; }
    [Inject] private IPodService PodService { get; set; }

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


        Diagram.RegisterComponent<KubeNode<V1Deployment>, KubeNodeWidget<V1Deployment>>();
        KubeNodeContainer.Instance.Clear();
        var list = DeploymentService.List();
        var x = 50;
        var offset = 30;
        var column2XBase = 250;
        var column3XBase = 450;
        var y = 0;

        foreach (var deploy in list)
        {
            y += offset;
            _ = new KubeNode<V1Deployment>(Diagram, deploy, new Point(x, y));
            var key = $"{deploy.Namespace()}/{deploy.Name()}";
            var deployNode = KubeNodeContainer.Instance.Get(key);

            var replicaSets = ReplicaSetService.ListByOwnerUid(deploy.Metadata.Uid);

            foreach (var rs in replicaSets)
            {
                _ = new KubeNode<V1ReplicaSet>(Diagram, rs, new Point(column2XBase, y));
                var rkey = $"{rs.Namespace()}/{rs.Name()}";
                var rsNode = KubeNodeContainer.Instance.Get(rkey);
                LinkNodes(deployNode, rsNode);

                var pods = PodService.ListByOwnerUid(rs.Metadata.Uid);
                foreach (var pod in pods)
                {
                    _ = new KubeNode<V1Pod>(Diagram, pod, new Point(column3XBase, y));
                    var pkey = $"{pod.Namespace()}/{pod.Name()}";
                    var podNode = KubeNodeContainer.Instance.Get(pkey);
                    LinkNodes(rsNode, podNode);
                    y += offset * 2;
                }

                y += offset * 2;
            }
        }


        await base.OnInitializedAsync();
    }


    private void LinkNodes(NodeModel source, NodeModel target)
    {
        Diagram.Links.Add(new LinkModel(source.GetPort(PortAlignment.Right), target.GetPort(PortAlignment.Left))
        {
            Router = new OrthogonalRouter(),
            PathGenerator = new StraightPathGenerator(),
            // SourceMarker = LinkMarker.Square,
            TargetMarker = LinkMarker.Arrow
        });
    }
}
