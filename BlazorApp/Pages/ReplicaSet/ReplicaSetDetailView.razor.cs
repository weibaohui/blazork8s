using System;
using AntDesign;
using k8s.Models;

namespace  BlazorApp.Pages.ReplicaSet
{
    public  partial class ReplicaSetDetailView : FeedbackComponent<V1ReplicaSet, bool>
    {
        private V1ReplicaSet Item { get; set; }

        protected override void OnInitialized()
        {
            Item = base.Options;
            base.OnInitialized();
        }
    }
}
