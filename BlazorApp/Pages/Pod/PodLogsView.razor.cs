using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service;
using BlazorApp.Service.impl;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using XtermBlazor;

namespace BlazorApp.Pages.Pod;

public partial class PodLogsView : FeedbackComponent<V1Pod, bool>
{
    [Inject]
    private IPodService PodService { get; set; }


    public V1Pod PodItem;


    protected override void OnInitialized()
    {
        PodItem = base.Options;
        base.OnInitialized();
    }

    private Xterm _terminal;

    private TerminalOptions _options = new TerminalOptions
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
        "xterm-addon-fit",
        "xterm-addon-search",
        "xterm-addon-web-links",
    };


    private int    _columns, _rows;
    private string _searchInput = "";


    private async Task OnFirstRender()
    {
        var logs = await PodService.Logs(PodItem, true);

        await using (var xs = new XtermStream(_terminal))
        {
            await logs.CopyToAsync(xs);
        }


        // Blazor Server
        await _terminal.InvokeAddonFunctionVoidAsync("xterm-addon-fit", "fit");

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
