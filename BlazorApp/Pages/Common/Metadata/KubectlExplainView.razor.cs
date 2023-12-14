using System.Threading.Tasks;
using BlazorApp.Service.k8s;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp.Pages.Common.Metadata;

public partial class KubectlExplainView : ComponentBase
{
    [Parameter]
    public string Field { get; set; }

    [Inject]
    IKubectlService Kubectl { get; set; }

    public string Result { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (!Field.IsNullOrEmpty())
        {
            Result = await Kubectl.Explain(Field);
        }
    }
}
