using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using BlazorApp.GptWorkflow;
using BlazorApp.GptWorkflow.Workflow;
using BlazorApp.Pages.Common;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Ai;

public partial class Workflow : PageBase, IDisposable
{
    private Context _ctx = new Context();
    private Timer _timer;
    [Inject] private IAiService Ai { get; set; }

    [Inject] private IKubectlService Kubectl { get; set; }

    [Inject] private IWorkflowContainer Container { get; set; }

    public void Dispose()
    {
        _timer.Dispose();
    }

    protected override async Task OnInitializedAsync()
    {
        _timer = new Timer(1000);
        _timer.Elapsed += async (sender, eventArgs) => await OnTimerCallback();
        _timer.Start();
        Container.RegisterWorkflow<InspectPodRepairWorkflow>();
        Container.RegisterWorkflow<EchoWorkflow>();
        await base.OnInitializedAsync();
    }

    private async Task Start()
    {
        var workflowHost = Container.Host();

        _ctx.History = new List<string>();
        _ctx.UserTask = "请检查kubernetes-dashboards命名空间下的pod运行状态";
        _ctx.AiService = Ai;
        _ctx.KubectlService = Kubectl;
        _ctx.Host = workflowHost;
        await workflowHost.StartWorkflow(InspectPodRepairWorkflow.Name, _ctx);
    }


    private async Task OnTimerCallback()
    {
        await InvokeAsync(StateHasChanged);
    }
}
