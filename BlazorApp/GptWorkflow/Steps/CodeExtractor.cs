using System.Text.RegularExpressions;
using Extension;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Steps;

public class CodeExtractor : StepBody
{
    private const string StepName = "CodeExtract";
    private const string StepDescription = "CodeExtract";

    public string Pattern { get; set; }
    public GlobalContext GlobalContext { get; set; }

    private bool Check()
    {
        return true;
    }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        var msg = Message.NewMessage(GlobalContext, StepName, StepDescription);
        msg.StepParameter.Add("Pattern", Pattern);
        var text = msg.StepInput;
        if (string.IsNullOrWhiteSpace(text))
        {
            return ExecutionResult.Next();
        }

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
                        GlobalContext.Logger.LogDebug("CodeExtractor capture: {Capture}", capture);
                        ret += capture.Value.Trim() + ";";
                    }
                }

                GlobalContext.Logger.LogDebug("CodeExtract {Pattern},final result: {Ret}", Pattern, ret);
                msg.StepResponse = ret;
            }
        }


        return ExecutionResult.Next();
    }
}
