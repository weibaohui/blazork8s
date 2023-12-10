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
                Logger.LogInformation("PortForwardExecutor StandardOutput: {Data}", e);
            };
            service.StandardError += (sender, e) =>
            {
                Logger.LogInformation("PortForwardExecutor StandardError: {Data}", e.Data);
            };
        }
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
}
