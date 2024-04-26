using WorkflowCore.Interface;

namespace BlazorApp.GptWorkflow;

public interface IWorkflowContainer
{
    void RegisterWorkflow<TWorkflow>() where TWorkflow : IGptWorkflow<Context>;

    IWorkflowHost Host();
}
