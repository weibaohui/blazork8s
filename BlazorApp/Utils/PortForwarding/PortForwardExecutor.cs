using System;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using Entity;
using k8s.Models;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils.PortForwarding;

public class PortForwardExecutor(PortForward pf)
{
    private static readonly ILogger<PortForwardExecutor> Logger = LoggingHelper<PortForwardExecutor>.Logger();

    public PortForward GetPortForward()
    {
        return pf;
    }


    private string Args()
    {
        var args =
            $" port-forward -n {pf.KubeNamespace}  --address 0.0.0.0 {pf.Type.ToString().ToLower()}/{pf.KubeName} {pf.LocalPort}:{pf.KubePort}";
        return args;
    }

    public void Start()
    {
        if (pf == null)
        {
            return;
        }

        ProcessManager.Instance.StartService(pf.Name(), "kubectl", Args());
    }

    private string GetNcProbeCommand()
    {
        return $"nc -v -w 1 -z 127.0.0.1 {pf.LocalPort} \r";
    }

    private string GetNcProbeArgs()
    {
        return $"-v -w 1 -z 127.0.0.1 {pf.LocalPort} \r";
    }

    public async Task StartProbe()
    {
        if (pf == null)
        {
            return;
        }

        try
        {
            var cmd = Cli.Wrap("nc")
                .WithValidation(CommandResultValidation.None)
                .WithArguments(GetNcProbeArgs());
            // 执行命令并捕获输出
            var result = await cmd.ExecuteBufferedAsync();
            var all = result.StandardOutput + result.StandardError;
            if (all.Contains("failed"))
                pf.Status = "failed";
            else if (all.Contains("succeeded")) pf.Status = "succeeded";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, " Probe Error:  {Command}", GetNcProbeCommand());
            pf.Status = "failed";
            return;
        }

        pf.StatusTimestamp = DateTime.Now;
    }

    public void Dispose()
    {
        if (pf == null)
        {
            return;
        }

        ProcessManager.Instance.StopService(pf.Name());
    }
}
