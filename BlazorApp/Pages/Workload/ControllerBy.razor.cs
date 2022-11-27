using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Deployment;
using BlazorApp.Pages.Node;
using BlazorApp.Pages.ReplicaSet;
using BlazorApp.Service;
using BlazorApp.Service.impl;
using Entity;
using Extension.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Workload
{
    public partial class ControllerBy : ComponentBase
    {
        [Parameter]
        public IList<V1OwnerReference> Owner { get; set; }

        [Parameter]
        public bool ShowOwnerName { get; set; }

        [Inject]
        private IReplicaSetService ReplicaSetService { get; set; }

        [Inject]
        private INodeService NodeService { get; set; }
        [Inject]
        private IPodService PodService { get; set; }
        [Inject]
        private IDeploymentService DeploymentService { get; set; }

        [Inject]
        private IPageDrawerService PageDrawerService { get; set; }

        private async Task OnRsNameClick(string rsName)
        {
            var rs      = await ReplicaSetService.FindByName(rsName);
            var options = PageDrawerService.DefaultOptions("ReplicaSet:" + rs.Name());
            await PageDrawerService.CreateAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(options, rs);
        }

        private async Task OnNodeNameClick(string nodeName)
        {
            var (node, pods) = await NodeService.GetNodeWithPodListByNodeName( nodeName);
            var options          = PageDrawerService.DefaultOptions("Node:" + node.Name());
            await PageDrawerService.CreateAsync<NodeDetailView, NodeVO, bool>(options,
                new NodeVO { Node = node, Pods = pods });
        }

        private async Task OnDeploymentNameClick(string name)
        {
            var deploy  = await DeploymentService.FindByName(name);
            var options = PageDrawerService.DefaultOptions("Deployment:" + deploy.Name());
            await PageDrawerService.CreateAsync<DeploymentDetailView, V1Deployment, bool>(options, deploy);
        }
    }
}
