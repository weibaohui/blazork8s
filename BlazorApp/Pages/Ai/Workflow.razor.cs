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
        Container.RegisterWorkflow<HelloWorldWorkflow>();
        await base.OnInitializedAsync();
    }

    private async Task Start()
    {
        _ctx.History = new List<string>();
        _ctx.UserTask = "巡查集群运行状态";
        _ctx.Command = "get pods -n default -o wide";
        _ctx.Prompt = "当前巡检命名空间是default.请你作为一个k8s专家.\r\n" +
                      "0、列出有问题的POD名称,请确保pod名称是从给你的内容中提取的，并保证一行一个pod。\r\n" +
                      "1、就每个方面列出最主要的2个问题。\r\n" +
                      "2、详细解释下问题的发生的原因。\r\n" +
                      "3、给出可能得解决方案。\r\n" +
                      "4、请根据解决方案，推断用出修复命令。列出你准备执行的命令，要求命令用```shell ```包裹起来。请针对第0条中列出的POD，形成可以直接执行的、正确的命令\r\n" +
                      "请使用<br>换行.请作答";
        _ctx.AiService = Ai;
        await Container.Host().StartWorkflow(HelloWorldWorkflow.Name, _ctx);
    }


    private async Task OnTimerCallback()
    {
        await InvokeAsync(StateHasChanged);
    }
}
