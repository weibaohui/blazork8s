using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ai;

public partial class ChatDeployment : ComponentBase
{
    private string _txtValue;
    private string _advice;
    private string _yamlAdvice;
    private string _execResult;

    [Inject]
    private IXunFeiAiService XunFeiAi { get; set; }
    [Inject]
    private IOpenAiService OpenAi { get; set; }
    [Inject]
    private IQwenAiService QwenAi { get; set; }
    [Inject]
    private IKubectlService Kubectl { get; set; }


    private readonly List<string> _data =
    [
        "部署一个k8s nginx应用",
        "请给我一套k8s部署yaml，名称为nginx，可以通过ingress访问，域名为www.nginx.com，并使用```yaml ```包裹起来",
        "请给我一套k8s部署yaml，名称为nginx，可以通过ingress访问",
        "请给出一套部署2048小游戏的k8s yaml"
    ];

    private bool _visible = false;

    private Task Open()
    {
        this._visible = true;
        return Task.CompletedTask;
    }

    private Task Close()
    {
        this._visible = false;
        return Task.CompletedTask;
    }

    protected override async Task OnInitializedAsync()
    {
        XunFeiAi.SetChatEventHandler(EventHandler);
        QwenAi.SetChatEventHandler(QwenEventHandler);
        await base.OnInitializedAsync();
    }

    private async void EventHandler(object sender, string resp)
    {
        _advice     += resp;
        _yamlAdvice += resp;
        await InvokeAsync(StateHasChanged);
    }
    private async void QwenEventHandler(object sender, string resp)
    {
        _advice     = resp;
        _yamlAdvice = resp;
        await InvokeAsync(StateHasChanged);
    }

    private async Task ChatBtnClicked()
    {
        _advice     = "";
        _yamlAdvice = "";
        _execResult = "";

        if (!string.IsNullOrEmpty(_txtValue))
        {
            // _advice     = await XunFeiAi.AIChat(_txtValue);
            // _advice     = await OpenAi.Chat(_txtValue);
            _advice     = await QwenAi.AIChat(_txtValue);
            _yamlAdvice = GetRegexYaml(_advice);
        }
    }

    private string GetRegexYaml(string input)
    {
        var pattern = "```yaml([^`]*)```";
        var tmp     = RegexYaml(input, pattern);
        if (string.IsNullOrEmpty(tmp))
        {
            tmp = RegexYaml(input, "```([^`]*)```");
        }

        if (!string.IsNullOrEmpty(tmp)) return tmp;
        if (_advice.StartsWith("---") || _advice.StartsWith("apiVersion"))
        {
            return _advice;
        }

        return tmp;
    }

    private static string RegexYaml(string input, string pattern)
    {
        var result  = string.Empty;
        var matches = Regex.Matches(input, pattern);
        foreach (Match match in matches)
        {
            if (match.Success)
            {
                result += match.Groups[1].Value;
            }
        }

        return result;
    }

    private async Task BtnApplyClicked()
    {
        _execResult = string.Empty;
        _execResult = await Kubectl.Apply(_yamlAdvice);
    }

    private async Task BtnDeleteClicked()
    {
        _execResult = string.Empty;
        _execResult = await Kubectl.Delete(_yamlAdvice);
    }

    private void CopyToSearch(string item)
    {
        _txtValue = item;
    }
}
