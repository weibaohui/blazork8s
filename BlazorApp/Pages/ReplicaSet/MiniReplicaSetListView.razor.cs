using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Extension.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ReplicaSet
{
    public partial class MiniReplicaSetListView : ComponentBase
    {
        [Inject]
        private IReplicaSetService ReplicaSetService { get; set; }

        [Inject]
        private IPodService PodService { get; set; }

        [Inject]
        private DrawerService DrawerService { get; set; }

        private IList<V1Pod>        PodList { get; set; }
        public  IList<V1ReplicaSet> Items   { get; set; }

        [Parameter]
        public string ControllerByUid { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(ControllerByUid))
            {
                Items = ReplicaSetService.ListByOwnerUid(ControllerByUid);
            }

            PodList = PodService.List();
            await base.OnInitializedAsync();
        }


        private async Task OnRsClick(V1ReplicaSet rs)
        {
            await PageDrawerHelper<V1ReplicaSet>.Instance
                .SetDrawerService(DrawerService)
                .ShowDrawerAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(rs);
        }

        private int CountPodsByOwner(string uid)
        {
            return PodList.CountPodsByOwner(uid);
        }
    }
}
