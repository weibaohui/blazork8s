#nullable enable
using System.Threading.Tasks;
using BlazorApp.Chat;
using Entity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp.Utils;

public class PodLogExecutor
{
    private static readonly ILogger<PodLogExecutor> Logger = LoggingHelper<PodLogExecutor>.Logger();

    public string  Namespace;
    public string  Name;
    public string  ContainerName;
    public string? Command;
    public bool    ShowTimestamp = true;
    public bool    Follow        = true;
    public string? SinceTimestamp;
    public bool    ShowAll = true;

    public int     Columns;
    public int     Rows;

    //返回的日志内容
    private IHubContext<ChatHub>? _ctx;

    public PodLogExecutor(string ns, string name, string containerName)
    {
        Namespace     = ns;
        Name          = name;
        ContainerName = containerName;
    }


    public PodLogExecutor SetHubContext(IHubContext<ChatHub> ctx)
    {
        _ctx = ctx;
        return this;
    }


    public PodLogExecutor BuildCommand()
    {
        Command = "";
        var extCmd = "";
        if (Follow)
            extCmd += " --follow ";
        if (ShowTimestamp)
            extCmd += " --timestamps ";
        if (ShowAll && !SinceTimestamp.IsNullOrEmpty())
            extCmd += $" --since-time='{SinceTimestamp}' ";

        Command = $"logs -n {Namespace} {Name} -c {ContainerName} {extCmd}";
        return this;
    }

    public string Key => $"{Namespace}/{Name}/{ContainerName}";

    public void Kill()
    {
        CommandExecutorHelper.Instance.Kill(Key);
        Logger.LogInformation($"{Key} killed");
    }

    public async Task Exec()
    {
        if (Command.IsNullOrEmpty())
        {
            return;
        }

        //log 每次获取前都杀死一次
        CommandExecutorHelper.Instance.Kill(Key);
        var executor = CommandExecutorHelper.Instance.Create(Key);

        executor.CommandExecuted += (sender, e) =>
        {
            var entity = new PodLogEntity
            {
                Namespace      = Namespace,
                Name           = Name,
                ContainerName  = ContainerName,
                LogLineContent = e.Output,
            };
            //TODO PodLog更换为枚举值
            _ctx?.Clients.All.SendAsync("PodLog", entity);
        };
        await executor.ExecuteCommandAsync("kubectl", @Command);
    }

    public PodLogExecutor SetColumns(int columns)
    {
        Columns = columns;
        return this;
    }

    public PodLogExecutor SetRows(int rows)
    {
        Rows = rows;
        return this;
    }

    public PodLogExecutor SetContainerName(string containerName)
    {
        ContainerName = containerName;
        return this;
    }
}
