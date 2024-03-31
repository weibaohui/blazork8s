#nullable enable
using System;
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
    private readonly        string                  _name;

    private readonly string _namespace;
    private          bool   _allContainers = false;

    private int     _columns;
    private string? _command;
    private string  _containerName;

    //返回的日志内容
    private IHubContext<ChatHub>? _ctx;
    private bool                  _follow;
    private bool                  _ignoreErrors;
    private bool                  _prefix;
    private bool                  _previous;
    private int                   _rows;
    private bool                  _showTimestamp;
    private DateTime?             _sinceTimestamp;

    public PodLogExecutor(string ns, string name, string containerName)
    {
        _namespace     = ns;
        _name          = name;
        _containerName = containerName;
    }

    public string Key => $"{_namespace}/{_name}/{_containerName}";


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
        if (_prefix)
            extCmd += " --prefix=true ";
        if (_previous)
            extCmd += " --previous=true ";
        if (_ignoreErrors)
            extCmd += " --ignore-errors=true ";
        if (_showTimestamp)
            extCmd += " --timestamps ";
        if (_sinceTimestamp != null)
            extCmd += $" --since-time='{new DateTimeOffset(_sinceTimestamp.Value):yyyy-MM-dd'T'HH:mm:ss.fffK}' ";

        extCmd += _allContainers ? " --all-containers=true " : $" -c {_containerName}  ";

        _command = $"logs -n {_namespace} {_name} {extCmd}";
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


    public async Task StartLog()
    {
        if (_command.IsNullOrEmpty())
        {
            return;
        }

        //log 每次获取前都杀死一次
        Logger.LogInformation("{Key} killed before log", Key);

        TerminalHelper.Instance.Kill(Key);
        Logger.LogInformation("Command: {Command}", _command);

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

        Logger.LogInformation("Exec {Command}", _command);
        Logger.LogInformation("{Key} killed before exec", Key);

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

    public PodLogExecutor SetAllContainers(bool flag)
    {
        _allContainers = flag;
        return this;
    }

    public PodLogExecutor SetShowTimestamp(bool showTimestamp)
    {
        _showTimestamp = showTimestamp;
        return this;
    }

    public PodLogExecutor SetFollow(bool follow)
    {
        _follow = follow;
        return this;
    }

    public PodLogExecutor SetPrefix(bool prefix)
    {
        _prefix = prefix;
        return this;
    }

    public PodLogExecutor SetPrevious(bool previous)
    {
        _previous = previous;
        return this;
    }

    public PodLogExecutor SetIgnoreErrors(bool ignoreErrors)
    {
        _ignoreErrors = ignoreErrors;
        return this;
    }

    public PodLogExecutor SetSinceTimestamp(DateTime? sinceTimestamp)
    {
        _sinceTimestamp = sinceTimestamp;
        return this;
    }
}
