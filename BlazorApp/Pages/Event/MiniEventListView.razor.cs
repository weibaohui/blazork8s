using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Pages.Workload;
using BlazorApp.Service;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using Extension.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Event
{
    public partial class MiniEventListView : ComponentBase
    {
        [Inject]
        private IEventService EventService { get; set; }

        [Inject]
        private IAiService Ai { get; set; }
        [Inject]
        private IPageDrawerService PageDrawerService { get; set; }
        private IList<Corev1Event> Events { get;            set; }


        [Parameter]
        public string Uid { get; set; }

        [Parameter]
        public string Host { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var coreEventList = EventService.List();
            await base.OnInitializedAsync();

            Events = string.IsNullOrEmpty(Uid)
                ? coreEventList.FilterBySourceHost(Host)
                : coreEventList.FilterByUID(Uid);
        }


        private async Task Ask(Corev1Event e)
        {
            var options = PageDrawerService.DefaultOptions($"AI分析", width: 1000);
            await PageDrawerService.ShowDrawerAsync<AiAnalyzeView, IAiService.AiChatData, bool>(options, new IAiService.AiChatData
            {
                Data  = e,
                Style = "error"
            });
        }

        private async Task AskAll()
        {
            var options = PageDrawerService.DefaultOptions($"AI分析", width: 1000);
            await PageDrawerService.ShowDrawerAsync<AiAnalyzeView, IAiService.AiChatData, bool>(options, new IAiService.AiChatData
            {
                Data  = Events.Where(x => x.Type == "Warning").ToList(),
                Style = "error"
            });
        }
    }
}
