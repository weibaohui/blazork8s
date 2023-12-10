using System.Threading.Tasks;
using BlazorApp.Utils.Terminal;
using Entity;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class PortForwardExecutor
{
    private static readonly ILogger<PortForwardExecutor> Logger = LoggingHelper<PortForwardExecutor>.Logger();
    private                 PortForward                  PortForward { get; set; }

    private string Command()
    {
        var command =
            $"kubectl port-forward --address 0.0.0.0 {PortForward.Type.ToString().ToLower()}/{PortForward.Metadata.Name} {PortForward.LocalPort}:{PortForward.KubePort} \r";
        return command;
    }

    public PortForwardExecutor(PortForward portForward)
    {
        PortForward = portForward;
        var command = Command();
        var service = TerminalHelper.Instance.GetOrCreate(command);
        if (!service.IsStandardOutPutSet)
        {
            service.StandardOutput += (sender, e) =>
            {
                Logger.LogInformation($"PortForwardExecutor StandardOutput: {e}");
            };
            service.StandardError += (sender, e) =>
            {
                Logger.LogInformation($"PortForwardExecutor StandardError: {e.Data}");
            };
        }

    }

    public async Task Start()
    {
        var command = Command();
        var service = TerminalHelper.Instance.GetOrCreate(command);
        if (!service.IsRunning)
        {

            await service.Start();
        }

        await service.Write(command);
        Logger.LogInformation($"PortForwardExecutor: {command}");
    }
}
