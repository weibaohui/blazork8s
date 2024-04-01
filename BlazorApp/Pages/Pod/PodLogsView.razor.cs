using System;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Chat;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using Entity;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using XtermBlazor;

namespace BlazorApp.Pages.Pod;

public partial class PodLogsView : FeedbackComponent<V1Pod, bool>
{
    private readonly TerminalOptions _options = new()
    {
        CursorBlink = true,
        CursorStyle = CursorStyle.Bar,
        Columns     = 100,
        Rows        = 50,
        Theme =
        {
            Background = "#17615e",
        },
    };


    private int            _columns, _rows;
    private string         _containerName;
    private bool           _follow;
    private bool           _ignoreErrors;
    private PodLogExecutor _logHelper;

    private V1Pod     _podItem;
    private string    _podRunningTimeout = "";
    private bool      _prefix;
    private bool      _previous;
    private bool      _showTimestamp;
    private string    _since = "";
    private DateTime? _sinceTimestamp;
    private string    _tail = "";

    private Xterm _terminal;

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
        await _terminal.Clear();
        var _ = _containerName == "all-containers"
            ? _logHelper.SetAllContainers(true)
            : _logHelper.SetAllContainers(false);
        _logHelper.SetShowTimestamp(_showTimestamp)
            .SetSinceTimestamp(_sinceTimestamp)
            .SetFollow(_follow)
            .SetPrefix(_prefix)
            .SetPrevious(_previous)
            .SetSince(_since)
            .SetTail(_tail)
            .SetPodRunningTimeout(_podRunningTimeout)
            .SetIgnoreErrors(_ignoreErrors);
        _logHelper.BuildLogCommand();
        await _logHelper.StartLog();
    }

    private void OnContainerSelectChanged(string name)
    {
        _containerName = name;
    }


    private async Task OnFirstRender()
    {
        // Blazor Server
        // await _terminal.InvokeAddonFunctionVoidAsync("xterm-addon-attach", "new AttachAddon()");
        // await _terminal.InvokeAddonFunctionVoidAsync("xterm-addon-fit", "fit");

        _columns = await _terminal.GetColumns();
        _rows    = await _terminal.GetRows();
    }


    /// <summary>
    /// 接收到SignalR后端发送过来的日志，回显到Xterm
    /// </summary>
    /// <param name="obj"></param>
    private async Task OnPodLogChanged(PodLogEntity obj)
    {
        if (obj.Namespace == _podItem.Namespace() && obj.Name == _podItem.Name())
        {
            await _terminal.WriteLine(obj.LogLineContent);
        }
    }

    #region Getter Setter

    private void DatePickerChanged(DateTimeChangedEventArgs<DateTime?> arg)
    {
        _sinceTimestamp = arg.Date;
    }

    private void OnSinceSelectChanged(string since)
    {
        _since = since;
    }

    private void OnTimeoutSelectChanged(string timeout)
    {
        _podRunningTimeout = timeout;
    }

    private void OnTailSelectChanged(string tail)
    {
        _tail = tail;
    }

    #endregion
}
