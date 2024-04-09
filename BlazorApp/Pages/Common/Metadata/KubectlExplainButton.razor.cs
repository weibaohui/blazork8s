using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using BlazorApp.Pages.Common;

namespace BlazorApp.Pages.Common.Metadata;

public partial class KubectlExplainButton:PageBase
{
    [Parameter]
    public string Field { get; set; }

    private async Task KubectlExplainClick()
    {
        var options = PageDrawerService.DefaultOptions("Kubectl Explain", width: 800);
        await PageDrawerService.ShowDrawerAsync<KubectlExplainView, string, bool>(options, Field);
    }
}
