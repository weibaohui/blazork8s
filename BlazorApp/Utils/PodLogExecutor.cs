#nullable enable
using System.Threading.Tasks;
using BlazorApp.Chat;
using BlazorApp.Utils.Terminal;
using Entity;
using Extension;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Utils;

public class PodLogExecutor
{
    private static readonly ILogger<PodLogExecutor> Logger = LoggingHelper<PodLogExecutor>.Logger();
    private readonly        string                  _name;

    private readonly string _namespace;

    private string? _command;
    private string  _containerName;

    //返回的日志内容
    private IHubContext<ChatHub>? _ctx;

    public PodLogExecutor(string ns, string name, string containerName)
    {
        _namespace     = ns;
        _name          = name;
        _containerName = containerName;
    }

    private string Key => $"{_namespace}/{_name}/{_containerName}";


    public PodLogExecutor SetHubContext(IHubContext<ChatHub> ctx)
    {
        _ctx = ctx;
        return this;
    }


    public PodLogExecutor BuildExecCommand()
    {
        _command = $"""exec -i -t -n {_namespace} {_name} -c {_containerName} -- sh -c "clear; (bash  || sh)" """;
        return this;
    }

    public void Kill()
    {
        TerminalHelper.Instance.Kill(Key);
        Logger.LogInformation("{Key} killed", Key);
    }

    public async Task StartExec()
    {
        if (_command.IsNullOrEmpty())
        {
            return;
        }

        Logger.LogInformation("Exec {Command}", _command);
        TerminalHelper.Instance.Kill(Key);

        var terminalService = TerminalHelper.Instance.GetOrCreate(Key);
        if (!terminalService.IsStandardOutPutSet)
        {
            terminalService.StandardOutput += (_, e) =>
            {
                var entity = new PodLogEntity
                {
                    Namespace      = _namespace,
                    Name           = _name,
                    ContainerName  = _containerName,
                    LogLineContent = e,
                };
                //TODO PodLog更换为枚举值
                _ctx?.Clients.All.SendAsync("PodLog", entity);
            };
        }


        if (!terminalService.IsRunning)
        {
            await terminalService.Start();
        }

        await terminalService.Write($"kubectl {_command} \r");
    }

    public async void Write(string content)
    {
        var terminalService = TerminalHelper.Instance.GetOrCreate(Key);
        await terminalService.Write(content);
        await terminalService.Write('\r');
    }

    #region Getter Setter

    public PodLogExecutor SetContainerName(string containerName)
    {
        _containerName = containerName;
        return this;
    }

    #endregion

    public string? GetCommand()
    {
        return _command;
    }
}
