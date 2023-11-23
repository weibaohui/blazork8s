using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using AntDesign.TableModels;
using BlazorApp.Pages.Node;
using BlazorApp.Service;
using BlazorApp.Service.impl;
using BlazorApp.Utils;
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

        [Inject]
        private IWatchService WatchService { get; set; }


        private TablePagedService<V1Pod> _tps = new();


        private string               _selectedNs = "";
        private ResourceCache<V1Pod> _itemList   = ResourceCache<V1Pod>.Instance();

        private async Task OnResourceChanged(ResourceCache<V1Pod> data)
        {
            _itemList = data;
            await _tps.CopyData(_itemList);
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnInitializedAsync()
        {
            await _tps.CopyData(_itemList);
        }


        private async Task OnNsSelectedHandler(string ns)
        {
            _selectedNs = ns;
            await _tps.OnNsSelectedHandler(ns);
            await InvokeAsync(StateHasChanged);
        }

        public void RemoveSelection(string uid)
        {
            _tps.SelectedRows = _tps.SelectedRows.Where(x => x.Metadata.Uid != uid);
        }


        private async Task OnChange(QueryModel<V1Pod> queryModel)
        {
            _tps.OnChange(queryModel);
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnSearchHandler(string key)
        {
            _tps.SearchName(key);

            await InvokeAsync(StateHasChanged);
        }


        private async Task OnNodeNameClick(string nodeName)
        {
            var nodeVo  = await NodeService.GetNodeVOWithPodListByNodeName(nodeName);
            var options = PageDrawerService.DefaultOptions($"Node:{nodeName}");
            await PageDrawerService.ShowDrawerAsync<NodeDetailView, NodeVO, bool>(options, nodeVo);
        }

        private async Task OnPodClick(V1Pod pod)
        {
            var options = PageDrawerService.DefaultOptions($"{pod.Kind ?? "Pod"}:{pod.Name()}");
            await PageDrawerService.ShowDrawerAsync<PodDetailView, V1Pod, bool>(options, pod);
        }

        private async Task PodDeleteHandler(V1Pod pod)
        {
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnPodChanged(string obj)
        {
            await _tps.GetData(_selectedNs);
            await InvokeAsync(StateHasChanged);
        }
    }
}
