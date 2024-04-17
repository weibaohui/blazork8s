using System.Threading.Tasks;
using BlazorApp.Service;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp.Pages.Common.Metadata;

public partial class KubectlExplainView : DrawerPageBase<string>

{
    private string _cultureName;
    private string _result;
    private string _translateResult;

    [Parameter] public string Field { get; set; }

    [Inject] private IKubectlService Kubectl { get; set; }

    [Inject] private IDocService DocService { get; set; }

    [Inject] private IAiService AiService { get; set; }

    [Inject] private ILogger<KubectlExplainView> Logger { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Field = base.Options;


        var x = (SimpleI18NStringLocalizer)L;
        _cultureName = x.GetCulture().Name;


        if (AiService.Enabled()) AiService.SetChatEventHandler(EventHandler);

        //先查询缓存中有没有解释
        //没有解释执行kubectl explain
        //缓存中只有中文，其他语言执行AI翻译
        if (!Field.IsNullOrEmpty())
        {
            var explainEntity = await DocService.GetExplainByField(Field);
            _result = explainEntity?.Explain;
            if (_cultureName == "zh-CN") _translateResult = explainEntity?.ExplainCN;
        }

        if (string.IsNullOrWhiteSpace(_result))
        {
            Logger.LogWarning("no explain for {Field}", Field);
            //没有解释，执行kubectl explain
            _result = await Kubectl.Explain(Field);
        }

        //没有翻译，执行AI翻译
        if (string.IsNullOrWhiteSpace(_translateResult))
        {
            Logger.LogWarning("translate {Field},AI enable:{Enabled}", Field, AiService.Enabled());
            await ReTranslate();
        }

        await base.OnInitializedAsync();
    }


    private async void EventHandler(object sender, string resp)
    {
        _translateResult += resp;
        await InvokeAsync(StateHasChanged);
    }

    private async Task ReTranslate()
    {
        if (!AiService.Enabled()) return;

        _translateResult = "";
        await InvokeAsync(StateHasChanged);

        var language = SimpleI18NStringLocalizer.LanguageMap[_cultureName];
        _translateResult =
            await AiService.AIChat($"请将下面的内容，逐字翻译为{language}。注意请不要遗漏细节并保持原来的格式：\r" + _result);
        await InvokeAsync(StateHasChanged);
    }
}
