using System;
using System.Threading.Tasks;
using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Options;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorApp.Diagrams;

public partial class MyDiagram : ComponentBase, IDisposable
{
    // private Timer _timer;
    [Inject] private IJSRuntime JsRuntime { get; set; }
    [Inject] private IDeploymentService DeploymentService { get; set; }
    [Inject] private IReplicaSetService ReplicaSetService { get; set; }
    [Inject] private IPodService PodService { get; set; }
    private BlazorDiagram Diagram { get; set; }


    public void Dispose()
    {
        // _timer.Dispose();
    }

    protected override async Task OnInitializedAsync()
    {
        // _timer = new Timer(5000);
        // _timer.Elapsed += async (sender, eventArgs) => await OnTimerCallback();
        // _timer.Start();

        var options = new BlazorDiagramOptions
        {
            Zoom =
            {
                Enabled = false
            }
        };

        Diagram = new BlazorDiagram(options);

        Diagram.RegisterComponent<KubeNode<V1Deployment>, KubeNodeWidget<V1Deployment>>();
        Diagram.RegisterComponent<KubeNode<V1ReplicaSet>, KubeNodeWidget<V1ReplicaSet>>();
        Diagram.RegisterComponent<KubeNode<V1Pod>, KubeNodeWidget<V1Pod>>();

        await LoadDiagram();

        await base.OnInitializedAsync();
    }

    private async Task OnTimerCallback()
    {
        // await JsRuntime.InvokeVoidAsync("eval", $"window.location.reload(true)");
        await LoadDiagram();
    }

    private async Task LoadDiagram()
    {
        KubeNodeContainer<V1Deployment>.Instance.Clear();
        KubeNodeContainer<V1ReplicaSet>.Instance.Clear();
        KubeNodeContainer<V1Pod>.Instance.Clear();
        Diagram.Nodes.Clear();
        var list = DeploymentService.List();
        var x = 50;
        var offset = 75;
        var column2XBase = 400;
        var column3XBase = 750;
        var y = 50;

        foreach (var deploy in list)
        {
            _ = new KubeNode<V1Deployment>(Diagram, deploy, new Point(x, y));
            var key = $"{deploy.Namespace()}/{deploy.Name()}";
            var deployNode = KubeNodeContainer<V1Deployment>.Instance.Get(key);

            var replicaSets = ReplicaSetService.ListByOwnerUid(deploy.Metadata.Uid);

            for (var n = 0; n < replicaSets.Count; n++)
            {
                var rs = replicaSets[n];
                _ = new KubeNode<V1ReplicaSet>(Diagram, rs, new Point(column2XBase, y));
                var rkey = $"{rs.Namespace()}/{rs.Name()}";
                var rsNode = KubeNodeContainer<V1ReplicaSet>.Instance.Get(rkey);
                DiagramHelper.LinkNodes(Diagram, deployNode, rsNode);

                var pods = PodService.ListByOwnerUid(rs.Metadata.Uid);
                for (var m = 0; m < pods.Count; m++)
                {
                    var pod = pods[m];
                    _ = new KubeNode<V1Pod>(Diagram, pod, new Point(column3XBase, y));
                    var pkey = $"{pod.Namespace()}/{pod.Name()}";
                    var podNode = KubeNodeContainer<V1Pod>.Instance.Get(pkey);
                    DiagramHelper.LinkNodes(Diagram, rsNode, podNode);
                    if (pods.Count > 1 && m != pods.Count - 1)
                        //只有一个就不用往下移位
                        y += offset;
                }

                //只有一个就不用往下移位
                if (replicaSets.Count > 1 && n != replicaSets.Count - 1) y += offset;
            }

            if (list.Count > 1) y += offset;
        }

        await InvokeAsync(StateHasChanged);
    }
}