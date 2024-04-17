using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.AI;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Ai;

public partial class AnyChat : DrawerPageBase<string>
{
    private string _ask = string.Empty;
    private string _result = string.Empty;

    [Inject] private IAiService AiService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _ask = base.Options;

        if (AiService.Enabled())
        {
            AiService.SetChatEventHandler(EventHandler);

            if (!string.IsNullOrWhiteSpace(_ask))
            {
                _result = await AiService.AIChat(_ask);
            }
        }
        else
        {
            _result = "请先开启AI能力，并配置AI参数";
        }


        await base.OnInitializedAsync();
    }


    private async void EventHandler(object sender, string resp)
    {
        _result += resp;
        await InvokeAsync(StateHasChanged);
    }
}
