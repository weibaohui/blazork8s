using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlazorApp.Service;
using BlazorApp.Service.k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.ai;

public partial class ChatDeployment : ComponentBase
{
    public string txtValue;
    public string Advice;
    public string YamlAdvice;
    bool          _loading = false;
    public string ExecResult;

    [Inject]
    private IOpenAiService OpenAi { get; set; }

    [Inject]
    private IKubectlService kubectl { get; set; }

    [Inject]
    private IRockAiService RockAi { get; set; }

    public List<string> data = new List<string>
    {
        "部署一个k8s nginx应用",
        "请给我一套k8s部署yaml，名称为nginx，可以通过ingress访问，域名为www.nginx.com，并使用```yaml ```包裹起来",
        "请给我一套k8s部署yaml，名称为nginx，可以通过ingress访问",
        "请给出一套部署2048小游戏的k8s yaml",
    };

    bool _visible = false;

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
        await base.OnInitializedAsync();
    }

    private async Task ChatBtnClicked()
    {
        Advice     = "";
        YamlAdvice = "";
        ExecResult = "";

        if (!string.IsNullOrEmpty(txtValue))
        {
            _loading = true;
            Advice   = await OpenAi.Chat(txtValue);
            // Advice     = await RockAi.Chat(txtValue);
            YamlAdvice = GetRegexYaml(Advice);
            _loading   = false;
        }
    }

    private string GetRegexYaml(string input)
    {
        string pattern = "```yaml([^`]*)```";
        var    tmp     = RegexYaml(input, pattern);
        if (string.IsNullOrEmpty(tmp))
        {
            tmp = RegexYaml(input, "```([^`]*)```");
        }

        if (string.IsNullOrEmpty(tmp))
        {
            if (Advice.StartsWith("---") || Advice.StartsWith("apiVersion"))
            {
                return Advice;
            }
        }

        return tmp;
    }

    private static string RegexYaml(string input, string pattern)
    {
        var             result  = string.Empty;
        MatchCollection matches = Regex.Matches(input, pattern);
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
        ExecResult = string.Empty;
        ExecResult = await kubectl.Apply(YamlAdvice);
    }

    private async Task BtnDeleteClicked()
    {
        ExecResult = string.Empty;
        ExecResult = await kubectl.Delete(YamlAdvice);
    }
}
