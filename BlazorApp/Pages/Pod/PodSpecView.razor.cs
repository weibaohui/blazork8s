using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Node;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using BlazorApp.Utils.PortForwarding;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod;

public partial class PodSpecView : PageBase
{
    [Inject]
    IMessageService MessageService { get; set; }

    [Parameter]
    public V1PodSpec PodSpec { get; set; }
    [Inject]
    private INodeService NodeService { get; set; }


    [Parameter]
    public string PodNamespace { get; set; }
    [Parameter]
    public string PodName { get; set; }
    protected override async Task OnInitializedAsync()
    {

        await base.OnInitializedAsync();
    }

    private async Task OnNodeNameClick(string nodeName)
    {
        var node = NodeService.GetByName(nodeName);
        await PageDrawerHelper<V1Node>.Instance
            .SetDrawerService(PageDrawerService.DrawerService)
            .ShowDrawerAsync<NodeDetailView, V1Node, bool>(node);
    }

    public async Task OnPortForward(int port)
    {
        await PortForwardExecutorHelper.Instance.ForwardPort(PortForwardType.Pod, PodNamespace, PodName,
            port.ToString(),
            PortForward.RandomPort());
        await MessageService.Success("设置 转发成功 !");
    }
}
