using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Chat;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using XtermBlazor;

namespace BlazorApp.Pages.Pod;

public partial class PodExecView : FeedbackComponent<V1Pod, bool>
{
    private HashSet<string> _addons =
    [
        "addon-fit"
    ];


    private int            _columns, _rows;
    private string         _containerName;
    private string         _lastCommand = "";
    private PodLogExecutor _logHelper;

    private TerminalOptions _options = new TerminalOptions
    {
        CursorBlink = true,
        CursorStyle = CursorStyle.Bar,
        Rows        = 20,
        Columns     = 100,
        Theme =
        {
            Background = "#17615e",
        },
    };


    private V1Pod _podItem;

    private Xterm _terminal;

    private string _tmpCommand = "";

    private int index = 0;

    [Inject]
    private ILogger<PodExecView> Logger { get; set; }

    [Inject]
    private IPodService PodService { get; set; }

    [Inject]
    private IHubContext<ChatHub> Ctx { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _podItem       = base.Options;
        _containerName = _podItem.Spec.Containers[0].Name;
        _logHelper = new PodLogExecutorHelper().GetOrCreate(_podItem.Namespace(), _podItem.Name(), _containerName)
            .SetHubContext(Ctx);
    }

    private async Task OnOkBtnClicked()
    {
        await OnContainerSelectChanged(_containerName);
    }

    private async Task OnContainerSelectChanged(string name)
    {
        _containerName = name;
        await _terminal.Clear();
        _logHelper.SetContainerName(_containerName);
        _logHelper.BuildExecCommand();
        await _logHelper.StartExec();
    }

    /// <summary>
    /// 接收到SignalR后端发送过来的日志，回显到Xterm
    /// </summary>
    /// <param name="obj"></param>
    private async Task OnPodLogChanged(PodLogEntity obj)
    {
        if (obj.Namespace == _podItem.Namespace() && obj.Name == _podItem.Name())
        {
            var resp = obj.LogLineContent;
            // resp = resp.Replace(_lastCommand, "");
            await _terminal.Write(resp);
        }
    }

    private async Task OnFirstRender()
    {
        // await _terminal.Addon("addon-fit").InvokeVoidAsync("fit");
        _columns = await _terminal.GetColumns();
        _rows    = await _terminal.GetRows();
    }

    private async Task OnKey(KeyEventArgs args)
    {
        switch (args.DomEvent.Key)
        {
            case "Enter":
                _lastCommand = _tmpCommand;
                //光标移动到最后
                for (var i = index; i < _tmpCommand.Length; i++)
                    await _terminal.Write("\x1B[C");
                for (var i = 0; i < _tmpCommand.Length; i++)
                    await _terminal.Write("\b \b");

                _logHelper.Write(_tmpCommand);
                _tmpCommand = "";
                index       = 0;
                break;
            case "Escape":
                _tmpCommand = "";
                await _terminal.Clear();
                break;
            case "Backspace":
            {
                //退格前的字符全文长度
                var oldLength = _tmpCommand.Length;
                var prefix    = _tmpCommand[..index];
                var suffix    = _tmpCommand.Substring(index, _tmpCommand.Length - index);
                _tmpCommand = prefix[..^1] + suffix;

                //光标移动到最后
                for (var i = 0; i <= suffix.Length + 1; i++)
                    await _terminal.Write("\x1B[C");


                // 退格删除原来的全部字符
                for (var i = 0; i <= oldLength + 1; i++)
                    await _terminal.Write("\b \b");

                //新的字符串重新写入
                await _terminal.Write(_tmpCommand);

                //光标重新回到删除字符前的位置
                for (var i = 0; i < suffix.Length; i++)
                    await _terminal.Write("\x1B[D");

                //重新计算index位置
                index -= 1;
            }
                break;
            case "ArrowLeft":
                index -= 1;
                await _terminal.Write("\x1B[D");
                break;
            case "ArrowRight":
                index += 1;
                await _terminal.Write("\x1B[C");
                break;
            default:
                if (index == _tmpCommand.Length || _tmpCommand.Length == 0)
                {
                    //在末尾插入字符
                    index       += 1;
                    _tmpCommand += args.DomEvent.Key;
                    await _terminal.Write(args.DomEvent.Key);
                }
                else
                {
                    //在光标后插入一个字符
                    var offset = _tmpCommand.Length - index;
                    //使用了左右光标进行移动，左移-1，右移+1，0为最前
                    //将新字符串插入到光标位置
                    var prefix = _tmpCommand[..index];
                    var suffix = _tmpCommand.Substring(index, _tmpCommand.Length - index);
                    _tmpCommand = prefix + args.DomEvent.Key + suffix;

                    //光标移动到最后
                    for (var i = index; i < _tmpCommand.Length; i++)
                        await _terminal.Write("\x1B[C");


                    // 清除之前的
                    for (var i = 0; i < _tmpCommand.Length; i++)
                        await _terminal.Write("\b \b");

                    //重新写入最新的
                    await _terminal.Write(_tmpCommand);

                    //光标复原到插入新字符串的位置,中长度-索引=向左偏移
                    for (var i = 0; i <= offset - 1; i++)
                        await _terminal.Write("\x1B[D");

                    index += 1;
                }

                break;
        }

        if (args.DomEvent is { Key: "c", CtrlKey: true })
        {
            await _terminal.Write("Ctrl+C");
            await _terminal.Clear();
        }
    }


    private async Task Resize(MouseEventArgs args)
    {
        await _terminal.Resize(_columns, _rows);
    }
}
