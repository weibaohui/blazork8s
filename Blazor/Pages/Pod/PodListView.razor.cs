using System;
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


        [Parameter]
        public string ControllerByUid { get; set; }


        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(ControllerByUid))
            {
                Pods = await PodService.ListByOwnerUid(ControllerByUid);
            }

            await base.OnInitializedAsync();
        }


        async Task OnPodNameClick(V1Pod pod)
        {
            await PodService.ShowPodDrawer(pod);
        }
    }
}
