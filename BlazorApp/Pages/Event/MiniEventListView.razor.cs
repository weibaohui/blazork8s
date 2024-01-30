using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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

        private IList<Corev1Event> Events { get; set; }


        [Parameter]
        public string Uid { get; set; }

        [Parameter]
        public string Host { get; set; }

        private string Advice { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var coreEventList = EventService.List();
            await base.OnInitializedAsync();

            Events = string.IsNullOrEmpty(Uid)
                ? coreEventList.FilterBySourceHost(Host)
                : coreEventList.FilterByUID(Uid);

            if (Ai.Enabled())
            {
                Ai.SetChatEventHandler(EventHandler);

                if (Events != null && Events.Any(x => x.Type == "Warning"))
                {
                    Advice = await Ai.ExplainError(
                        JsonSerializer.Serialize(
                            Events.Where(x => x.Type == "Warning").ToList()
                        )
                    );
                }
            }
        }


        private async void EventHandler(object sender, string resp)
        {
            Advice += resp;
            await InvokeAsync(StateHasChanged);
        }
        private async Task Ask(Corev1Event e)
        {
            Advice = "";
            await InvokeAsync(StateHasChanged);
            Advice = await  Ai.ExplainError(JsonSerializer.Serialize(e));
            await InvokeAsync(StateHasChanged);
        }
    }
}
