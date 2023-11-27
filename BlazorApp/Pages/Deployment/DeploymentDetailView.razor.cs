using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Deployment
{
    public partial class DeploymentDetailView : FeedbackComponent<V1Deployment, bool>
    {
        private V1Deployment Item { get; set; }

        [Inject]
        private IPodService PodService { get; set; }

        [Inject]
        private IReplicaSetService ReplicaSetService { get; set; }

        private IList<V1Pod> Pods { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Pods = new List<V1Pod>();
            Item = base.Options;

            var rs = ReplicaSetService.ListByOwnerUid(Item.Uid());
            rs.ForEach(r =>
            {
                var pods = PodService.ListByOwnerUid(r.Uid());
                pods.ForEach(p => Pods.Add(p));
            });
            await base.OnInitializedAsync();
        }
    }
}
