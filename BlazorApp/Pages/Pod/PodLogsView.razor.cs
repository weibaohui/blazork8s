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
    [Inject]
    private IPodService PodService { get; set; }

    [Inject]
    private IHubContext<ChatHub> _ctx { get; set; }


    public V1Pod PodItem;


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        PodItem = base.Options;
        await new PodLogHelper()
            .Create(PodItem).SetHubContext(_ctx)
            .BuildCommand()
            .Exec();
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
        // "xterm-addon-attach",
        "xterm-addon-fit"
    };


    private int    _columns, _rows;
    private string _searchInput = "";


    private async Task OnFirstRender()
    {


        // Blazor Server
        // await _terminal.InvokeAddonFunctionVoidAsync("xterm-addon-attach", "new AttachAddon()");
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



    private async Task OnPodLogChanged(PodLogEntity obj)
    {
        if (obj.Namespace==PodItem.Namespace()&&obj.Name==PodItem.Name())
        {
            await _terminal.WriteLine(obj.LogLineContent);
        }
    }
}
