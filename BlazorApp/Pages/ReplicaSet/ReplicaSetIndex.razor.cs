using System;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.TableModels;
using BlazorApp.Service;
using BlazorApp.Service.impl;
using BlazorApp.Utils;
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

        private TableDataHelper<V1ReplicaSet> tps = new();


        private string _selectedNs = "";


        protected override async Task OnInitializedAsync()
        {
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
            tps.SearchName(key);
            await InvokeAsync(StateHasChanged);
        }


        private async Task OnRsClick(V1ReplicaSet rs)
        {
            var options = PageDrawerService.DefaultOptions($"{rs.Kind ?? "ReplicaSet"}:{rs.Name()}");
            await PageDrawerService.ShowDrawerAsync<ReplicaSetDetailView, V1ReplicaSet, bool>(options, rs);
        }
    }
}