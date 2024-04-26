using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class PassDetect : StepBody
{
    public Context Context { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine($"Context.UserTask={Context.UserTask}");
        Console.WriteLine($"Context.UserTask={Context.UserTask}");
        Console.WriteLine($"Context.UserTask={Context.UserTask}");
        Console.WriteLine($"Context.UserTask={Context.UserTask}");
        Console.WriteLine($"Context.UserTask={Context.UserTask}");
        Console.WriteLine($"Context.LatestMessage={Context.LatestMessage}");
        var text = Context.LatestMessage;
        if (text.StartsWith("PASS"))
        {
            Context.LatestMessage = "PASS";
            Console.WriteLine($"PASS detect: {text} ====>>>> PASS");
        }

        Context.History.Add(Context.LatestMessage);
        return ExecutionResult.Next();
    }
}
