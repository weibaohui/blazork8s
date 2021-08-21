using System;
using System.Collections.Generic;
using k8s.Models;
using Microsoft.AspNetCore.Components;
namespace ui.Pages.Event
{
    public partial class EventListView : ComponentBase
    {
  
        [Parameter]
        public IList<Corev1Event> Events { get; set; }
    }
}
