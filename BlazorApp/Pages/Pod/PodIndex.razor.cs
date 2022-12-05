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
            PodService.watchAllPod();
            var timer = new System.Timers.Timer(1000);
            timer.Enabled =  true;
            timer.Elapsed += refreshPods;
        }

        protected async void refreshPods(object? source, System.Timers.ElapsedEventArgs e)
        {
            await tps.GetData(_selectedNs);
            await InvokeAsync(StateHasChanged);
            Console.WriteLine("refreshPods");
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
            var nodeVo  = await NodeService.GetNodeVOWithPodListByNodeName(nodeName);
            var options = PageDrawerService.DefaultOptions($"Node:{nodeName}");
            await PageDrawerService.ShowDrawerAsync<NodeDetailView, NodeVO, bool>(options, nodeVo);
        }

        private async Task OnPodClick(V1Pod pod)
        {
            var options = PageDrawerService.DefaultOptions($"{pod.Kind}:{pod.Name()}");
            await PageDrawerService.ShowDrawerAsync<PodDetailView, V1Pod, bool>(options, pod);
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
