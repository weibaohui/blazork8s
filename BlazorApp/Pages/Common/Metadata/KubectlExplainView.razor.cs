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
    IDocService DocService { get; set; }

    [Inject]
    IAiService AiService { get; set; }

    private string _result;
    private string _resultCn;

    protected override async Task OnInitializedAsync()
    {
        Field = base.Options;
        await base.OnInitializedAsync();

        //先查询缓存中有没有解释
        //没有解释执行kubectl explain
        //缓存中没有中文，执行AI翻译
        if (!Field.IsNullOrEmpty())
        {
            var explainEntity =await DocService.GetExplainByField(Field);
            _result   = explainEntity?.Explain;
            _resultCn = explainEntity?.ExplainCN;
        }

        if (string.IsNullOrWhiteSpace(_result))
        {
            //没有解释，执行kubectl explain
            _result = await Kubectl.Explain(Field);

            if (string.IsNullOrWhiteSpace(_resultCn))
            {
                //没有中文解释，查询翻译
                if (AiService.Enabled())
                {
                    AiService.SetChatEventHandler(EventHandler);
                    _resultCn = await AiService.AIChat("请逐字翻译以下内容，注意请不要遗漏细节并保持原来的格式：" + _result);
                }
            }
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
