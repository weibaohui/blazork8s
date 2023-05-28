using System;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Service;
using BlazorApp.Service.impl;
using k8s.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using XtermBlazor;

namespace BlazorApp.Pages.Learn;

public partial class LessonIndex : ComponentBase
{
    [Inject]
    private IPodService PodService { get; set; }

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

    private string[] _addonIds =
    {
        "xterm-addon-attach",
    };


    private int _columns, _rows;

    private async Task OnFirstRender()
    {
        // await _terminal.InvokeAddonFunctionVoidAsync("xterm-addon-attach", "attach");

        var pods    = await PodService.ListPods();
        var podItem = pods.First(x => x.Namespace() == "default" && x.Name() == "nginx-ff6774dc6-vb6rx");
        Console.WriteLine("podItem.Name");
        Console.WriteLine(podItem.Name());
        var logs = await PodService.Logs(
            podItem, true);

        var x = await PodService.ExecInPod(podItem, "ls");


        await using (var xs = new XtermStream(_terminal))
        {
            // await x.CopyToAsync(xs);
        }


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


    private async Task Resize(MouseEventArgs args)
    {
        await _terminal.Resize(_columns, _rows);
    }


    private async Task AddCommand(string ct)
    {
        await _terminal.WriteLine(ct);
    }
}
