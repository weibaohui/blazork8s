namespace BlazorApp.GptWorkflow;

public class WorkflowConst
{
    //CodeType枚举
    public enum CodeType
    {
        Shell,
        Yaml
    }

    public const string SubWorkflowEnd = "SubWorkflowEnd";
    public const string BranchRunEnd = "BranchRunEnd";
    public static string RegexPatternShell = @"```shell(?:[\s\S]*?)```";
    public static string RegexPatternYaml = @"```yaml(?:[\s\S]*?)```";
    public static string RegexPatternPython = @"```python(?:[\s\S]*?)```";
    public static string RegexPatternKubectl = @"kubectl\s+\w+\s+.*?(?=\r?\n)";
    public static string DecidePASS = "PASS";
    public static string DecideNonePASS = "NonePASS";
}
