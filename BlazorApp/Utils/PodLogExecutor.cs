#nullable enable
using System.Threading.Tasks;
using BlazorApp.Chat;
using BlazorApp.Utils.Terminal;
using Entity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp.Utils;

public class PodLogExecutor
{
    private static readonly ILogger<PodLogExecutor> Logger = LoggingHelper<PodLogExecutor>.Logger();

    private string  _namespace;
    private string  _name;
    private string  _containerName;
    private string? _command;
    private bool    _showTimestamp = true;
    private bool    _follow        = true;
    private string? _sinceTimestamp;
    private bool    _showAll = true;

    private int _columns;
    private int _rows;

    //返回的日志内容
    private IHubContext<ChatHub>? _ctx;

    public PodLogExecutor(string ns, string name, string containerName)
    {
        _namespace     = ns;
        _name          = name;
        _containerName = containerName;
    }


    public PodLogExecutor SetHubContext(IHubContext<ChatHub> ctx)
    {
        _ctx = ctx;
        return this;
    }


    public PodLogExecutor BuildLogCommand()
    {
        _command = "";
        var extCmd = "";
        if (_follow)
            extCmd += " --follow ";
        if (_showTimestamp)
            extCmd += " --timestamps ";
        if (_showAll && !_sinceTimestamp.IsNullOrEmpty())
            extCmd += $" --since-time='{_sinceTimestamp}' ";

        _command = $"logs -n {_namespace} {_name} -c {_containerName} {extCmd}";
        return this;
    }

    public PodLogExecutor BuildExecCommand()
    {
        _command = $"""exec -i -t -n {_namespace} {_name} -c {_containerName} -- sh -c "clear; (bash  || sh)" """;
        return this;
    }

    public string Key => $"{_namespace}/{_name}/{_containerName}";

    public void Kill()
    {
        CommandExecutorHelper.Instance.Kill(Key);
        Logger.LogInformation($"{Key} killed");
    }


    public async Task StartLog()
    {
        if (_command.IsNullOrEmpty())
        {
            return;
        }

        //log 每次获取前都杀死一次
        Logger.LogInformation($"{Key} killed before log");

        TerminalHelper.Instance.Kill(Key);
        Logger.LogInformation("Log " + _command);

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

    public async Task StartExec()
    {
        if (_command.IsNullOrEmpty())
        {
            return;
        }

        Logger.LogInformation("Exec " + _command);
        Logger.LogInformation($"{Key} killed before exec");

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

    public PodLogExecutor SetColumns(int columns)
    {
        _columns = columns;
        return this;
    }

    public PodLogExecutor SetRows(int rows)
    {
        _rows = rows;
        return this;
    }

    public PodLogExecutor SetContainerName(string containerName)
    {
        _containerName = containerName;
        return this;
    }
}
