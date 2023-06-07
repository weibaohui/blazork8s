using System.Text.Json;
using System.Threading.Tasks;
using AntDesign;
using BlazorApp.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Pod;

public partial class PodAnalyzeView : FeedbackComponent<V1Pod, bool>
{
    [Inject]
    private IPodService PodService { get; set; }

    [Inject]
    private IOpenAiService OpenAi { get; set; }

    public V1Pod  PodItem;
    public string Advice { get; set; }
    bool          _loading = true;


    protected override async Task OnInitializedAsync()
    {
        PodItem = base.Options;
        Advice  = await OpenAi.Explain(JsonSerializer.Serialize(PodItem));
        await base.OnInitializedAsync();
        _loading = false;
    }
}
