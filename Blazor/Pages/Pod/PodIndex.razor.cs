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
        int _pageSize  = 5;
        int _total     = 100;
        bool _loading = false;

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("OnInitializedAsync");
            await GetData(selectedNs);

            await base.OnInitializedAsync();
        }
        void ChangePageSize(int pageSize)
        {
            if (_pageSize != pageSize)
            {
                _pageSize = pageSize;
                _pageIndex = 1;
            }
        }

        public async Task GetData(string ns)
        {
            _loading = true;
            var podList = await PodService.ListByNamespace(ns);
            Pods = podList.Items;
            _total = Pods.Count;
            _loading = false;
            await this.InvokeAsync(StateHasChanged);
        }
        public async void OnNsSelectedHandler(string ns)
        {
            selectedNs = ns;
            await GetData(ns);
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
              //.Skip((_pageIndex - 1) * _pageSize)
              //          .Take(_pageSize).ToArray();
            _loading = true;

            Console.WriteLine("OnChange");
            Console.WriteLine(JsonConvert.SerializeObject(queryModel));
            queryModel.SortModel.Where(x => x.Sort != null).ForEach(x=> {
                Console.WriteLine(x.FieldName, x.Sort);
            });
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
           
             _loading = false;
            await this.InvokeAsync(StateHasChanged);
        }
    }
}
