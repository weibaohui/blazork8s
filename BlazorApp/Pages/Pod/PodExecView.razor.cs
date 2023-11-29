using System;
using System.Text.Json;
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
using Microsoft.JSInterop;
using XtermBlazor;

namespace BlazorApp.Pages.Pod;

public partial class PodExecView : FeedbackComponent<V1Pod, bool>
{
    [Inject]
    private IPodService PodService { get; set; }

    [Inject]
    private IHubContext<ChatHub> _ctx { get; set; }


    private V1Pod          _podItem;
    private string         _containerName;
    private PodLogExecutor _logHelper;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _podItem       = base.Options;
        _containerName = _podItem.Spec.Containers[0].Name;
        _logHelper = new PodLogExecutorHelper().GetOrCreate(_podItem.Namespace(), _podItem.Name(), _containerName)
            .SetHubContext(_ctx);
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
            await _terminal.Write(obj.LogLineContent);
        }
    }


    [Inject]
    protected IJSRuntime JSRuntime { get; set; }

    private Xterm _terminal, _terminalEvent;

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


    private TerminalOptions _options2 = new TerminalOptions
    {
        Columns = 140,
    };

    private bool   showAttachCustomKeyEventHandlerLog;
    private int    _eventId     = 0,  _columns, _rows;
    private string _searchInput = "", _input = "Hello World";

    private async Task OnFirstRender()
    {
        await _terminalEvent.WriteLine($"({++_eventId}) OnFirstRender()");

        bool isWebAssembly = JSRuntime is IJSInProcessRuntime;

        if (isWebAssembly)
        {
            // Blazor WebAssembly
            _terminal.AttachCustomKeyEventHandler((args) =>
            {
                if (showAttachCustomKeyEventHandlerLog)
                {
                    _terminalEvent.WriteLine(
                        $"({++_eventId}) AttachCustomKeyEventHandler(): Key: {args.Key}, Ctrl: {args.CtrlKey} {JsonSerializer.Serialize(args)}");
                }

                return true;
            });
        }
        else
        {
            // Blazor Server
            await _terminal.AttachCustomKeyEventHandlerEvaluate("(event) => true");

            _terminal.AttachCustomKeyEventHandler((args) =>
            {
                if (showAttachCustomKeyEventHandlerLog)
                {
                    _terminalEvent.WriteLine(
                        $"({++_eventId}) AttachCustomKeyEventHandler(): Key: {args.Key}, Ctrl: {args.CtrlKey} {JsonSerializer.Serialize(args)}");
                }

                // The return value is ignored on Blazor Server, AttachCustomKeyEventHandlerEvaluate is used for handling that
                return true;
            });
        }


        _columns = await _terminal.GetColumns();
        _rows    = await _terminal.GetRows();
    }

    private async Task OnBinary(string data)
    {
        await _terminalEvent.WriteLine($"({++_eventId}) OnBinary(): {data}");
    }

    private async Task OnCursorMove()
    {
        await _terminalEvent.WriteLine($"({++_eventId}) OnCursorMove()");
    }

    private string _tmpCommand = "";

    private async Task OnData(string data)
    {
        _tmpCommand += data;
        await _terminal.Write(_tmpCommand);
        await _terminalEvent.WriteLine($"({++_eventId}) OnData(): {data}");
    }

    private async Task OnKey(KeyboardEventArgs args)
    {
        if (args.Key == "Enter")
        {
            _logHelper.Write(_tmpCommand);
            await _terminalEvent.WriteLine($"_tmpCommand {_tmpCommand} sended");
            _tmpCommand = "";
        }

        await _terminal.Write("\r");

        await _terminalEvent.WriteLine(
            $"({++_eventId}) OnKey(): Key: {args.Key}, Ctrl: {args.CtrlKey} {JsonSerializer.Serialize(args)}");
    }

    private async Task OnLineFeed()
    {
        await _terminalEvent.WriteLine($"({++_eventId}) OnLineFeed()");
    }

    private async Task OnScroll(int newPosition)
    {
        await _terminalEvent.WriteLine($"({++_eventId}) OnScroll(): newPosition: {newPosition}");
    }

    private async Task OnSelectionChange()
    {
        await _terminalEvent.WriteLine(
            $"({++_eventId}) OnSelectionChange(): GetSelection(): {await _terminal.GetSelection()}");
    }

    private async Task OnRender(RenderEventArgs args)
    {
        await _terminalEvent.WriteLine($"({++_eventId}) OnRender(): {JsonSerializer.Serialize(args)}");
    }

    private async Task OnResize(ResizeEventArgs args)
    {
        await _terminalEvent.WriteLine($"({++_eventId}) OnResize(): {JsonSerializer.Serialize(args)}");
    }

    private async Task OnTitleChange(string title)
    {
        await _terminalEvent.WriteLine($"({++_eventId}) OnTitleChange(): title: {title}");
    }

    private async Task OnBell()
    {
        await _terminalEvent.WriteLine($"({++_eventId}) OnBell()");
    }

    private async Task Write(MouseEventArgs args)
    {
        await _terminal.Write(_input);
    }

    private async Task WriteLine(MouseEventArgs args)
    {
        await _terminal.WriteLine(_input);
    }

    private async Task HasSelection(MouseEventArgs args)
    {
        await _terminalEvent.WriteLine($"({++_eventId}) HasSelection(): {await _terminal.HasSelection()}");
    }

    private async Task GetSelection(MouseEventArgs args)
    {
        await _terminalEvent.WriteLine($"({++_eventId}) GetSelection(): {await _terminal.GetSelection()}");
    }

    private async Task GetSelectionPosition(MouseEventArgs args)
    {
        var selectionPosition = await _terminal.GetSelectionPosition();
        await _terminalEvent.WriteLine(
            $"({++_eventId}) GetSelectionPosition(): {JsonSerializer.Serialize(selectionPosition)}");
    }

    private async Task ClearSelection(MouseEventArgs args)
    {
        await _terminal.ClearSelection();
    }

    private async Task SelectAll(MouseEventArgs args)
    {
        await _terminal.SelectAll();
    }

    private async Task ScrollToTop(MouseEventArgs args)
    {
        await _terminal.ScrollToTop();
    }

    private async Task ScrollToBottom(MouseEventArgs args)
    {
        await _terminal.ScrollToBottom();
    }

    private async Task Reset(MouseEventArgs args)
    {
        await _terminal.Reset();
    }

    private async Task Search(MouseEventArgs args)
    {
        bool searchSuccess =
            await _terminal.InvokeAddonFunctionAsync<bool>("xterm-addon-search", "findNext", _searchInput);
        await _terminalEvent.WriteLine($"({++_eventId}) Search(): {searchSuccess}");
    }

    private async Task Resize(MouseEventArgs args)
    {
        await _terminal.Resize(_columns, _rows);
    }

    private async Task GetRows(MouseEventArgs args)
    {
        var rows = await _terminal.GetRows();
        await _terminalEvent.WriteLine($"({++_eventId}) GetRows(): {JsonSerializer.Serialize(rows)}");
    }

    private async Task GetColumns(MouseEventArgs args)
    {
        var cols = await _terminal.GetColumns();
        await _terminalEvent.WriteLine($"({++_eventId}) GetColumns(): {JsonSerializer.Serialize(cols)}");
    }

    private async Task GetOptions(MouseEventArgs args)
    {
        var serializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        var options = await _terminal.GetOptions();
        await _terminalEvent.WriteLine(
            $"({++_eventId}) GetOptions(): {JsonSerializer.Serialize(options, serializerOptions).Replace("\n", "\r\n")}");
    }

    private async Task SetOptions(MouseEventArgs args)
    {
        Random random = new Random();
        string color  = $"#{random.Next(0x1000000):X6}";

        await _terminal.SetOptions(new TerminalOptions
        {
            Theme =
            {
                Background = color
            }
        });

        await _terminalEvent.WriteLine($"({++_eventId}) SetOptions(): Set Background color to {color}");
    }
}
