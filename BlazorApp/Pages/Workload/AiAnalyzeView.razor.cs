using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Workload;

public partial class AiAnalyzeView : FeedbackComponent<IAiService.AiChatData, bool>
{
    [Inject]
    private IPodService PodService { get; set; }

    [Inject]
    private IAiService Ai { get; set; }

    string                        Advice { get; set; }
    private IAiService.AiChatData _item;

    protected override async Task OnInitializedAsync()
    {
        _item = base.Options;
        Ai.SetChatEventHandler(EventHandler);
        Advice = _item.Style switch
        {
            "security" => await Ai.ExplainSecurity(KubernetesJson.Serialize(_item)),
            "error"    => await Ai.ExplainError(KubernetesJson.Serialize(_item)),
            _          => Advice
        };

        await base.OnInitializedAsync();
    }


    private async void EventHandler(object sender, string resp)
    {
        Advice += resp;
        await InvokeAsync(StateHasChanged);
    }
}
