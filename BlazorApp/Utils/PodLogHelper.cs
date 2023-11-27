#nullable enable
using System.Threading.Tasks;
using BlazorApp.Chat;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp.Utils;

public class PodLogHelper
{
    public string Namespace;
    public string Name;
    public string ContainerName;
    public string Command;
    public bool   ShowTimestamp;
    public bool   Follow = true;
    public string SinceTimestamp;
    public bool   ShowAll;

    public string LastHeartBeatTime;
    public int    Columns;
    public int    Rows;

    //返回的日志内容
    private IHubContext<ChatHub>? _ctx;


    public PodLogHelper Create()
    {
        return this;
    }

    public PodLogHelper SetHubContext(IHubContext<ChatHub> ctx)
    {
        _ctx = ctx;
        return this;
    }

    public PodLogHelper Create(V1Pod pod)
    {
        Namespace     = pod.Namespace();
        Name          = pod.Name();
        ContainerName = pod.Spec.Containers[0].Name;
        return this;
    }

    public PodLogHelper Create(V1Pod pod, string containerName)
    {
        Namespace     = pod.Namespace();
        Name          = pod.Name();
        ContainerName = containerName;
        return this;
    }


    public PodLogHelper BuildCommand()
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

    public async Task Exec()
    {
        if (Command.IsNullOrEmpty())
        {
            return;
        }

        //log 每次获取前都杀死一次
        CommandExecutorHelper.Instance.Kill(Command);
        var executor = CommandExecutorHelper.Instance.Create(Command);

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

    public PodLogHelper SetColumns(int columns)
    {
        Columns = columns;
        return this;
    }
    public PodLogHelper SetRows(int rows)
    {
        Rows = rows;
        return this;
    }
}
