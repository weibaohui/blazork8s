using System.Threading.Tasks;
using BlazorApp.Service.k8s;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Pages.Common.Metadata;

public partial class KubectlDescribeView : DrawerPageBase<string>
{


    [Inject]
    IKubectlService Kubectl { get; set; }




    [Inject]
    ILogger<KubectlExplainView> Logger { get; set; }

    private string _resource =string.Empty;
    private string _result   =string.Empty;

    protected override async Task OnInitializedAsync()
    {
        _resource=base.Options;
        if (!string.IsNullOrWhiteSpace(_resource))
        {
            _result = await Kubectl.Describe(_resource);
            _result = _result.Replace("  ", " ");
        }
        await base.OnInitializedAsync();
    }
}
