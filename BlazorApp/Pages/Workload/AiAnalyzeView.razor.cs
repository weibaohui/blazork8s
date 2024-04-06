using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Workload;

public partial class AiAnalyzeView : DrawerPageBase<IAiService.AiChatData>
{
    private IAiService.AiChatData _item;

    [Inject]
    private IPodService PodService { get; set; }

    [Inject]
    private IAiService Ai { get; set; }

    string Advice { get; set; }

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