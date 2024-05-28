using System;
using System.Threading.Tasks;
using BlazorApp.Utils.Terminal;
using CliWrap;
using CliWrap.Buffered;
using Entity;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils.PortForwarding;

public class PortForwardExecutor
{
    private static readonly ILogger<PortForwardExecutor> Logger = LoggingHelper<PortForwardExecutor>.Logger();

    public PortForwardExecutor(PortForward portForward)
    {
        PortForward = portForward;
    }

    public PortForward PortForward { get; set; }

    private string Command()
    {
        var command =
            $"kubectl port-forward -n {PortForward.KubeNamespace}  --address 0.0.0.0 {PortForward.Type.ToString().ToLower()}/{PortForward.KubeName} {PortForward.LocalPort}:{PortForward.KubePort} \r";
        return command;
    }

    public async Task Start()
    {
        if (PortForward == null)
        {
            return;
        }

        var command = Command();
        // Logger.LogError("PTY: {Command}",command);
        var service = TerminalHelper.Instance.GetOrCreate(command);
        if (!service.IsRunning)
        {
            await service.Start();
        }

        await service.Write(command);
    }

    private string GetNcProbeCommand()
    {
        return $"nc -v -w 1 -z 127.0.0.1 {PortForward.LocalPort} \r";
    }

    private string GetNcProbeParameters()
    {
        return $"-v -w 1 -z 127.0.0.1 {PortForward.LocalPort} \r";
    }

    public async Task StartProbe()
    {
        if (PortForward == null)
        {
            return;
        }

        try
        {
            var cmd = Cli.Wrap("nc")
                .WithValidation(CommandResultValidation.None)
                .WithArguments(GetNcProbeParameters());
            // 执行命令并捕获输出
            var result = await cmd.ExecuteBufferedAsync();
            var all = result.StandardOutput + result.StandardError;
            if (all.Contains("failed"))
                PortForward.Status = "failed";
            else if (all.Contains("succeeded")) PortForward.Status = "succeeded";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "PortForwardExecutor Probe Error:  {Command}", GetNcProbeCommand());
            PortForward.Status = "failed";
            return;
        }

        PortForward.StatusTimestamp = DateTime.Now;
    }

    public void Dispose()
    {
        if (PortForward == null)
        {
            return;
        }

        //释放探测终端、转发命令执行终端
        TerminalHelper.Instance.GetOrCreate(Command()).Dispose();
    }
}
