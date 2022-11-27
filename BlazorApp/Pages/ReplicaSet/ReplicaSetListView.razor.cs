using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.TableModels;
using  BlazorApp.Service;
using  BlazorApp.Service.impl;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace  BlazorApp.Pages.ReplicaSet
{
    public  partial class ReplicaSetListView :ComponentBase
    {

        [Inject]
        private IReplicaSetService ReplicaSetService { get; set; }
        [Inject]
        private IPodService PodService { get; set; }
        [Inject]
        private IPageDrawerService PageDrawerService { get; set; }

        private V1PodList           PodList { get;          set; }
        public  IList<V1ReplicaSet> Items   { get;          set; }
        [Parameter]
        public string ControllerByUid { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(ControllerByUid))
            {
                Items = await ReplicaSetService.ListByOwnerUid(ControllerByUid);
            }
            PodList = await PodService.List();
            await base.OnInitializedAsync();
        }


        async Task OnRowClick(RowData<V1ReplicaSet> row)
        {
            var rs      = row.Data;
            var options = PageDrawerService.DefaultOptions($"{rs.Kind}:{rs.Name()}");
            await PageDrawerService.ShowDrawerAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(options, rs);
        }

        int CountPodsByOwner(string uid)
        {
            return PodList.Items
                .Count(x => x.GetController() != null && x.GetController().Uid == uid);
        }
    }
}
