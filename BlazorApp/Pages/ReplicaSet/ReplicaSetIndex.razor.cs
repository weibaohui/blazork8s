using System;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.TableModels;
using BlazorApp.Pages.Common;
using BlazorApp.Service;
using BlazorApp.Service.impl;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ReplicaSet
{
    public partial class ReplicaSetIndex : TableBase<V1ReplicaSet>
    {
        [Inject]
        private IReplicaSetService ReplicaSetService { get; set; }

        [Inject]
        private IPageDrawerService PageDrawerService { get; set; }


        private async Task OnResourceChanged(ResourceCacheHelper<V1ReplicaSet> data)
        {
            ItemList = data;
            TableDataHelper.CopyData(ItemList);
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            TableDataHelper.CopyData(ItemList);
            await InvokeAsync(StateHasChanged);
        }


        private async Task OnRsClick(V1ReplicaSet rs)
        {
            var options = PageDrawerService.DefaultOptions($"{rs.Kind ?? "ReplicaSet"}:{rs.Name()}");
            await PageDrawerService.ShowDrawerAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(options, rs);
        }
    }
}
