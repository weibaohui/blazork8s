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


    private int    _columns, _rows;
    private string _containerName;

    //光标index
    private int           _cursorIndex = 0;
    private IList<string> _history     = new List<string>();


    //历史记录索引index
    private int            _historyIndex = 0;
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


                if (_tmpCommand.Length > 0)
                {
                    //光标移动到最后
                    for (var i = _cursorIndex; i < _tmpCommand.Length; i++)
                        await _terminal.Write("\x1B[C");
                    for (var i = 0; i < _tmpCommand.Length; i++)
                        await _terminal.Write("\b \b");

                    //存入history
                    _history.Add(_tmpCommand);
                    //重置historyIndex
                    _historyIndex = _history.Count - 1;
                    if (_historyIndex < 0) _historyIndex = 0;
                }

                //发送命令
                _logHelper.Write(_tmpCommand);
                //重置
                _tmpCommand  = "";
                _cursorIndex = 0;
                break;
            case "ArrowDown":
                if (_historyIndex == _history.Count - 1)
                {
                    //此时是最后一个命令，命令已经显示在xterm上了。再按一次就清空屏幕
                    //此时光标在最后
                    // 退格删除原来的全部字符
                    for (var i = 0; i < _tmpCommand.Length; i++)
                        await _terminal.Write("\b \b");

                    _tmpCommand = "";
                    //重新计算光标位置
                    _cursorIndex = _tmpCommand.Length;
                }

                if (_historyIndex < _history.Count - 1)
                {
                    //已经按过向上键，才能向下。否则不需要向下，向下没有命令了。
                    _historyIndex += 1;

                    //此时光标在最后
                    // 退格删除原来的全部字符
                    for (var i = 0; i < _tmpCommand.Length; i++)
                        await _terminal.Write("\b \b");

                    _tmpCommand = _history[_historyIndex];
                    //按一次向后移动一个历史记录
                    if (_historyIndex > _history.Count - 1) _historyIndex = _history.Count - 1;

                    //重新计算光标位置
                    _cursorIndex = _tmpCommand.Length;
                    await _terminal.Write(_tmpCommand);
                }

                break;
            case "ArrowUp":
                if (_tmpCommand.Length > 0 && _historyIndex < _history.Count - 1)
                {
                    //通过向上按键已经拿到了一个历史记录，此时再次按下向上键，应该清除已经写到屏幕上的命令

                    //此时光标在最后
                    // 退格删除原来的全部字符
                    for (var i = 0; i < _tmpCommand.Length; i++)
                        await _terminal.Write("\b \b");

                    _tmpCommand = _history[_historyIndex];
                    //按一次向前移动一个历史记录
                    _historyIndex -= 1;
                    if (_historyIndex < 0) _historyIndex = 0;

                    //重新计算光标位置
                    _cursorIndex = _tmpCommand.Length;
                    await _terminal.Write(_tmpCommand);
                }

                if (_tmpCommand.Length == 0 && _historyIndex == _history.Count - 1)
                    //没有输入命令的情况下，并且是第一次按键。此时可以按向上键获取历史记录，已经有输入则不可以再按
                    if (_history.Count > 0)
                    {
                        _tmpCommand = _history[_historyIndex];
                        //按一次向前移动一个历史记录
                        _historyIndex -= 1;
                        if (_historyIndex < 0) _historyIndex = 0;

                        //重新计算光标位置
                        _cursorIndex = _tmpCommand.Length;
                        await _terminal.Write(_tmpCommand);
                    }

                break;
            case "Escape":
                _tmpCommand = "";
                await _terminal.Clear();
                break;
            case "Backspace":
            {
                //退格前的字符全文长度
                var oldLength = _tmpCommand.Length;
                var prefix    = _tmpCommand[.._cursorIndex];
                var suffix    = _tmpCommand.Substring(_cursorIndex, _tmpCommand.Length - _cursorIndex);
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
                _cursorIndex -= 1;
            }
                break;
            case "ArrowLeft":
                _cursorIndex -= 1;
                await _terminal.Write("\x1B[D");
                break;
            case "ArrowRight":
                _cursorIndex += 1;
                await _terminal.Write("\x1B[C");
                break;
            default:
                if (args.DomEvent.CtrlKey || args.DomEvent.MetaKey || args.DomEvent.ShiftKey || args.DomEvent.AltKey)
                {
                    if (args.DomEvent is { Key: "c", CtrlKey: true }) await _terminal.Write("Ctrl+C\n");
                    break;
                }

                if (_cursorIndex == _tmpCommand.Length || _tmpCommand.Length == 0)
                {
                    //在末尾插入字符
                    _cursorIndex += 1;
                    _tmpCommand  += args.DomEvent.Key;
                    await _terminal.Write(args.DomEvent.Key);
                }
                else
                {
                    //在光标后插入一个字符
                    var offset = _tmpCommand.Length - _cursorIndex;
                    //使用了左右光标进行移动，左移-1，右移+1，0为最前
                    //将新字符串插入到光标位置
                    var prefix = _tmpCommand[.._cursorIndex];
                    var suffix = _tmpCommand.Substring(_cursorIndex, _tmpCommand.Length - _cursorIndex);
                    _tmpCommand = prefix + args.DomEvent.Key + suffix;

                    //光标移动到最后
                    for (var i = _cursorIndex; i < _tmpCommand.Length; i++)
                        await _terminal.Write("\x1B[C");


                    // 清除之前的所有字符串
                    for (var i = 0; i < _tmpCommand.Length; i++)
                        await _terminal.Write("\b \b");

                    //重新写入最新的字符串
                    await _terminal.Write(_tmpCommand);

                    //光标复原到插入新字符串的位置,中长度-索引=向左偏移
                    for (var i = 0; i <= offset - 1; i++)
                        await _terminal.Write("\x1B[D");

                    _cursorIndex += 1;
                }

                break;
        }
    }


    private async Task Resize(MouseEventArgs args)
    {
        await _terminal.Resize(_columns, _rows);
    }
}
