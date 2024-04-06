using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.ReplicaSet
{
    public partial class ReplicaSetDetailView : DrawerPageBase<V1ReplicaSet>
    {
        private V1ReplicaSet ReplicaSet { get; set; }

        protected override void OnInitialized()
        {
            ReplicaSet = base.Options;
            base.OnInitialized();
        }
    }
}