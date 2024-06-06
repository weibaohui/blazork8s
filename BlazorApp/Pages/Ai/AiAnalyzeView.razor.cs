using System;
using System.Threading.Tasks;
using BlazorApp.Pages.Common;
using BlazorApp.Service.AI;
using BlazorApp.Service.k8s;
using k8s;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Ai;

public partial class AiAnalyzeView : DrawerPageBase<IAiService.AiChatData>
{
    private IAiService.AiChatData _item;

    [Inject] private IPodService PodService { get; set; }

    [Inject] private IAiService Ai { get; set; }
    [Inject] private IPromptService PromptService { get; set; }

    string Advice { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _item = base.Options;
        Ai.SetChatEventHandler(EventHandler);

        var prompt = _item.Style switch
        {
            "security" => PromptService.GetPrompt("Security"),
            "error" => PromptService.GetPrompt("Error"),
            "Optimize" => PromptService.GetPrompt("Optimize"),
            _ => "Explain"
        };
        Console.WriteLine(prompt);
        var serialize = KubernetesJson.Serialize(_item);
        var content = $"{prompt} \n {serialize}";
        Advice = await Ai.AIChat(content);

        await base.OnInitializedAsync();
    }


    private async void EventHandler(object sender, string resp)
    {
        Advice += resp;
        await InvokeAsync(StateHasChanged);
    }
}
