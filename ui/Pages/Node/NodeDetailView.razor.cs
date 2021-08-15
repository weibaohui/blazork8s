using AntDesign;
using k8s.Models;

namespace ui.Pages.Node
{
    public partial class NodeDetailView : FeedbackComponent<V1Node, V1Node>
    {
        public V1Node Node;

        protected override void OnInitialized()
        {
            Node = Options;
            base.OnInitialized();
        }


        private async void OnClose()
        {
            var drawerRef = FeedbackRef as DrawerRef<V1Node>;
            await drawerRef!.CloseAsync(Node);
        }
    }
}
