using System.Collections.Generic;
using WorkflowCore.Interface;

namespace BlazorApp.GptWorkflow;

public class WorkflowContainer(IWorkflowHost workflowHost) : IWorkflowContainer
{
    private readonly List<string> _registeredWorkflows = [];

    public void RegisterWorkflow<TWorkflow>() where TWorkflow : IGptWorkflow<Context>
    {
        var key = typeof(TWorkflow).Name;

        lock (this)
        {
            if (_registeredWorkflows.Contains(key))
            {
                // Console.WriteLine("Added duplicate workflow: " + key);
                return;
            }

            _registeredWorkflows.Add(key);
            workflowHost.RegisterWorkflow<TWorkflow, Context>();
        }
    }

    public IWorkflowHost Host()
    {
        return workflowHost;
    }
}
