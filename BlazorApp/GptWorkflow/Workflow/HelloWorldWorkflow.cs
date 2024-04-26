using BlazorApp.GptWorkflow.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BlazorApp.GptWorkflow.Workflow;

public class HelloWorldWorkflow : IGptWorkflow<Context>
{
    public static string Name => "HelloWorld";
    public string Id => "HelloWorld";
    public int Version => 1;

    public void Build(IWorkflowBuilder<Context> builder)
    {
        var text = """
                   四、修复命令示例:
                    kubectl get nodes 也是个实例。
                      ```shell
                      # 重新拉取镜像
                      kubectl delete pod 2048-game-6dfc5bd7b8-dq7dt
                      kubectl delete pod 2048-game-7665455675-j5ft7
                      kubectl delete pod not-ready-nginx-5c79994b84-z5qxc
                   
                      # 查看容器日志
                      kubectl logs crash-nginx-84945cfd5c-6c6zf
                      kubectl logs crash-nginx-84945cfd5c-7b48s
                      kubectl logs crash-nginx-84945cfd5c-zx9mb
                      kubectl logs nginx-web2-5d448cc744-sk66x
                      ```
                   
                      修复命令示例是根据可能的解决方案提供的，具体修复命令需要根据实际情况进行调整。
                   """;
        builder
            .UseDefaultErrorBehavior(WorkflowErrorHandling.Suspend)
            .StartWith<HelloWorld>()
            .Then<Kubectl>()
            .Input(step => step.Context, ctx => ctx)
            .Input(step => step.Command, ctx => ctx.Command)
            .Output(ctx => ctx.Result, step => step.Result)
            .Then<OpenAi>()
            .Input(step => step.Context, ctx => ctx)
            .Input(step => step.Text, ctx => ctx.Result)
            .Input(step => step.Prompt, ctx => ctx.Prompt)
            .Input(step => step.Ai, ctx => ctx.AiService)
            .Output(ctx => ctx.Result, step => step.Result)
            .Then<CodeExtract>()
            .Input(step => step.Context, ctx => ctx)
            .Input(step => step.Pattern, ctx => CodeExtractPattern.SHELL)
            .Input(step => step.Text, ctx => ctx.Result)
            .Output(ctx => ctx.Result, step => step.Result)
            .Then<CodeExtract>()
            .Input(step => step.Context, ctx => ctx)
            .Input(step => step.Pattern, ctx => CodeExtractPattern.KUBECTL)
            .Input(step => step.Text, ctx => ctx.Result)
            .Output(ctx => ctx.Result, step => step.Result)
            .Then<Kubectl>()
            .Input(step => step.Context, ctx => ctx)
            .Input(step => step.Command, ctx => ctx.Result)
            .Output(ctx => ctx.Result, step => step.Result)
            .Then<GoodbyeWorld>();
    }
}
