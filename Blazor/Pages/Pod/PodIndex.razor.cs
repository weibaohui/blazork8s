using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.TableModels;
using Blazor.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace Blazor.Pages.Pod
{
    public partial class PodIndex : ComponentBase
    {
        [Inject]
        private IPodService PodService { get; set; }

        public IList<V1Pod> Pods;

        public string selectedNs = "";

        IEnumerable<V1Pod> selectedRows;
        ITable             table;

        int _pageIndex = 1;
        int _pageSize  = 10;
        int _total     = 100;

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("OnInitializedAsync");
            var podList = await PodService.ListByNamespace(selectedNs);
            Pods   = podList.Items;
            _total = Pods.Count;
            await base.OnInitializedAsync();
        }


        public async void OnNsSelectedHandler(string ns)
        {
            selectedNs = ns;
            var podList = await PodService.ListByNamespace(selectedNs);
            Pods   = podList.Items;
            _total = Pods.Count;

            Console.WriteLine($"POD index receive ns:{ns}");
        }

        public void RemoveSelection(string uid)
        {
            var selected = selectedRows.Where(x => x.Metadata.Uid != uid);
            selectedRows = selected;
        }

        private void Delete(string uid)
        {
        }

        public async Task OnChange(QueryModel<V1Pod> queryModel)
        {
            Console.WriteLine("OnChange");
            Console.WriteLine(JsonConvert.SerializeObject(queryModel.SortModel));
            var next = new Random().Next(10);
            if (next % 2 == 0)
            {
                Console.WriteLine("OnDescending");
                Pods = Pods.OrderByDescending(pod => pod.Metadata.CreationTimestamp).ToList();
            }
            else
            {
                Console.WriteLine("OnAsc");
                Pods = Pods.OrderBy(pod => pod.Metadata.CreationTimestamp).ToList();
            }

            queryModel.SortModel
                .Where(w => !string.IsNullOrEmpty(w.Sort))
                .ForEach(s =>
                {
                    if (s.Sort == "descend")
                    {
                        Console.WriteLine("OnDescending");
                        Pods = Pods.OrderByDescending(pod => pod.Metadata.CreationTimestamp).ToList();
                    }
                    else
                    {
                        Console.WriteLine("OnAsc");
                        Pods = Pods.OrderBy(pod => pod.Metadata.CreationTimestamp).ToList();
                    }
                });
            this.StateHasChanged();
        }
    }
}
