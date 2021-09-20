using System;
using AntDesign;
using k8s.Models;

namespace Blazor.Pages.ReplicaSet
{
    public  partial class ReplicaSetDetailView : FeedbackComponent<V1ReplicaSet, bool>
    {
        public V1ReplicaSet RsItem;

        protected override void OnInitialized()
        {
            RsItem = base.Options;
            base.OnInitialized();
        }
    }
}
