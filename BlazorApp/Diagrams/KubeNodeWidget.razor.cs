using System;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using Entity.Crd.Gateway;
using Extension.k8s;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Diagrams;

public partial class KubeNodeWidget<T> : PageBase where T : IKubernetesObject<V1ObjectMeta>
{
    private string _resName;

    private string _typeName;
    [Parameter] public KubeNode<T> Node { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _typeName = Node.Item.GetType().Name;
        _resName = Node.Item.Name();
        await base.OnInitializedAsync();
    }

    private bool? GetReadyStatus(T item)
    {
        Console.WriteLine($"GetReadyStatus for {_typeName} {_resName}");
        return _typeName switch
        {
            "V1Deployment" => (item as V1Deployment)?.IsReady(),
            "V1Service" => true,
            "V1Ingress" => true,
            "V1Pod" => (item as V1Pod)?.IsReady(),
            "V1ReplicaSet" => (item as V1ReplicaSet)?.IsReady(),
            "V1GRPCRoute" => (item as V1GRPCRoute)?.Status?.Parents?.IsReady(),
            "V1HTTPRoute" => (item as V1HTTPRoute)?.Status?.Parents?.IsReady(),
            "V1Alpha2TCPRoute" => (item as V1Alpha2TCPRoute)?.Status?.Parents?.IsReady(),
            "V1Alpha2UDPRoute" => (item as V1Alpha2UDPRoute)?.Status?.Parents?.IsReady(),
            "V1Gateway" => (item as V1Gateway)?.Status?.IsReady(),
            // "V1Job" => (Node.Item as V1Job)?.IsReady(),
            // "V1CronJob" =>( Node.Item as V1CronJob)?.IsReady(),
            // "V1DaemonSet" => (Node.Item as V1DaemonSet)?.IsReady(),
            // "V1StatefulSet" => (Node.Item as V1StatefulSet)?.IsReady(),
            // "V1Node" => (Node.Item as V1Node)?.IsReady(),
            // "V1ReplicationController" => (Node.Item as V1ReplicationController)?.IsReady(),
            _ => false
        };
    }

    private bool? GetProcessingStatus(T item)
    {
        return _typeName switch
        {
            "V1Deployment" => (Node.Item as V1Deployment)?.IsProcessing(),
            "V1Pod" => (Node.Item as V1Pod)?.IsProcessing(),
            "V1ReplicaSet" => (Node.Item as V1ReplicaSet)?.IsProcessing(),
            // "V1Job" => (Node.Item as V1Job)?.IsReady(),
            // "V1CronJob" =>( Node.Item as V1CronJob)?.IsReady(),
            // "V1DaemonSet" => (Node.Item as V1DaemonSet)?.IsReady(),
            // "V1StatefulSet" => (Node.Item as V1StatefulSet)?.IsReady(),
            // "V1Node" => (Node.Item as V1Node)?.IsReady(),
            // "V1ReplicationController" => (Node.Item as V1ReplicationController)?.IsReady(),
            _ => false
        };
    }

    private string GetIcon(string type)
    {
        return type switch
        {
            "V1Deployment" => "hdd",
            "V1Pod" => "appstore",
            "V1ReplicaSet" => "build",
            "V1Job" => "container",
            "V1CronJob" => "schedule",
            "V1DaemonSet" => "reconciliation",
            "V1StatefulSet" => "project",
            "V1ReplicationController" => "split-cells",
            "V1Node" => "database",
            "V1Ingress" => "chrome",
            "V1Service" => "api",
            "V1HTTPRoute" => "codepen-square",
            "V1GRPCRoute" => "google-square",
            "V1Alpha2TCPRoute" => "usb",
            "V1Alpha2UDPRoute" => "alert",
            _ => "hdd"
        };
    }
}
