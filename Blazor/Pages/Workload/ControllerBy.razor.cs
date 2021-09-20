using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Workload
{
    public partial class ControllerBy: ComponentBase
    {
        [Parameter]
        public IList<V1OwnerReference> Owner { get; set; }
        [Inject]
        private IReplicaSetService ReplicaSetService { get; set; }
        [Inject]
        private INodeService NodeService { get; set; }

        private async Task OnRsNameClick(string rsName)
        {
            await ReplicaSetService.ShowReplicaSetDrawer(rsName);
        }
        private async Task OnNodeNameClick(string nodeName)
        {
            await NodeService.ShowNodeDrawer(nodeName);
        }
    }
}
