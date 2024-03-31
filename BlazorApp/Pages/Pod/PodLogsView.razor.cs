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

    private string[] _addonIds = new string[]
    {
        // "xterm-addon-attach",
        // "xterm-addon-fit"
    };

    private int            _columns, _rows;
    private string         _containerName;
    private bool           _follow;
    private PodLogExecutor _logHelper;

    private V1Pod  _podItem;
    private string _searchInput = "";
    private bool   _showTimestamp;

    private Xterm _terminal;

    [Inject]
    private IPodService PodService { get; set; }

    [Inject]
    private IHubContext<ChatHub> _ctx { get; set; }

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
        var _ = _containerName == "all-containers"
            ? _logHelper.SetAllContainers(true)
            : _logHelper.SetAllContainers(false);
        _logHelper.SetShowTimestamp(_showTimestamp);
        _logHelper.SetFollow(_follow);
        _logHelper.BuildLogCommand();
        await _logHelper.StartLog();
    }


    private async Task OnFirstRender()
    {
        // Blazor Server
        // await _terminal.InvokeAddonFunctionVoidAsync("xterm-addon-attach", "new AttachAddon()");
        // await _terminal.InvokeAddonFunctionVoidAsync("xterm-addon-fit", "fit");

        _columns = await _terminal.GetColumns();
        _rows    = await _terminal.GetRows();
    }


    private async Task ScrollToTop(MouseEventArgs args)
    {
        await _terminal.ScrollToTop();
    }

    private async Task ScrollToBottom(MouseEventArgs args)
    {
        await _terminal.ScrollToBottom();
    }


    // private async Task OnSearchClicked(MouseEventArgs args)
    // {
    //     bool searchSuccess =
    //         await _terminal.InvokeAddonFunctionAsync<bool>("xterm-addon-search", "findNext", _searchInput);
    // }


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
}
