using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Utils.PortForwarding;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Service
{
    public partial class ServiceDetailView : DrawerPageBase<V1Service>
    {

        [Inject]
        IMessageService MessageService { get; set; }
        private V1Service Item { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item = base.Options;
            await base.OnInitializedAsync();
        }

        private async Task Forward(V1ServicePort p)
        {
            await PortForwardExecutorHelper.Instance.ForwardPort(PortForwardType.Svc,Item.Namespace(), Item.Name(),
                p.Port.ToString(),
                PortForward.RandomPort());
            await MessageService.Success("设置 转发成功 !");
        }
    }
}
