using AntDesign;
using k8s.Models;

namespace ui.Pages.Pod
{
    public partial class PodDetailView : FeedbackComponent<V1Pod, bool>
    {
        public V1Pod PodItem;

        protected override void OnInitialized()
        {
            PodItem = base.Options;
            base.OnInitialized();
        }
    }
}
