using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using Blazor.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Deployment
{
    public partial class DeploymentDetailView : FeedbackComponent<V1Deployment, bool>
    {
        public V1Deployment Item;

        [Inject]
        private IPodService PodService { get; set; }

        [Inject]
        private IReplicaSetService ReplicaSetService { get; set; }

        public IList<V1Pod> Pods  { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Pods = new List<V1Pod>();
            Item = base.Options;

            var rs = await ReplicaSetService.ListByOwnerUid(Item.Uid());
            rs.ForEach(async r =>
            {
                var pods = await PodService.ListByOwnerUid(r.Uid());
                pods.ForEach(p => Pods.Add(p));
            });
            await base.OnInitializedAsync();
        }
    }
}
