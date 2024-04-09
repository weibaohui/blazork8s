using System;
using System.Globalization;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.StatefulSet;

public partial class StatefulSetAction : PageBase
{
    [Parameter]
    public V1StatefulSet Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

    [Inject]
    IMessageService MessageService { get; set; }
    [Inject]
    ModalService ModalService { get; set; }
    [Inject]
    private IStatefulSetService StatefulSetService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }


    private async Task OnScaleClick(V1StatefulSet item)
    {
        var options = new ConfirmOptions()
        {
            Title        = "Scale StatefulSet : " + item.Name(),
            MaskClosable = true,
            Mask         = true
        };

        var confirmRef =
            await ModalService.CreateConfirmAsync<ReplicaScaleView, double, double>(options,
                item.Spec.Replicas ?? 0);

        confirmRef.OnOpen = () => Task.CompletedTask;

        confirmRef.OnClose = () => Task.CompletedTask;

        confirmRef.OnOk = async (result) =>
        {
            await StatefulSetService.UpdateReplicas(item, Int32.Parse(result.ToString(CultureInfo.CurrentCulture)));
            await InvokeAsync(StateHasChanged);
        };
    }
    private async Task OnRestartClick(V1StatefulSet item)
    {
        await StatefulSetService.Restart(item);
        await MessageService.Success($"{item.Name()} Restarted");
    }
}
