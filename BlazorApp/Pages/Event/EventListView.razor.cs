using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Service;
using Extension.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;

namespace  BlazorApp.Pages.Event
{
    public partial class EventListView : ComponentBase
    {
        [Inject]
        private IEventService EventService { get; set; }

        private IList<Corev1Event> Events { get; set; }


        [Parameter]
        public string Uid { get; set; }
        [Parameter]
        public string Host { get; set; }

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
        }
    }
}
