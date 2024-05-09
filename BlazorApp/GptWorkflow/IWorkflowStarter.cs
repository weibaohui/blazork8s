using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.GptWorkflow.Workflow;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;

namespace BlazorApp.GptWorkflow;

public interface IWorkflowStarter
{
    public Task Start(string userTask, string workflowName, Action<object, Message> eventHandler);
}

public class WorkflowStarter(IAiService ai, IKubectlService kubectl, IWorkflowContainer container) : IWorkflowStarter
{
    public async Task Start(string userTask, string workflowName, Action<object, Message> eventHandler)
    {
        Init();
        var workflowHost = container.Host();
        var ctx = new GlobalContext
        {
            History = new List<string>(),
            UserTask = userTask,
            AiService = ai,
            KubectlService = kubectl,
            Host = workflowHost,
            OutputEventHandler = eventHandler
        };
        await workflowHost.StartWorkflow(workflowName, ctx);
    }

    /// <summary>
    ///     Initializes the workflow container with the workflows
    /// </summary>
    private void Init()
    {
        container.RegisterWorkflow<InspectPodRepairWorkflow>();
        container.RegisterWorkflow<EchoWorkflow>();
    }
}
