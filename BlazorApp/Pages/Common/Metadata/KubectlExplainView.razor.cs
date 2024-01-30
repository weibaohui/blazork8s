using System.Threading.Tasks;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp.Pages.Common.Metadata;

public partial class KubectlExplainView : DrawerPageBase<string>

{
    [Parameter]
    public string Field { get; set; }

    [Inject]
    IKubectlService Kubectl { get; set; }

    [Inject]
    IAiService AiService    { get; set; }

    private string _result;
    private string _resultCn;

    protected override async Task OnInitializedAsync()
    {
        Field = base.Options;
        await base.OnInitializedAsync();
        if (!Field.IsNullOrEmpty())
        {
            _result = await Kubectl.Explain(Field);
        }

        if (AiService.Enabled())
        {
            AiService.SetChatEventHandler(EventHandler);
            _resultCn=await AiService.AIChat("请逐字翻译以下内容，注意请不要遗漏细节并保持原来的格式：" + _result);
        }
    }


    private async void EventHandler(object sender, string resp)
    {
        _resultCn += resp;
        await InvokeAsync(StateHasChanged);
    }

    private async Task ReTranslate()
    {
        _resultCn = "";
        await InvokeAsync(StateHasChanged);
        _resultCn = await AiService.AIChat("请详细翻译下这段文字，不要遗漏细节：" + _result);
    }
}
