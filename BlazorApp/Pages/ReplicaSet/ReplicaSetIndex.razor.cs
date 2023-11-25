using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ReplicaSet
{
    public partial class ReplicaSetIndex : TableBase<V1ReplicaSet>
    {
        [Inject]
        private IReplicaSetService ReplicaSetService { get; set; }


        private async Task OnResourceChanged(ResourceCache<V1ReplicaSet> data)
        {
            ItemList = data;
            TableData.CopyData(ItemList);
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            TableData.CopyData(ItemList);
            await InvokeAsync(StateHasChanged);
        }


        private async Task OnRsClick(V1ReplicaSet rs)
        {
            var options = PageDrawerService.DefaultOptions($"{rs.Kind ?? "ReplicaSet"}:{rs.Name()}");
            await PageDrawerService.ShowDrawerAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(options, rs);
        }
    }
}