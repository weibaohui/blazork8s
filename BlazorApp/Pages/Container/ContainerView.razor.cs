using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Utils.PortForwarding;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Container
{
    public partial class ContainerView : ComponentBase
    {
        [Inject]
        IMessageService MessageService { get; set; }

        [Parameter]
        public IList<V1Container> Containers { get; set; }

        [Parameter]
        public IList<V1ContainerStatus> ContainerStatuses { get; set; }


        [Parameter]
        public string Type { get; set; }


        [Parameter]
        public V1Pod Pod { get; set; }


        private async Task Forward(V1ContainerPort p)
        {
            await PortForwardExecutorHelper.Instance.ForwardPort(PortForwardType.Pod,Pod.Namespace(), Pod.Name(),
                p.ContainerPort.ToString(),
                PortForward.RandomPort());
            await MessageService.Success("设置 转发成功 !");
        }
    }
}
