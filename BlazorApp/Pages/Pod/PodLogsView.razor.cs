using System;
using System.Threading;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.k8s;
using BlazorApp.Utils;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using XtermBlazor;

namespace BlazorApp.Pages.Pod;

public partial class PodLogsView : FeedbackComponent<V1Pod, bool>, IDisposable
{
    private readonly CancellationTokenSource _gracefulCts = new();

    private readonly TerminalOptions _options = new()
    {
        CursorBlink = true,
        CursorStyle = CursorStyle.Bar,
        Columns     = 100,
        Rows        = 50,
        Theme =
        {
            Background = "#17615e"
        }
    };

    private string _command = "";

    private PodLogCommandBuilder _commander;

    private string    _containerName;
    private bool      _follow;
    private bool      _ignoreErrors;
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
    private IKubectlService Kubectl { get; set; }

    [Inject]
    private IPodService PodService { get; set; }

    public new void Dispose()
    {
        _gracefulCts.CancelAsync();
    }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _podItem       = base.Options;
        _containerName = _podItem.Spec.Containers[0].Name;
        _tail          = "2";
        Kubectl.SetOutputEventHandler(EventHandler);
        Kubectl.SetCancellationToken(_gracefulCts.Token);
        _commander = new PodLogCommandBuilder();
    }

    private async void EventHandler(object sender, string resp)
    {
        await _terminal.WriteLine(resp);
        await InvokeAsync(StateHasChanged);
    }


    private async Task OnOkBtnClicked()
    {
        await _terminal.Clear();

        _commander
            .SetNamespace(_podItem.Namespace())
            .SetPodName(_podItem.Name())
            .SetContainerName(_containerName)
            .SetAllContainers(_containerName == "all-containers")
            .SetShowTimestamp(_showTimestamp)
            .SetSinceTimestamp(_sinceTimestamp)
            .SetFollow(_follow)
            .SetPrefix(_prefix)
            .SetPrevious(_previous)
            .SetSince(_since)
            .SetTail(_tail)
            .SetPodRunningTimeout(_podRunningTimeout)
            .SetIgnoreErrors(_ignoreErrors);
        _commander.Build();
        _command = _commander.GetCommand();
        if (!string.IsNullOrWhiteSpace(_command)) await Kubectl.Command(_command);
    }

    private void OnContainerSelectChanged(string name)
    {
        _containerName = name;
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
