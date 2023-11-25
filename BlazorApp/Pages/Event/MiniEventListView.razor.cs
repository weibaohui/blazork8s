using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorApp.Service;
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
        private IOpenAiService OpenAi { get; set; }

        private IList<Corev1Event> Events { get; set; }


        [Parameter]
        public string Uid { get; set; }

        [Parameter]
        public string Host { get; set; }

        private string Advice { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var coreEventList = EventService.List();
            Events = string.IsNullOrEmpty(Uid)
                ? coreEventList.FilterBySourceHost(Host)
                : coreEventList.FilterByUID(Uid);

            if (OpenAi.Enabled())
            {
                if (Events!.Any(x => x.Type == "Warning"))
                {
                    Advice = await OpenAi.ExplainError(JsonSerializer.Serialize(Events));
                }
            }
        }
    }
}