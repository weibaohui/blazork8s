using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Diagrams;

public partial class DeploymentDiagram : DrawerPageBase<V1Deployment>
{
    [Parameter] public V1Deployment Deployment { get; set; }
    [Inject] private IReplicaSetService ReplicaSetService { get; set; }
    [Inject] private IPodService PodService { get; set; }

    private BlazorDiagram Diagram { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Deployment ??= base.Options;
        var options = new BlazorDiagramOptions
        {
            Zoom =
            {
                Enabled = false
            }
        };

        Diagram = new BlazorDiagram(options);


        Diagram.RegisterComponent<KubeNode<V1Deployment>, KubeNodeWidget<V1Deployment>>();
        KubeNodeContainer<V1Deployment>.Instance.Clear();
        KubeNodeContainer<V1ReplicaSet>.Instance.Clear();
        KubeNodeContainer<V1Pod>.Instance.Clear();
        var list = new List<V1Deployment>() { Deployment };
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
            var deployNode = KubeNodeContainer<V1Deployment>.Instance.Get(key);

            var replicaSets = ReplicaSetService.ListByOwnerUid(deploy.Metadata.Uid);

            foreach (var rs in replicaSets)
            {
                _ = new KubeNode<V1ReplicaSet>(Diagram, rs, new Point(column2XBase, y));
                var rkey = $"{rs.Namespace()}/{rs.Name()}";
                var rsNode = KubeNodeContainer<V1ReplicaSet>.Instance.Get(rkey);
                LinkNodes(deployNode, rsNode);

                var pods = PodService.ListByOwnerUid(rs.Metadata.Uid);
                foreach (var pod in pods)
                {
                    _ = new KubeNode<V1Pod>(Diagram, pod, new Point(column3XBase, y));
                    var pkey = $"{pod.Namespace()}/{pod.Name()}";
                    var podNode = KubeNodeContainer<V1Pod>.Instance.Get(pkey);
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
        var sourcePort = source.GetPort(PortAlignment.Right);
        var targetPort = target.GetPort(PortAlignment.Left);
        if (sourcePort != null && targetPort != null)
        {
            Diagram.Links.Add(new LinkModel(sourcePort, targetPort)
            {
                Router = new OrthogonalRouter(),
                PathGenerator = new StraightPathGenerator(),
                // SourceMarker = LinkMarker.Square,
                TargetMarker = LinkMarker.Arrow
            });
        }
    }
}
