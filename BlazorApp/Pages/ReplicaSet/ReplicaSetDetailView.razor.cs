using AntDesign;
using k8s.Models;

namespace  BlazorApp.Pages.ReplicaSet
{
    public  partial class ReplicaSetDetailView : FeedbackComponent<V1ReplicaSet, bool>
    {
        private V1ReplicaSet ReplicaSet { get; set; }

        protected override void OnInitialized()
        {
            ReplicaSet = base.Options;
            base.OnInitialized();
        }
    }
}
