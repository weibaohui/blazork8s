using System;
using System.Threading.Tasks;
using BlazorApp.Utils.Terminal;
using Entity;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class PortForwardExecutor
{
    private static readonly ILogger<PortForwardExecutor> Logger = LoggingHelper<PortForwardExecutor>.Logger();
    public                  PortForward                  PortForward { get; set; }

    public string Command()
    {
        var command =
            $"kubectl port-forward --address 0.0.0.0 {PortForward.Type.ToString().ToLower()}/{PortForward.KubeName} {PortForward.LocalPort}:{PortForward.KubePort} \r";
        return command;
    }

    public PortForwardExecutor(PortForward portForward)
    {
        PortForward = portForward;

    }

    public async Task Start()
    {
        if (PortForward == null)
        {
            return;
        }

        var command = Command();
        var service = TerminalHelper.Instance.GetOrCreate(command);
        if (!service.IsRunning)
        {
            await service.Start();
        }

        await service.Write(command);
    }

    public string GetNcProbeCommand()
    {
        return $"nc -v -w 1 -z 127.0.0.1 {PortForward.LocalPort} \r";
    }

    public async Task StartProbe()
    {
        if (PortForward == null)
        {
            return;
        }

        var command = GetNcProbeCommand();
        var service = TerminalHelper.Instance.GetOrCreate(command);

        if (!service.IsRunning)
        {
            await service.Start();
        }

        if (!service.IsStandardOutPutSet)
        {
            service.StandardOutput += (sender, e) =>
            {
                // Logger.LogInformation("PortForwardExecutor Probe StandardOutput:  {Command}::::{Data}", GetNcProbeCommand(),e);
                if (e.Contains("failed"))
                {
                    PortForward.Status = "failed";
                }
                if (e.Contains("succeeded"))
                {
                    PortForward.Status = "succeeded";
                }
                PortForward.StatusTimestamp = DateTime.Now;
            };
            service.StandardError += (sender, e) =>
            {
                Logger.LogInformation("PortForwardExecutor Probe StandardError:  {Command}::::{Data}", GetNcProbeCommand(),e.Message);
            };
        }
        await service.Write(command);
    }

    public void Dispose()
    {
         if (PortForward == null)
         {
             return;
         }
         //释放探测终端、转发命令执行终端
         TerminalHelper.Instance.GetOrCreate(GetNcProbeCommand()).Dispose();
         TerminalHelper.Instance.GetOrCreate(Command()).Dispose();
    }
}
