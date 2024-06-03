namespace BlazorApp.GptWorkflow;

public class WorkflowConst
{
    //CodeType枚举
    public enum CodeType
    {
        Shell,
        Yaml,
        Python
    }

    public const string SubWorkflowEnd = "SubWorkflowEnd";
    public const string BranchRunEnd = "BranchRunEnd";
    public const string RegexPatternShell = @"```shell(?:[\s\S]*?)```";
    public const string RegexPatternYaml = @"```yaml(?:[\s\S]*?)```";
    public const string RegexPatternPython = @"```python(?:[\s\S]*?)```";
    public const string RegexPatternKubectl = @"kubectl\s+\w+\s+.*?(?=\r?\n)";
    public const string DecidePass = "PASS";
    public const string DecideNonePass = "NonePASS";
}
