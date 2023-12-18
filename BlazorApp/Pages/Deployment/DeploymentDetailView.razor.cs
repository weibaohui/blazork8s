using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Deployment
{
    public partial class DeploymentDetailView : DrawerPageBase<V1Deployment>
    {
        private V1Deployment Item { get; set; }

        [Inject]
        private IPodService PodService { get; set; }

        [Inject]
        private IReplicaSetService ReplicaSetService { get; set; }

        private IList<V1Pod> PodList { get; set; } = new List<V1Pod>();

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;

            var rs = ReplicaSetService.ListByOwnerUid(Item.Uid());
            rs.ForEach(r =>
            {
                var pods = PodService.ListByOwnerUid(r.Uid());
                pods.ForEach(p => PodList.Add(p));
            });
            await base.OnInitializedAsync();
        }
    }
}
