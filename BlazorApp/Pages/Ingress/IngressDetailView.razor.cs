using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Ingress
{
    public partial class IngressDetailView : FeedbackComponent<V1Ingress, bool>
    {
        private V1Ingress Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}