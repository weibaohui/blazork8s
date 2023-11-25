using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Node;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod
{
    public partial class PodDetailView : FeedbackComponent<V1Pod, bool>
    {
        [Inject]
        private INodeService NodeService { get; set; }

        [Inject]
        private IPageDrawerService PageDrawerService { get; set; }

        private V1Pod _podItem;

        protected override void OnInitialized()
        {
            _podItem = base.Options;
            base.OnInitialized();
        }

        private async Task OnNodeNameClick(string nodeName)
        {
            var node    = NodeService.GetByName(nodeName);
            var options = PageDrawerService.DefaultOptions($"Node:{nodeName}");
            await PageDrawerService.ShowDrawerAsync<NodeDetailView, V1Node, bool>(options, node);
        }
    }
}
