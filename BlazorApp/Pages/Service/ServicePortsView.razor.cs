using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Utils.PortForwarding;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Service;

public partial class ServicePortsView : ComponentBase
{
    [Inject]
    IMessageService MessageService { get; set; }

    [Parameter]
    public IList<V1ServicePort> Ports { get; set; }


    [Parameter]
    public string SvcNamespace { get; set; }

    [Parameter]
    public string SvcName { get; set; }

    private async Task Forward(V1ServicePort p)
    {
        await PortForwardExecutorHelper.Instance.ForwardPort(PortForwardType.Svc, SvcNamespace, SvcName,
            p.Port.ToString(),
            PortForward.RandomPort());
        await MessageService.Success("设置 转发成功 !");
    }
}
