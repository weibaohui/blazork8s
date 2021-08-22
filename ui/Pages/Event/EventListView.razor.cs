using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s.Models;
using Microsoft.AspNetCore.Components;
namespace ui.Pages.Event
{
    public partial class EventListView : ComponentBase
    {
        [Parameter]
        public IList<Corev1Event> Events { get; set; }
        protected override void OnInitialized()
        {
            Events = Events.OrderByDescending(w=>w.Type).OrderByDescending(e => e.LastTimestamp).ToList();
            this.StateHasChanged();
        }
    }
}
