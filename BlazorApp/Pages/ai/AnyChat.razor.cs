using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.AI;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ai;

public partial class AnyChat : DrawerPageBase<string>
{
    [Inject]
    IAiService AiService { get; set; }

    private string _ask    = string.Empty;
    private string _result = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        _ask = base.Options;

        if (AiService.Enabled())
        {
            AiService.SetChatEventHandler(EventHandler);
        }

        if (!string.IsNullOrWhiteSpace(_ask))
        {
            _result = await AiService.AIChat(_ask);
        }

        await base.OnInitializedAsync();
    }


    private async void EventHandler(object sender, string resp)
    {
        _result += resp;
        await InvokeAsync(StateHasChanged);
    }
}
