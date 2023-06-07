using System.Text.Json;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Deployment;

public partial class DeploymentAnalyzeView :  FeedbackComponent<V1Deployment, bool>
{

    [Inject]
    private IOpenAiService OpenAi { get; set; }

    public V1Deployment  Item;
    public string Advice { get; set; }
    bool          _loading = true;


    protected override async Task OnInitializedAsync()
    {
        Item = base.Options;
        Advice  = await OpenAi.Explain(JsonSerializer.Serialize(Item));
        await base.OnInitializedAsync();
        _loading = false;
    }
}
