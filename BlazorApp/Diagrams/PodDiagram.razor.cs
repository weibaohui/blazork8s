using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Options;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorApp.Diagrams;

/// <summary>
/// 以POD为中心，进行图形展示。
/// 左边是Pod的来源，右边是Pod的网络连接关系。
/// </summary>
public partial class PodDiagram : ComponentBase
{
    [Inject] private IJSRuntime JsRuntime { get; set; }
    [Inject] private IDeploymentService DeploymentService { get; set; }
    [Inject] private IStatefulSetService StatefulSetService { get; set; }
    [Inject] private IDaemonSetService DaemonSetService { get; set; }
    [Inject] private INodeService NodeService { get; set; }
    [Inject] private IReplicationControllerService ReplicationControllerService { get; set; }
    [Inject] private IReplicaSetService ReplicaSetService { get; set; }
    [Inject] private IJobService JobService { get; set; }
    [Inject] private ICronJobService CronJobService { get; set; }
    [Inject] private IPodService PodService { get; set; }
    [Inject] private IServiceService ServiceService { get; set; }
    [Inject] private IIngressService IngressService { get; set; }

    /// <summary>
    /// PodName是一个非全称名称，可能是部分名称，比如"web"，"api"等。
    /// </summary>
    [Parameter]
    public string PodName { get; set; }

    private IList<V1Pod> Pods { get; set; }
    private BlazorDiagram Diagram { get; set; }


    protected override async Task OnInitializedAsync()
    {
        Pods = PodService.List().Where(p => p.Name().Contains(PodName))
            .OrderByDescending(x => x.OwnerReferences()?.FirstOrDefault()?.Kind).ToList();

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
        Diagram.RegisterComponent<KubeNode<V1Job>, KubeNodeWidget<V1Job>>();
        Diagram.RegisterComponent<KubeNode<V1StatefulSet>, KubeNodeWidget<V1StatefulSet>>();
        Diagram.RegisterComponent<KubeNode<V1DaemonSet>, KubeNodeWidget<V1DaemonSet>>();
        Diagram.RegisterComponent<KubeNode<V1ReplicationController>, KubeNodeWidget<V1ReplicationController>>();
        Diagram.RegisterComponent<KubeNode<V1Node>, KubeNodeWidget<V1Node>>();
        Diagram.RegisterComponent<KubeNode<V1Ingress>, KubeNodeWidget<V1Ingress>>();
        Diagram.RegisterComponent<KubeNode<V1Service>, KubeNodeWidget<V1Service>>();
        Diagram.RegisterComponent<KubeNode<V1CronJob>, KubeNodeWidget<V1CronJob>>();

        await LoadDiagram();

        await base.OnInitializedAsync();
    }


    private async Task LoadDiagram()
    {
        KubeNodeContainer<V1Deployment>.Instance.Clear();
        KubeNodeContainer<V1ReplicaSet>.Instance.Clear();
        KubeNodeContainer<V1Job>.Instance.Clear();
        KubeNodeContainer<V1CronJob>.Instance.Clear();
        KubeNodeContainer<V1Pod>.Instance.Clear();
        KubeNodeContainer<V1StatefulSet>.Instance.Clear();
        KubeNodeContainer<V1DaemonSet>.Instance.Clear();
        KubeNodeContainer<V1ReplicationController>.Instance.Clear();
        KubeNodeContainer<V1Node>.Instance.Clear();
        KubeNodeContainer<V1Service>.Instance.Clear();
        KubeNodeContainer<V1Ingress>.Instance.Clear();
        Diagram.Nodes.Clear();


        var offset = 75;
        var columnXBase = 50;
        var column2XBase = 400;
        var column3XBase = 750; //中央位置
        var column4XBase = 1100;
        var column5XBase = 1450;
        var y = 50;

        //1.Pod在中央，左边1是来源rs，左边2是deploy
        //先按controllerBy 查找所有相关的rs
        if (Pods is not { Count: > 0 })
        {
            return;
        }

        foreach (var pod in Pods)
        {
            var owner = pod.GetController();

            _ = new KubeNode<V1Pod>(Diagram, pod, new Point(column3XBase, y));
            var pkey = $"{pod.Namespace()}/{pod.Name()}";
            var podNode = KubeNodeContainer<V1Pod>.Instance.Get(pkey);

            if (owner == null)
            {
                //看不到controllerBy，可能是因为没有controllerRef
                //通过k8s规则来找到owner
                if (pod.Labels().ContainsKey("controller-uid") && pod.Labels().ContainsKey("job-name"))
                {
                    //可能是job
                    var jobName = pod.Labels()["job-name"];
                    var jobUid = pod.Labels()["controller-uid"];
                    var job = JobService.GetByUid(jobUid);
                    if (job != null)
                    {
                        //找到Job
                        _ = new KubeNode<V1Job>(Diagram, job, new Point(column2XBase, y));
                        var key = $"{job.Namespace()}/{job.Name()}";
                        var node = KubeNodeContainer<V1Job>.Instance.Get(key);
                        DiagramHelper.LinkNodes(Diagram, node, podNode);
                    }
                }
            }

            if (owner != null)
            {
                var uid = owner.Uid;
                var kind = owner.Kind;
                switch (kind)
                {
                    case "StatefulSet":
                        var sts = StatefulSetService.GetByUid(uid);
                        if (sts != null)
                        {
                            _ = new KubeNode<V1StatefulSet>(Diagram, sts, new Point(column2XBase, y));
                            var key = $"{sts.Namespace()}/{sts.Name()}";
                            var stsNode = KubeNodeContainer<V1StatefulSet>.Instance.Get(key);
                            DiagramHelper.LinkNodes(Diagram, stsNode, podNode);
                        }

                        break;
                    case "DaemonSet":
                        var ds = DaemonSetService.GetByUid(uid);
                        if (ds != null)
                        {
                            var key = $"{ds.Namespace()}/{ds.Name()}";
                            var dsNode = KubeNodeContainer<V1DaemonSet>.Instance.Get(key);
                            if (dsNode == null)
                            {
                                _ = new KubeNode<V1DaemonSet>(Diagram, ds, new Point(column2XBase, y));
                                dsNode = KubeNodeContainer<V1DaemonSet>.Instance.Get(key);
                            }

                            DiagramHelper.LinkNodes(Diagram, dsNode, podNode);
                        }

                        break;
                    case "ReplicationController":
                        var rc = ReplicationControllerService.GetByUid(uid);
                        if (rc != null)
                        {
                            _ = new KubeNode<V1ReplicationController>(Diagram, rc, new Point(column2XBase, y));
                            var key = $"{rc.Namespace()}/{rc.Name()}";
                            var rcNode = KubeNodeContainer<V1ReplicationController>.Instance.Get(key);
                            DiagramHelper.LinkNodes(Diagram, rcNode, podNode);
                        }

                        break;
                    case "Node":
                        var node = NodeService.GetByUid(uid);
                        if (node != null)
                        {
                            var key = $"{node.Namespace()}/{node.Name()}";
                            var nodeNode = KubeNodeContainer<V1Node>.Instance.Get(key);
                            if (nodeNode == null)
                            {
                                _ = new KubeNode<V1Node>(Diagram, node, new Point(column2XBase, y));
                                nodeNode = KubeNodeContainer<V1Node>.Instance.Get(key);
                            }

                            DiagramHelper.LinkNodes(Diagram, nodeNode, podNode);
                        }

                        break;
                    case "ReplicaSet":
                        var rs = ReplicaSetService.GetByUid(uid);
                        if (rs != null)
                        {
                            var key = $"{rs.Namespace()}/{rs.Name()}";
                            var rsNode = KubeNodeContainer<V1ReplicaSet>.Instance.Get(key);
                            if (rsNode == null)
                            {
                                _ = new KubeNode<V1ReplicaSet>(Diagram, rs, new Point(column2XBase, y));
                                rsNode = KubeNodeContainer<V1ReplicaSet>.Instance.Get(key);
                            }

                            DiagramHelper.LinkNodes(Diagram, rsNode, podNode);

                            //rs上一层是deploy
                            var ownerOwner = rs.GetController();
                            if (ownerOwner != null)
                            {
                                var ownerOwnerUid = ownerOwner.Uid;
                                var deploy = DeploymentService.GetByUid(ownerOwnerUid);
                                if (deploy != null)
                                {
                                    var deployKey = $"{deploy.Namespace()}/{deploy.Name()}";
                                    var deployNode = KubeNodeContainer<V1Deployment>.Instance.Get(deployKey);
                                    if (deployNode == null)
                                    {
                                        _ = new KubeNode<V1Deployment>(Diagram, deploy, new Point(columnXBase, y));
                                        deployNode = KubeNodeContainer<V1Deployment>.Instance.Get(deployKey);
                                        DiagramHelper.LinkNodes(Diagram, deployNode, rsNode);
                                    }
                                }
                            }
                        }

                        break;
                    case "Job":
                        var job = JobService.GetByUid(uid);
                        if (job != null)
                        {
                            _ = new KubeNode<V1Job>(Diagram, job, new Point(column2XBase, y));
                            var key = $"{job.Namespace()}/{job.Name()}";
                            var jobNode = KubeNodeContainer<V1Job>.Instance.Get(key);
                            DiagramHelper.LinkNodes(Diagram, jobNode, podNode);

                            //job 上一层是CronJob
                            var ownerOwner = job.GetController();
                            if (ownerOwner != null)
                            {
                                var ownerOwnerUid = ownerOwner.Uid;
                                var cronJob = CronJobService.GetByUid(ownerOwnerUid);

                                if (cronJob != null)
                                {
                                    var cronJobKey = $"{cronJob.Namespace()}/{cronJob.Name()}";
                                    var cronJobNode = KubeNodeContainer<V1CronJob>.Instance.Get(cronJobKey);
                                    if (cronJobNode == null)
                                    {
                                        _ = new KubeNode<V1CronJob>(Diagram, cronJob, new Point(columnXBase, y));
                                        cronJobNode = KubeNodeContainer<V1CronJob>.Instance.Get(cronJobKey);
                                        DiagramHelper.LinkNodes(Diagram, cronJobNode, jobNode);
                                    }
                                }
                            }
                        }

                        break;
                }
            }

            //寻找Service
            var labels = pod.Metadata.Labels;
            if (labels is { Count: > 0 })
            {
                var svcList = ServiceService.ListByLabels(labels);
                foreach (var svc in svcList)
                {
                    var key = $"{svc.Namespace()}/{svc.Name()}";
                    var svcNode = KubeNodeContainer<V1Service>.Instance.Get(key);
                    if (svcNode == null)
                    {
                        _ = new KubeNode<V1Service>(Diagram, svc, new Point(column4XBase, y));
                        svcNode = KubeNodeContainer<V1Service>.Instance.Get(key);
                        if (svcList.Count > 1) y += offset;
                    }

                    DiagramHelper.LinkNodesTwoWay(Diagram, podNode, svcNode);


                    //寻找Service后面的Ingress
                    var ingList = IngressService.ListByServiceList(new List<V1Service> { svc });
                    foreach (var ing in ingList)
                    {
                        var ingKey = $"{ing.Namespace()}/{ing.Name()}";
                        var ingNode = KubeNodeContainer<V1Ingress>.Instance.Get(ingKey);
                        if (ingNode == null)
                        {
                            _ = new KubeNode<V1Ingress>(Diagram, ing, new Point(column5XBase, y));
                            ingNode = KubeNodeContainer<V1Ingress>.Instance.Get(ingKey);
                            if (ingList.Count > 1) y += offset;
                        }

                        DiagramHelper.LinkNodesTwoWay(Diagram, svcNode, ingNode);
                    }
                }
            }

            if (Pods.Count > 1) y += offset;
        }


        await InvokeAsync(StateHasChanged);
    }
}
