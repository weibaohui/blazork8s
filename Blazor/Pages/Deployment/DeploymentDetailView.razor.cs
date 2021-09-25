using System;
using AntDesign;
using k8s.Models;

namespace Blazor.Pages.Deployment
{
    public  partial class DeploymentDetailView : FeedbackComponent<V1Deployment, bool>
    {
        public V1Deployment Item;

        protected override void OnInitialized()
        {
            Item = base.Options;
            base.OnInitialized();
        }
    }
}
