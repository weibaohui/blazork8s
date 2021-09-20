using System.Threading.Tasks;
using AntDesign;
using Blazor.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Pod
{
    public partial class PodDetailView : FeedbackComponent<V1Pod, bool>
    {

        [Inject]
        private INodeService NodeService { get; set; }


        public V1Pod PodItem;

        protected override void OnInitialized()
        {
            PodItem = base.Options;
            base.OnInitialized();
        }
        private async Task OnNodeNameClick(string nodeName)
        {
            await NodeService.ShowNodeDrawer(nodeName);
        }
    }
}
