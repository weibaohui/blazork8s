using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlazorApp.Service;
using BlazorApp.Service.impl;
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

    private async Task ChatBtnClicked()
    {
        Advice     = "";
        YamlAdvice = "";

        if (!string.IsNullOrEmpty(txtValue))
        {
            _loading   = true;
            Advice     = await OpenAi.Chat(txtValue);
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
        Match match = Regex.Match(input, pattern);
        if (match.Success)
        {
            string code = match.Groups[1].Value;
            return code;
        }

        return string.Empty;
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
