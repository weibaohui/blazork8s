using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorApp.Service;
using Extension.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Event
{
    public partial class EventListView : ComponentBase
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

        public string Advice { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var coreEventList = await EventService.List();
            if (string.IsNullOrEmpty(Uid))
            {
                Events = coreEventList.FilterBySourceHost(Host);
            }
            else
            {
                Events = coreEventList.FilterByUID(Uid);
            }

            if (Events!.Any(x => x.Type == "Warning"))
            {
                Advice = await OpenAi.Explain(JsonSerializer.Serialize(Events));
            }
        }
    }
}
