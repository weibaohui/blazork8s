using System;
using System.Globalization;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Pages.Common;
using BlazorApp.Pages.Common.Metadata;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.StatefulSet;
public partial class StatefulSetAction : ComponentBase
{
    [Parameter]
    public V1StatefulSet Item { get; set; }
    [Parameter]
    public MenuMode MenuMode { get; set; }=MenuMode.Vertical;

    [Inject]
    ModalService ModalService { get; set; }
    [Inject]
    private IStatefulSetService StatefulSetService { get; set; }
    [Inject]
    private IPageDrawerService PageDrawerService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
    private async Task OnDeleteClick(V1StatefulSet item)
    {
        await StatefulSetService.Delete(item.Namespace(), item.Name());
        StateHasChanged();
    }
    private async Task OnYamlClick(V1StatefulSet item)
    {
        var options = PageDrawerService.DefaultOptions($"Yaml:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<YamlView<V1StatefulSet>, V1StatefulSet, bool>(options, item);
    }
    private async Task OnDocClick(V1StatefulSet item)
    {
        var options = PageDrawerService.DefaultOptions($"Doc:{item.Name()}", width: 1000);
        await PageDrawerService.ShowDrawerAsync<DocTreeView<V1StatefulSet>, V1StatefulSet, bool>(options, item);
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
}
