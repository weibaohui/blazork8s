using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.PersistentVolumeClaim
{
    public partial class PersistentVolumeClaimDetailView : FeedbackComponent<V1PersistentVolumeClaim, bool>
    {
        private V1PersistentVolumeClaim Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }
    }
}
