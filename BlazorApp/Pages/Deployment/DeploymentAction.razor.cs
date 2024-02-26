using System;
using System.Globalization;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Deployment;

public partial class DeploymentAction : ComponentBase
{
    [Inject]
    public IDeploymentService DeploymentService { get; set; }

    [Inject]
    ModalService ModalService { get; set; }
    [Inject]
    IMessageService MessageService { get; set; }

    [Parameter]
    public MenuMode MenuMode { get; set; } = MenuMode.Vertical;

    [Parameter]
    public V1Deployment Item { get; set; }


    [Inject]
    private IAiService Ai { get; set; }

    [Parameter]
    public EventCallback<V1Deployment> OnDeploymentDelete { get; set; }

    public bool Enable;

    protected override async Task OnInitializedAsync()
    {
        Enable = Ai.Enabled();
        await base.OnInitializedAsync();
    }

    private async Task OnDeleteClick(V1Deployment item)
    {
        await DeploymentService.Delete(item.Namespace(), item.Name());
        await OnDeploymentDelete.InvokeAsync(item);
    }

    private async Task OnRestartClick(V1Deployment item)
    {
        await DeploymentService.Restart(item);
        await MessageService.Success($"{item.Name()} Restarted");
    }

    private async Task OnScaleClick(V1Deployment item)
    {
        var options = new ConfirmOptions()
        {
            Title        = "Scale Deployment : " + item.Name(),
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
            await DeploymentService.UpdateReplicas(item, Int32.Parse(result.ToString(CultureInfo.CurrentCulture)));
            await InvokeAsync(StateHasChanged);
        };
    }
}
