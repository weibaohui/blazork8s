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

    private Xterm _terminal;

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


    private int    _eventId     = 0,  _columns, _rows;
    private string _searchInput = "", _input = "Hello World";
    private string[] _addonIds = new string[]
    {
        // "xterm-addon-attach",
        "xterm-addon-fit"
    };
    private async Task OnFirstRender()
    {
        await _terminal.InvokeAddonFunctionVoidAsync("xterm-addon-fit", "fit");
        _columns = await _terminal.GetColumns();
        _rows    = await _terminal.GetRows();
    }

    private string _tmpCommand = "";

    private async Task OnData(string data)
    {
        _tmpCommand += data;
        await _terminal.Write(_tmpCommand);
    }

    private async Task OnKey(KeyboardEventArgs args)
    {
        if (args.Key == "Enter")
        {
            _logHelper.Write(_tmpCommand);
            _tmpCommand = "";
        }

        await _terminal.Write("\r");
    }





    private async Task Search(MouseEventArgs args)
    {
        bool searchSuccess =
            await _terminal.InvokeAddonFunctionAsync<bool>("xterm-addon-search", "findNext", _searchInput);
    }

    private async Task Resize(MouseEventArgs args)
    {
        await _terminal.Resize(_columns, _rows);
    }








}
