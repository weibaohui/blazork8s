using System;
using System.Text.RegularExpressions;
using Extension;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class CodeExtract : StepBody
{
    public string Pattern { get; set; }
    public Context Context { get; set; }

    private bool Check()
    {
        return true;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var text = Context.LatestMessage;
        if (Check())
        {
            if (!text.IsNullOrWhiteSpace() && !Pattern.IsNullOrWhiteSpace())
            {
                var ret = "";
                // 定义正则表达式模式
                // string pattern = @"```shell(?:[\s\S]*?)```";
                // string pattern = @"kubectl\s+\w+\s+.*?(?=\r?\n)";

                // 使用正则表达式进行匹配
                MatchCollection matches = Regex.Matches(text, Pattern);

                foreach (Match match in matches)
                {
                    foreach (Capture capture in match.Captures)
                    {
                        Console.WriteLine($"capture: {capture}");
                        ret += capture.Value.Trim() + ";";
                    }
                }

                Console.WriteLine($"CodeExtract {Pattern},final result: {ret}");
                Context.LatestMessage = ret;
                Context.History.Add(Context.LatestMessage);
                Context.OutputEventHandler.Invoke(this, Context.LatestMessage);
            }
        }

        return ExecutionResult.Next();
    }
}
