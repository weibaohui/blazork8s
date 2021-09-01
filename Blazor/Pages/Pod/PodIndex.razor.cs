using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.TableModels;
using Blazor.Service;
using Extension;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Pod
{
    public partial class PodIndex : ComponentBase
    {
        [Inject]
        private IPodService PodService { get; set; }

        public IList<V1Pod> Pods;
        private IList<V1Pod> _originPods;

        private string _selectedNs = "";

        IEnumerable<V1Pod> selectedRows;
        ITable             table;

        int  _pageIndex = 1;
        int  _pageSize  = 2;
        int  _total     = 100;
        bool _loading   = false;

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("OnInitializedAsync");
            await GetData(_selectedNs);

            await base.OnInitializedAsync();
        }

        void ChangePageSize(int pageSize)
        {
            if (_pageSize != pageSize)
            {
                _pageSize  = pageSize;
                _pageIndex = 1;
            }
        }

        public async Task GetData(string ns)
        {
            _loading = true;
            var podList = await PodService.ListByNamespace(ns);
            _originPods = podList.Items;
            Pods        = _originPods;
            _total      = _originPods.Count;
            _loading    = false;
            await this.InvokeAsync(StateHasChanged);
        }

        public async void OnNsSelectedHandler(string ns)
        {
            _selectedNs = ns;
            await GetData(ns);
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
            _loading = true;
            Console.WriteLine(JsonSerializer.Serialize(queryModel));
            Pods = _originPods.GetPagedTableData(queryModel);
            _loading = false;
            await this.InvokeAsync(StateHasChanged);
        }


    }
}
