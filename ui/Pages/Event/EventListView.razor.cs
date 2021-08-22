using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using ui.Service;

namespace ui.Pages.Event
{
    public partial class EventListView : ComponentBase
    {
        [Inject]
        private IEventService EventService { get; set; }
       
        public IList<Corev1Event> Events { get; set; }

      
        [Parameter]
        public string Uid { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var coreEventList = await EventService.List();
            if (!string.IsNullOrEmpty(Uid))
            {
                Events = coreEventList.Items.Where(w => w.InvolvedObject.Uid == Uid)
                   .OrderByDescending(w => w.Type).OrderByDescending(e => e.LastTimestamp)
                   .ToList();
            }
        }
    }
}
