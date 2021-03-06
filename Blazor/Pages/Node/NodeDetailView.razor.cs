using System.Collections.Generic;
using AntDesign;
using Entity;
using k8s.Models;

namespace Blazor.Pages.Node
{
    public partial class NodeDetailView : FeedbackComponent<NodeVO, bool>
    {
        public V1Node       Node;
        public IList<V1Pod> Pods;

        protected override void OnInitialized()
        {
            Node = Options.Node;
            Pods = Options.Pods;
            base.OnInitialized();
        }


        private async void OnClose()
        {
            var drawerRef = FeedbackRef as DrawerRef<bool>;
            await drawerRef!.CloseAsync(true);
        }
    }
}
