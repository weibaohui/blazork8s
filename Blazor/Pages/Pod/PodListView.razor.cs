using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.TableModels;
using Blazor.Service;
using Blazor.Service.impl;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Pod
{
    public partial class PodListView : ComponentBase
    {
        [Inject]
        private IPodService PodService { get; set; }

        [Parameter]
        public IList<V1Pod> Pods { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }


        async Task OnRowClick(RowData<V1Pod> row)
        {
            await PodService.ShowPodDrawer(row.Data);
        }
    }
}