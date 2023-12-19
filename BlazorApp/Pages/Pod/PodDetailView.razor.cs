using BlazorApp.Pages.Common;
using k8s.Models;

namespace BlazorApp.Pages.Pod
{
    public partial class PodDetailView : DrawerPageBase<V1Pod>
    {
        public V1Pod Item;

        protected override void OnInitialized()
        {
            Item = base.Options;
            base.OnInitialized();
        }


    }
}
