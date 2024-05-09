using WorkflowCore.Interface;

namespace BlazorApp.GptWorkflow;

public interface IGptWorkflow : IWorkflow
{
}

public interface IGptWorkflow<TWorkflow> : IWorkflow<GlobalContext>
{
}
