using System;
using System.Globalization;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ReplicaSet;

public partial class ReplicaSetAction : ComponentBase
{
    [Parameter]
    public V1ReplicaSet Item { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

    [Inject]
    ModalService ModalService { get; set; }
    [Inject]
    private IReplicaSetService ReplicaSetService { get; set; }


 



    private async Task OnScaleClick(V1ReplicaSet item)
    {
        var options = new ConfirmOptions()
        {
            Title        = "Scale ReplicaSet : " + item.Name(),
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
            await ReplicaSetService.UpdateReplicas(item, Int32.Parse(result.ToString(CultureInfo.CurrentCulture)));
            await InvokeAsync(StateHasChanged);
        };
    }
}
