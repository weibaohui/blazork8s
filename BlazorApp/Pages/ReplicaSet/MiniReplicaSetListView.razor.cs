using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.TableModels;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
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


        async Task OnRowClick(RowData<V1ReplicaSet> row)
        {
            var rs      = row.Data;
            await PageDrawerHelper<V1Node>.Instance
                .SetDrawerService(DrawerService)
                .ShowDrawerAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(rs);
        }

        int CountPodsByOwner(string uid)
        {
            return PodList
                .Count(x => x.GetController() != null && x.GetController().Uid == uid);
        }
    }
}
