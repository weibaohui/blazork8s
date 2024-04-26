namespace BlazorApp.GptWorkflow;

public class CodeExtractPattern
{
    public static string SHELL = @"```shell(?:[\s\S]*?)```";
    public static string YAML = @"```yaml(?:[\s\S]*?)```";
    public static string KUBECTL = @"kubectl\s+\w+\s+.*?(?=\r?\n)";
}
