using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorApp.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages
{
    public partial class Counter : ComponentBase
    {
        [Inject]
        private IKubeService KubeService { get; set; }

        private int currentCount = 0;

        private List<string> podList;
        private async Task IncrementCount()
        {

            currentCount += 1;

        }

        private async Task<List<string>> PodList()
        {
            var list = await KubeService.ListPodByNs();
            list.ForEach(Console.WriteLine);
            podList = list;
            return list;
        }
        //
    }
}
