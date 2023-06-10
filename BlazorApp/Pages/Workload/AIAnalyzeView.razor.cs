using System;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service;
using k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Workload;

public partial class AIAnalyzeView : FeedbackComponent<Object, bool>
{
    [Inject]
    private IPodService PodService { get; set; }

    [Inject]
    private IOpenAiService OpenAi { get; set; }

    IOpenAiService.AIChatData _item;
    string                    Advice { get; set; }
    bool                      _loading = true;


    protected override async Task OnInitializedAsync()
    {
        _item = (IOpenAiService.AIChatData)base.Options;
        switch (_item.style)
        {
            case "security":
                Advice = await OpenAi.ExplainSecurity(KubernetesYaml.Serialize(_item));
                break;
            case "error":
                Advice = await OpenAi.ExplainError(KubernetesYaml.Serialize(_item));
                break;
        }

        await base.OnInitializedAsync();
        _loading = false;
    }
}