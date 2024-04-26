using System;
using System.Text.RegularExpressions;
using Extension;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class CodeExtract : StepBody
{
    public string Text { get; set; }
    public string Result { get; set; }
    public string Pattern { get; set; }
    public Context Context { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        if (!Text.IsNullOrWhiteSpace() && !Pattern.IsNullOrWhiteSpace())
        {
            // 定义正则表达式模式
            // string pattern = @"```shell(?:[\s\S]*?)```";
            // string pattern = @"kubectl\s+\w+\s+.*?(?=\r?\n)";

            // 使用正则表达式进行匹配
            MatchCollection matches = Regex.Matches(Text, Pattern);

            foreach (Match match in matches)
            {
                foreach (Capture capture in match.Captures)
                {
                    Console.WriteLine($"capture: {capture}");
                    Result += capture.Value.Trim() + ";";
                }
            }

            Console.WriteLine($"CodeExtract {Pattern},final result: {Result}");
        }

        Context.History.Add(Result);
        return ExecutionResult.Next();
    }
}
