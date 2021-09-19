using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.TableModels;
using Blazor.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Pod
{
    public partial class PodIndex : ComponentBase
    {
        [Inject]
        private IPodService PodService { get; set; }

        [Inject]
        private INodeService NodeService { get; set; }


        public TablePagedService<V1Pod> tps;


        private string _selectedNs = "";


        protected override async Task OnInitializedAsync()
        {
            tps = new TablePagedService<V1Pod>(PodService);
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

        private async Task OnChange(QueryModel<V1Pod> queryModel)
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


        private async Task OnNodeNameClick(string nodeName)
        {
            var nodeDetail = await NodeService.DrawerNodeDetail(nodeName);
            await NodeService.ShowNodeDrawer(nodeDetail.node, nodeDetail.pods);
        }

        private async Task OnPodClick(V1Pod pod)
        {
            await PodService.ShowPodDrawer(pod);
        }
    }
}