using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.TableModels;
using Blazor.Service;
using Blazor.Service.impl;
using Extension;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Pod
{
    public partial class PodIndex : ComponentBase
    {
        [Inject]
        private IPodService PodService { get; set; }

        [Inject]
        private DrawerService DrawerService { get; set; }

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

        public async Task OnChange(QueryModel<V1Pod> queryModel)
        {
            tps.OnChange(queryModel);
            await InvokeAsync(StateHasChanged);
        }

        async Task OnRowClick(RowData<V1Pod> row)
        {
            var options = new DrawerOptions
            {
                Title = "POD:" + row.Data.Name(),
                Width = 800
            };
            var drawerRef =
                await DrawerService.CreateAsync<PodDetailView, V1Pod, bool>(options,
                    row.Data);
        }
    }
}
