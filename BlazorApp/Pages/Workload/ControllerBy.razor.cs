using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Pages.Deployment;
using BlazorApp.Pages.Node;
using BlazorApp.Pages.ReplicaSet;
using BlazorApp.Service;
using Entity;
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
            var options = PageDrawerService.DefaultOptions($"{rs.Kind ?? "ReplicaSet"}:{rs.Name()}");
            await PageDrawerService.ShowDrawerAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(options, rs);
        }

        private async Task OnNodeNameClick(string nodeName)
        {
            var nodeVo  = await NodeService.GetNodeVOWithPodListByNodeName(nodeName);
            var options = PageDrawerService.DefaultOptions($"Node:{nodeName}");
            await PageDrawerService.ShowDrawerAsync<NodeDetailView, NodeVO, bool>(options, nodeVo);
        }

        private async Task OnDeploymentNameClick(string name)
        {
            var deploy  = await DeploymentService.FindByName(name);
            var options = PageDrawerService.DefaultOptions($"{deploy.Kind ?? "Deployment"}:{deploy.Name()}");

            await PageDrawerService.ShowDrawerAsync<DeploymentDetailView, V1Deployment, bool>(options, deploy);
        }
    }
}
