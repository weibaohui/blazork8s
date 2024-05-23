using BlazorApp.GptWorkflow.Actions;
using BlazorApp.GptWorkflow.Steps;
using BlazorApp.GptWorkflow.Tools;
using WorkflowCore.Interface;

namespace BlazorApp.GptWorkflow.Workflow;

public class FindCodeRunWorkflow : IGptWorkflow<GlobalContext>
{
    public static string Name => "FindCodeRunWorkflow";
    public string Id => "FindCodeRunWorkflow";
    public int Version => 1;

    public void Build(IWorkflowBuilder<GlobalContext> builder)
    {
        var branchShell = builder.CreateBranch()
            .StartWith<CodeExtractor>()
            //找到代码块
            .Input(step => step.GlobalContext, ctx => ctx)
            .Input(step => step.Pattern, ctx => WorkflowConst.RegexPatternShell)
            //提取命令
            .Then<CodeExtractor>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Input(step => step.Pattern, ctx => WorkflowConst.RegexPatternKubectl)
            //运行kubectl
            .Then<KubectlCommandRunner>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Then<End>()
            .Input(step => step.GlobalContext, ctx => ctx);

        var branchYaml = builder.CreateBranch()
            //找到Yaml块
            .Then<CodeExtractor>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Input(step => step.Pattern, ctx => WorkflowConst.RegexPatternYaml)
            //运行kubectl
            .Then<KubectlYamlApplier>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Then<End>()
            .Input(step => step.GlobalContext, ctx => ctx);

        builder
            .StartWith<YamlDetector>()
            .Input(step => step.GlobalContext, ctx => ctx)
            .Decide(data => data.CodeType)
            .Branch(WorkflowConst.CodeType.Shell, branchShell)
            .Branch(WorkflowConst.CodeType.Yaml, branchYaml)
            // .WaitFor(WorkflowConst.BranchRunEnd, ctx => WorkflowConst.BranchRunEnd)
            ;
    }
}
