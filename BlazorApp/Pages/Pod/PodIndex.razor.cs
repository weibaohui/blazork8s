using System;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.TableModels;
using BlazorApp.Pages.Node;
using BlazorApp.Service;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod
{
    public partial class PodIndex : ComponentBase
    {
        [Inject]
        private IPodService PodService { get; set; }

        [Inject]
        private INodeService NodeService { get; set; }

        [Inject]
        private IPageDrawerService PageDrawerService { get; set; }


        private TablePagedService<V1Pod> tps;


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
            var (node, pods) = await NodeService.GetNodeWithPodListByNodeName( nodeName);
            var options = PageDrawerService.DefaultOptions("Node:" + node.Name());
            await PageDrawerService.CreateAsync<NodeDetailView, NodeVO, bool>(options,
                new NodeVO { Node = node, Pods = pods });
        }

        private async Task OnPodClick(V1Pod pod)
        {
            var options = PageDrawerService.DefaultOptions("POD:" + pod.Name());
            await PageDrawerService.CreateAsync<PodDetailView, V1Pod, bool>(options, pod);
        }

        private async Task PodDeleteHandler(V1Pod pod)
        {
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnPodChanged(string obj)
        {
            await tps.GetData(_selectedNs);
            await InvokeAsync(StateHasChanged);
        }
    }
}
