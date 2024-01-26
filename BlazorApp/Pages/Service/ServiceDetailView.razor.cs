using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
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
        [Inject]
        private IPodService PodService { get; set; }


        private IList<V1Pod> PodList { get; set; } = new List<V1Pod>();
        protected override async Task OnInitializedAsync()
        {
            Item    = base.Options;
            if (Item?.Spec?.Selector!=null)
            {
                PodList = await PodService.FilterPodByLabels(Item.Namespace(),
                    PodSelectorHelper.ToFilter(Item.Spec.Selector));
            }

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
