using System;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.TableModels;
using BlazorApp.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ReplicaSet
{
    public partial class ReplicaSetIndex : ComponentBase
    {
        [Inject]
        private IReplicaSetService ReplicaSetService { get; set; }

        [Inject]
        private IPageDrawerService PageDrawerService { get; set; }

        public TablePagedService<V1ReplicaSet> tps;


        private string _selectedNs = "";


        protected override async Task OnInitializedAsync()
        {
            tps = new TablePagedService<V1ReplicaSet>(ReplicaSetService);
            await tps.GetData(_selectedNs);
        }


        public async Task OnNsSelectedHandler(string ns)
        {
            _selectedNs = ns;
            await tps.OnNsSelectedHandler(ns);
            await InvokeAsync(StateHasChanged);
        }

        public void RemoveSelection(string uid)
        {
            tps.SelectedRows = tps.SelectedRows.Where(x => x.Metadata.Uid != uid);
        }

        private void Delete(string uid)
        {
        }

        private async Task OnChange(QueryModel<V1ReplicaSet> queryModel)
        {
            tps.OnChange(queryModel);
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnSearchHandler(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                await tps.GetData(_selectedNs);
            }
            else
            {
                tps.OnSearch(tps.OriginItems.Where(w => w.Name().Contains(key)).ToList());
            }

            await InvokeAsync(StateHasChanged);
        }


        private async Task OnRsClick(V1ReplicaSet rs)
        {
            var options = PageDrawerService.DefaultOptions($"{rs.Kind ?? "ReplicaSet"}:{rs.Name()}");
            await PageDrawerService.ShowDrawerAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(options, rs);
        }
    }
}
