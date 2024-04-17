using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Ai;

public partial class ChatDeployment : PageBase
{
    private readonly List<string> _data =
    [
        "部署一个k8s nginx应用",
        "请给我一套k8s部署yaml，名称为nginx，可以通过ingress访问，域名为www.nginx.com，并使用```yaml ```包裹起来",
        "请给我一套k8s部署yaml，名称为nginx，可以通过ingress访问",
        "请给出一套部署2048小游戏的k8s yaml"
    ];

    private string _advice;
    private string _aiName = "";
    private string _execResult;
    private string _txtValue;
    private string _yamlAdvice;


    [Inject] private IAiService Ai { get; set; }

    [Inject] private IKubectlService Kubectl { get; set; }


    protected override async Task OnInitializedAsync()
    {
        _aiName = Ai.Name();
        Ai.SetChatEventHandler(EventHandler);
        await base.OnInitializedAsync();
    }

    private async void EventHandler(object sender, string resp)
    {
        _advice += resp;
        await InvokeAsync(StateHasChanged);
    }


    private async Task ChatBtnClicked()
    {
        _advice = "";
        _yamlAdvice = "";
        _execResult = "";

        if (!string.IsNullOrEmpty(_txtValue))
        {
            _advice = await Ai.AIChat(_txtValue);
            _yamlAdvice = GetRegexYaml(_advice);
        }
    }

    private string GetRegexYaml(string input)
    {
        var pattern = "```yaml([^`]*)```";
        var tmp = RegexYaml(input, pattern);
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
        var result = string.Empty;
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
