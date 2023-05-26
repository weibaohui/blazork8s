using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using XtermBlazor;

namespace BlazorApp.Pages.Learn
{
    public partial class LessonIndex : ComponentBase
    {
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


        private int _columns, _rows;


        private async Task OnFirstRender()
        {
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
}
