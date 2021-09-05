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

       public  TablePagedService<V1Pod> tps;

        

        private string _selectedNs = "";

        IEnumerable<V1Pod> selectedRows;
 
        protected override async Task OnInitializedAsync()
        {
            tps = new TablePagedService<V1Pod>(PodService);
            await tps.GetData(_selectedNs);

        }


 
        public async void OnNsSelectedHandler(string ns)
        {
            tps.OnNsSelectedHandler(ns);
            await this.InvokeAsync(StateHasChanged);
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
           await tps.OnChange(queryModel);
           await this.InvokeAsync(StateHasChanged);
        }


    }
}
