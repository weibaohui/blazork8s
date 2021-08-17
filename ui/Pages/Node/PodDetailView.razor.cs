using AntDesign;
using k8s.Models;

namespace ui.Pages.Node
{
    public partial class PodDetailView : FeedbackComponent<V1Pod, bool>
    {
        public V1Pod Pod;

        protected override void OnInitialized()
        {
            Pod = base.Options;
            base.OnInitialized();
        }
    }
}
